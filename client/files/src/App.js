import React, { useState, useEffect } from 'react';
import './App.css';
import axios from 'axios'

let urlApi = ''

function App() {

  const [description, setDescription] = useState('')
  const [error, setError] = useState('')
  const [todoItems, setTodoItems] = useState([])
  const [editItem, setEditItem] = useState({})

  useEffect(async () => {
    const response = await axios.get('/apihost')
    urlApi = `http://${response.data}`
    refresh()
  }, [])

  function refresh() {

    setDescription('')
    setEditItem(null)

    axios.get(`${urlApi}/todo`)
      .then(res => setTodoItems(res.data))
      .catch(err => handleError(err))
  }

  function doneChanged(item) {
    item.done = !item.done
    axios.put(`${urlApi}/todo`, item)
      .then(res => refresh())
      .catch(err => handleError(err))
  }

  function save() {
    const item = editItem ? { ...editItem, description } : { description }
    if (item.id)
      axios.put(`${urlApi}/todo`, item)
        .then(res => refresh())
        .catch(err => handleError(err))
    else
      axios.post(`${urlApi}/todo`, item)
        .then(res => refresh())
        .catch(err => handleError(err))
  }

  function toEdit(item) {
    setEditItem(item)
    setDescription(item.description)
  }

  function remove(id) {
    const item = { description }
    axios.delete(`${urlApi}/todo/${id}`, item)
      .then(res => refresh())
      .catch(err => handleError(err))
  }

  function handleError(err) {
    console.log(err)
    setError(err.response.data)
    setTimeout(() => setError(''), 2000)
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

export default App;
