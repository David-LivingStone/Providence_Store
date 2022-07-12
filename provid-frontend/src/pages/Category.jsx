import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { BASE_URL } from '../config';

const Category = () => {
    const [Category, setCategory] = useState([]);
    const [loading, setLoading] = useState(false)
    const [name, setName] = useState("")

    useEffect(() => {
        getCategory();
    }, [])

    const getCategory = async () => {
        setLoading(true)
        await axios.get(`${BASE_URL}Category`, {
            mode: 'no-cors'
        })
            .then((res) => {
                console.log(res.data);
                setCategory(res.data);
                setLoading(false)
            })
            .catch(err => {
                setLoading(false)
                console.log(err)
            })
    }

    const handleSubmit =(e) =>{
        e.preventDefault();
        setLoading(true)
        let model = {
            categoryName: name
        }
        axios.post(`${BASE_URL}Category`, model)
            .then((res) => {
                console.log(res);
                getCategory();
                setLoading(false)
            })
            .catch(err => {
                setLoading(false)
                console.log(err)
            })
    }

    return (
        <>
            <div className="container">
                <div className="row">
                    <div className="col-sm-12">
                        <button type="button" className="btn btn-primary my-3" data-bs-toggle="modal" data-bs-target="#exampleModal">Create Category</button>
                    </div>
                </div>
                <div className="row">
                    <div className="col-sm-12">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Category Name</th>
                                </tr>
                            </thead>
                            <tbody>
                                {Category.map((cat, index) => (

                                    <tr key={index}>
                                        <th scope="row">1</th>
                                        <td>{cat.categoryName}</td>
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


            <div>

                {/* Modal */}
                <div className="modal fade" id="exampleModal" tabIndex={-1} aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title" id="exampleModalLabel">Modal title</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close" />
                            </div>
                            <div className="modal-body">
                                <form className="form-horizontal" onSubmit={handleSubmit}>
                                    <div className="form-group">
                                        <label htmlFor="formGroupExampleInput">Category Name</label>
                                        <input type="text" onChange={(e)=>setName(e.target.value)} className="form-control" id="formGroupExampleInput" placeholder="Category Name" />
                                    </div>
                                    <button type="submit" className="btn btn-success btn-block my-3">Add Caegory</button>
                                </form>
                            </div>

                        </div>
                    </div>
                </div>
            </div >
        </>
    )
}

export default Category