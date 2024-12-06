import logo from '/images/logo.png';
import { useNavigate } from 'react-router-dom';
import '../css/Login.css';
import { useState, useEffect } from 'react';

export function Login() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [info, setInfo] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(`users/GetUser?username=${username}&password=${password}`);
            if (response.ok) {
                const user = await response.json();
                localStorage.setItem('user', JSON.stringify(user));
                setInfo("");
                navigate('/');
            } else {
                setInfo("Invalid username or password");
            }
        } catch (error) {
            setInfo(error);
        }
    }

    useEffect(() => {
        const user = JSON.parse(localStorage.getItem('user'));
        if (user != null) {
            navigate('/');
        }
    }, []);

    return (
        <>
            <main className="form-signin w-100 m-auto">
                <form onSubmit={handleSubmit}>
                    <img className="mb-4" src={logo} alt="" width="72" height="57" />
                    <h1 className="h3 mb-3 fw-normal">Please sign in</h1>

                    <div className="form-floating">
                        <input className="form-control" id="floatingInput" placeholder="name@example.com" value={username} onChange={(e) => setUsername(e.target.value)} />
                        <label htmlFor="floatingInput">Username</label>
                    </div>
                    <div className="form-floating">
                        <input type="password" className="form-control" id="floatingPassword" placeholder="Password" onChange={(e) => setPassword(e.target.value)} />
                        <label htmlFor="floatingPassword">Password</label>
                    </div>

                    <div className="form-check text-start my-3">
                        <input className="form-check-input" type="checkbox" value="remember-me" id="flexCheckDefault" />
                        <label className="form-check-label" htmlFor="flexCheckDefault">
                            Remember me
                        </label>
                    </div>
                    <button className="btn btn-primary w-100 py-2" type="submit">Sign in</button>
                    <p id="error">{info}</p>
                    <p className="mt-5 mb-3 text-body-secondary">&copy; 2024</p>
                </form>
            </main>
            <script src="../assets/dist/js/bootstrap.bundle.min.js"></script>
        </>
    )
}

export default Login;