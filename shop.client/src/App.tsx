import { useEffect, useState } from 'react';
import './App.css';

interface Product {
	id: number;
	name: string;
	description: string;
	price: number;
}

function App() {
	const [products, setProducts] = useState<Product[]>();

	useEffect(() => {
		productsData();
	}, []);

	const contents = products === undefined
		? <p><em>Loading...</em></p>
		: <table className="table table-striped" aria-labelledby="tableLabel">
			<thead>
				<tr>
					<th>ID</th>
					<th>Name</th>
					<th>Price</th>
					<th>Description</th>
				</tr>
			</thead>
			<tbody>
				{products.map(products =>
					<tr key={products.id}>
						<td>{products.id}</td>
						<td>{products.name}</td>
						<td>{products.price}</td>
						<td>{products.description}</td>
					</tr>
				)}
			</tbody>
		</table>;

	return (
		<div>
			<h1 id="tableLabel">Products</h1>
			{contents}
		</div>
	);

	async function productsData() {
		const response = await fetch('http://localhost:5022/products');
		if (response.ok) {
			const data = await response.json();
			setProducts(data);
		}
	}
}

export default App;