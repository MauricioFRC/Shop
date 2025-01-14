"use client";
import { useState, useEffect } from "react";

export default function Products() {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const productsResponse = await fetch("http://localhost:5022/api/products");
        const productsData = await productsResponse.json();

        const productsWithImages = await Promise.all(
          productsData.map(async (product) => {
            const imagesResponse = await fetch(`http://localhost:5022/api/get-product-image?id=${product.id}`);
            const imagesData = await imagesResponse.json();
            return { ...product, images: imagesData };
          })
        );

        setProducts(productsWithImages);
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <h1 className="font-bold text-center text-3xl pt-5">Products</h1>
      <table className="table-auto border-collapse gap-3 w-full mx-10">
        <thead>
          <tr className="text-left text-xl">
            <th>Id</th>
            <th>Name</th>
            <th>Price</th>
            <th>Stock</th>
            <th>Category</th>
            <th>Images</th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <tr key={product.id}>
              <td>{product.id}</td>
              <td>{product.name}</td>
              <td>{product.price}</td>
              <td>{product.stock}</td>
              <td>{product.category}</td>
              <td>
                {product.images.map((image, index) => (
                  <img
                    key={index}
                    src={image.url}
                    alt={product.name}
                    className="w-16 h-16 object-cover"
                  />
                ))}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
