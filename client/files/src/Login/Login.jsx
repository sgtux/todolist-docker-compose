import React, { useState } from 'react';
import axios from 'axios'

export function Login({ urlApi, tokenChanged }) {

    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')
    const [loading, setLoading] = useState(false)

    function login() {
        if (loading)
            return
        setLoading(true)
        axios.post(`${urlApi}/token`, { email, password })
            .then(res => {
                setLoading(false)
                tokenChanged(res.data.token)
            })
            .catch(err => {
                setLoading(false)
                handleError(err)
            })
    }

    function handleError(err) {
        setError((err.response || {}).data || err.toJSON().message)
        setTimeout(() => setError(''), 2000)
    }

    return (
        <div className="App">
            <header className="App-header">
                <h2>LOGIN</h2>
                <form onSubmit={e => { e.preventDefault(); login() }} style={{ marginBottom: 40 }} on>
                    <input value={email} onChange={e => setEmail(e.target.value)} placeholder="Email" />
                    <br />
                    <input value={password} onChange={e => setPassword(e.target.value)} placeholder="Password" type="password" />
                    <br />
                    <button style={{ marginLeft: 5 }} onClick={() => login()} className="btn btn-cancel">LOGIN</button>
                    <div>
                        <span hidden={!error} className="error-message">{error}</span>
                    </div>
                </form>
            </header>
        </div>
    )
}