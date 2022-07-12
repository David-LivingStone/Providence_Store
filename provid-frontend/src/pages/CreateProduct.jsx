import axios from 'axios';
import React, { useEffect, useState } from 'react'
import { BASE_URL } from '../config';

const CreateProduct = () => {
    const [loading, setLoading] = useState(false)
    const handleSubmit = (e) => {
        e.preventDefault();
        setLoading(true)
        let model = {
            // productName: productName,
            // businessName: businessName,
            // isBusiness: businessName ? true : false,
            // rating: rate,
            // description: description,
            // businessAddress: businessAddress,
            // businessEmail: businessEmail,
            // businessPhone: businessPhone
        }
        axios.post(`${BASE_URL}postreview`, model)
            .then((res) => {
                console.log(res);
                setLoading(false)
            })
            .catch(err => {
                setLoading(false)
                console.log(err)
            })
    }
  return (
    <section className="container">
        <h3 className="my-3 text-center">Add Product</h3>
        <form className="form-horizontal" onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="formGroupExampleInput">Product Name</label>
          <input type="text" className="form-control" id="formGroupExampleInput" placeholder="Product Name" />
        </div>
        <div className="form-group my-3">
          <label htmlFor="formGroupExampleInput2">Category</label>
          <input type="text" className="form-control" id="formGroupExampleInput2" />
        </div>
        <div className="form-group my-3">
          <label htmlFor="formGroupExampleInput2">Price</label>
          <input type="number" className="form-control" id="formGroupExampleInput2" />
        </div>
        <div className="form-group my-3">
          <label htmlFor="formGroupExampleInput2">Quantity</label>
          <input type="number" className="form-control" id="formGroupExampleInput2" />
        </div>
        <div className="form-group my-3">
          <label htmlFor="formGroupExampleInput2">Description</label>
          <textarea className="form-control"></textarea>
        </div>

        <button className="btn btn-success btn-block my-3">Post</button>
      </form>
    </section>
  )
}

export default CreateProduct