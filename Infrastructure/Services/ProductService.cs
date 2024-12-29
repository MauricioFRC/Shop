using Core.DTOs.Product;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;
using Mapster;
using QRCoder;
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
        //var category = await _productRepository.GetCategories(categoryName) 
            //?? throw new Exception("No se encontró la categoría");

        //var product = category.Adapt<Product>();
        //createProductRequest.CategoryId = category.Id;

        var createProduct = await _productRepository.CreateProduct(createProductRequest)
            ?? throw new ArgumentNullException("No se pudo crear el producto.");

        return createProduct;
    }

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

    private static void ValidateId(int id) => ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
}
