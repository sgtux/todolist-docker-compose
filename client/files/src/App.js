import React, { useState, useEffect } from 'react';
import './App.css';
import axios from 'axios'
import { Login } from './Login/Login'
import { TodoList } from './TodoList/TodoList'

function App() {

  const [token, setToken] = useState('')
  const [urlApi, setUrlApi] = useState('')

  useEffect(async () => {
    const response = await axios.get('/apihost')
    setUrlApi(`http://${response.data}/api`)
  }, [])

  function tokenChanged(token) {
    setToken(token)
  }

  return (
    urlApi ? token ?
      <TodoList urlApi={urlApi} token={token} /> :
      <Login tokenChanged={token => tokenChanged(token)} urlApi={urlApi} />
      : null
  )
}

export default App;