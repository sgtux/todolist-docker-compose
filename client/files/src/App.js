import React, { useState, useEffect } from 'react';
import './App.css';
import axios from 'axios'
import { Login } from './Login/Login'
import { TodoList } from './TodoList/TodoList'

function App() {

  const [token, setToken] = useState('')
  const [urlApi, setUrlApi] = useState('')

  useEffect(() => {
    axios.get('/apihost').then(res => setUrlApi(`${res.data}/api`))
    const t = localStorage.getItem('API_TOKEN')
    if (t)
      tokenChanged(t)
  }, [])

  function tokenChanged(token) {
    token ? localStorage.setItem('API_TOKEN', token) : localStorage.removeItem('API_TOKEN')
    setToken(token)
  }

  return (
    urlApi ? token ?
      <TodoList tokenChanged={token => tokenChanged(token)} urlApi={urlApi} token={token} /> :
      <Login tokenChanged={token => tokenChanged(token)} urlApi={urlApi} />
      : null
  )
}

export default App;