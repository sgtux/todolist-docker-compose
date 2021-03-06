import React, { useState, useEffect } from 'react'
import axios from 'axios'

export function TodoList({ urlApi, token }) {

    const requestConfig = { headers: { "Authorization": `Bearer ${token}` } }

    const [description, setDescription] = useState('')
    const [error, setError] = useState('')
    const [todoItems, setTodoItems] = useState([])
    const [editItem, setEditItem] = useState({})
    const [filter, setFilter] = useState('')

    useEffect(() => refresh(), [])

    function refresh(queryFilter = '') {

        setDescription('')
        setEditItem(null)

        axios.get(`${urlApi}/todo?filter=${queryFilter}`, requestConfig)
            .then(res => setTodoItems(res.data))
            .catch(err => handleError(err))
    }

    function doneChanged(item) {
        item.done = !item.done
        axios.put(`${urlApi}/todo`, item, requestConfig)
            .then(res => refresh())
            .catch(err => handleError(err))
    }

    function save() {
        const item = editItem ? { ...editItem, description } : { description }
        if (item.id)
            axios.put(`${urlApi}/todo`, item, requestConfig)
                .then(res => refresh())
                .catch(err => handleError(err))
        else
            axios.post(`${urlApi}/todo`, item, requestConfig)
                .then(res => refresh())
                .catch(err => handleError(err))
    }

    function toEdit(item) {
        setEditItem(item)
        setDescription(item.description)
    }

    function remove(id) {
        const item = { description }
        axios.delete(`${urlApi}/todo/${id}`, item, requestConfig)
            .then(res => refresh())
            .catch(err => handleError(err))
    }

    function handleError(err) {
        console.log(err)
        setError(err.response.data)
        setTimeout(() => setError(''), 2000)
    }

    function filterChanged(filter) {
        setFilter(filter)
        refresh(filter)
    }

    return (
        <div className="App">
            <header className="App-header">
                <h2>TODO LIST</h2>
                <div style={{ marginBottom: 40 }}>
                    <input value={description} onChange={e => setDescription(e.target.value)} />
                    <button style={{ marginLeft: 5 }} onClick={() => { setEditItem(null); setDescription('') }} type="button" className="btn btn-cancel">CANCEL</button>
                    <button style={{ marginLeft: 5 }} onClick={() => save()} type="button" className="btn btn-add">SAVE</button>
                    <div>
                        <span hidden={!error} className="error-message">{error}</span>
                    </div>
                </div>
                <input value={filter} onChange={e => filterChanged(e.target.value)} placeholder="Filter" />
                <table className="todo-table">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Description</th>
                            <th>Done?</th>
                            <th colSpan="2"></th>
                        </tr>
                    </thead>
                    <tbody>
                        {todoItems.map(p =>
                            <tr key={p.id + ''}>
                                <td>{p.id}</td>
                                <td>{p.description}</td>
                                <td>
                                    <input onChange={() => doneChanged(p)} type="checkbox" checked={p.done} />
                                </td>
                                <td>
                                    <button onClick={() => toEdit(p)} className="btn btn-edit">Edit</button>
                                </td>
                                <td>
                                    <button onClick={() => remove(p.id)} className="btn btn-remove">Remove</button>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </header>
        </div>
    );
}