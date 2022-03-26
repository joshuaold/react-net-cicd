import { useState, useEffect } from "react";
import "./App.css";
import { get } from "./fetch-helper";

const App = () => {
  const [products, setProducts] = useState();

  useEffect(() => {
    const fetchProducts = async () => {
      const response = await getProducts();
      setProducts(response);
    };

    fetchProducts();
  }, []);

  return (
    <div>
      <h1>jhkhkj</h1>
      {products && products.items.map((product) => <p>{product.name}</p>)}
    </div>
  );
};

const getProducts = async () => {
  const response = await get(
    "https://warehousesample.azurewebsites.net/api/products/constant"
  );

  return response;
};

export default App;
