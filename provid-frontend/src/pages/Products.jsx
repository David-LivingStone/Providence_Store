import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { BASE_URL } from '../config'

const Products = () => {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(false)

    useEffect(() => {
      getProducts();
    }, [])
    const getProducts = async () => {
        setLoading(true)
        await axios.get(`${BASE_URL}Product`, {
            mode: 'no-cors'
        })
            .then((res) => {
                 console.log(res.data);
                setProducts(res.data);
                setLoading(false)
            })
            .catch(err => {
                setLoading(false)
                console.log(err)
            })
    }
    
    return (
        <div className="container">
            <div className="row">
                <div className="col-sm-12">
                    <a href="/create-product" className="btn btn-primary my-4">Create Product</a>
                </div>
            </div>
            <div className="row">
                <div className="col-sm-12">
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Product Name</th>
                                <th scope="col">Category</th>
                                <th scope="col">Quatity</th>
                                <th scope="col">Desc</th>
                                <th scope="col">Price</th>
                                <th scope="col">Date</th>
                                <th scope="col">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            {products.map((product, index) =>(

                            <tr>
                                <th scope="row">1</th>
                                <td>{product.productName}</td>
                                <td>{product.category}</td>
                                <td>{product.quantity}</td>
                                <td>1</td>
                                <td>{product.price}</td>
                                <td>{product.date}</td>
                                <td>
                                    <a href="#" className="btn btn-warning btn-small">Edit</a> &nbsp;
                                    <a href="#" className="btn btn-danger btn-small">Delete</a>
                                </td>
                            </tr>))}

                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    )
}

export default Products