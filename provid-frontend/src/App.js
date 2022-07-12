import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Category from './pages/Category';
import CreateProduct from './pages/CreateProduct';
import Home from "./pages/Home";
import Navbar from "./pages/Navbar";
import Products from './pages/Products';

function App() {
  return (
    <>
    <Navbar />
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/products" element={<Products />} />
      <Route path="/create-product" element={<CreateProduct />} />
      <Route path="/categories" element={<Category />} />
    </Routes>
    </>
  );
}

export default App;
