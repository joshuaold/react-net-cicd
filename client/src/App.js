import { useState, useEffect } from "react";
import logo from "./logo.svg";
import "./App.css";
import { get } from "./fetch-helper";

const App = () => {
  const [products, setProducts] = useState();

  useEffect(async () => {
    const response = await getProducts();
    setProducts(response);
  }, []);

  return (
    <div>
      <h1>Updated</h1>
      {products && products.items.map((product) => <p>{product.name}</p>)}
    </div>
  );
};

const getProducts = async () => {
  const response = await get(
    "https://warehousesample.azurewebsites.net/api/products/"
  );

  return response;
};

export default App;
