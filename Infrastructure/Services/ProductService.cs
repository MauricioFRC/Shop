using Core.DTOs.Product;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;
using Mapster;
using Microsoft.AspNetCore.Http;
using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponseDTO> CreateProduct(CreateProductRequest createProductRequest)
    {
        var createProduct = await _productRepository.CreateProduct(createProductRequest)
            ?? throw new ArgumentNullException("No se pudo crear el producto.");

        return createProduct;
    }

    //public async Task<ProductResponseDTO> UploadProductImage(int productId, IFormFile file, CancellationToken cancellationToken)
    //{
    //    ValidateId(productId);
    //    var uploadProductImage = await _productRepository.UploadProductImage(productId, file, cancellationToken)
    //        ?? throw new ArgumentNullException("No se pudo subir la imagen del producto");

    //    return uploadProductImage;
    //}

    //public async Task<byte[]> GetProductImage(int productId, CancellationToken cancellationToken)
    //{
    //    ValidateId(productId);
    //    var productImage = await _productRepository.GetProductImage(productId, cancellationToken)
    //        ?? throw new ArgumentNullException($"No se encontró la imagen del producto con el Id: {productId}");

    //    return productImage;
    //}

    public async Task<ProductResponseDTO> DeleteProduct(int id)
    {
        ValidateId(id);
        var deleteProduct = await _productRepository.DeleteProduct(id);

        return deleteProduct == null ? throw new Exception("No se encontró el producto") : deleteProduct.Adapt<ProductResponseDTO>();
    }

    public async Task<List<ProductResponseDTO>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProducts();
        return products == null ? throw new Exception("No hay ningun producto disponible") : products.Adapt<List<ProductResponseDTO>>();
    }

    public async Task<ProductResponseDTO> GetProductById(int id)
    {
        var getProduct = await _productRepository.GetProductById(id);

        return getProduct ?? throw new Exception("No se encontró el Id solicitado");
    }

    public async Task<ProductResponseDTO> UpdateProduct(int id, UpdateProductDTO updateProductDTO)
    {
        var updateProduct = await _productRepository.UpdateProduct(id, updateProductDTO);

        return updateProduct ?? throw new Exception("No se encontró el producto solicitado");
    }

    public async Task<byte[]> GenerateProductDescriptionQr(int productId)
    {
        ValidateId(productId);
        var searchProduct = await _productRepository.GetProductById(productId)
          ?? throw new Exception($"No se encontró ningun producto con el Id: {productId}");

        var productDataFormated = new StringBuilder();
        productDataFormated.AppendLine($"Nombre: {searchProduct.Name}");
        productDataFormated.AppendLine($"Precio: {searchProduct.Price}");
        productDataFormated.AppendLine($"Categoría: {searchProduct.Category}");
        productDataFormated.AppendLine($"Descripción: {searchProduct.Description}");
        productDataFormated.AppendLine($"Stock: {searchProduct.Stock}");

        var data = productDataFormated.ToString();

        using QRCodeGenerator qrCodeGenerator = new();
        using QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
        using PngByteQRCode qrCode = new(qrCodeData);
        byte[] qrCodeImage = qrCode.GetGraphic(5);

        return qrCodeImage;
    }

    public async Task<byte[]> GeneratePdfProductReport(int maxRange, CancellationToken cancellationToken)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var products = await _productRepository.GetProductsByRange(maxRange, cancellationToken);

        var document = Document.Create(c =>
        {
            c.Page(page =>
            {
                page.Margin(50);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header().Text("Reporte de Productos").SemiBold().FontSize(20).AlignCenter();
                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn(3);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(2);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Id").SemiBold();
                        header.Cell().Text("Nombre").SemiBold();
                        header.Cell().Text("Precio").SemiBold();
                        header.Cell().Text("Descripción").SemiBold();
                        header.Cell().Text("Stock").SemiBold();
                        header.Cell().Text("Categoría").SemiBold();
                    });

                    foreach (var product in products)
                    {
                        table.Cell().Text(product.Id.ToString());
                        table.Cell().Text(product.Name);
                        table.Cell().Text(product.Price.ToString());
                        table.Cell().Text(product.Description ?? "Sin descripción");
                        table.Cell().Text(product.Stock.ToString());
                        table.Cell().Text(product.Category ?? "Sin categoría");
                    }
                });

                page.Footer().AlignCenter().Text($"Generado el {DateTime.Now:dd/MM/yyyy}");
            });
        });

        using MemoryStream stream = new();
        document.GeneratePdf(stream);
        return stream.ToArray();
    } 

    private static void ValidateId(int id) => ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
}
