import logo from '/images/logo.png';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

export function Register() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [surname, setSurname] = useState('');
    const [info, setInfo] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(`users/RegisterUser?username=${username}&password=${password}&firstName=${firstName}&surname=${surname}`,
                {
                    method: 'POST'
                });
            if (response.ok) {
                setInfo("User registered successfully");
            } else {
                setInfo("User with that username already exists");
            }
        } catch (error) {
            setInfo(error);
        }
    }

    return (
        <>
            <main className="form-signin w-100 m-auto">
                <form onSubmit={handleSubmit}>
                    <img className="mb-4" src={logo} alt="" width="72" height="57" />
                    <h1 className="h3 mb-3 fw-normal">Create an account</h1>
                    <div className="form-floating">
                        <input className="form-control" id="floatingInput" placeholder="name@example.com" value={firstName} onChange={(e) => setFirstName(e.target.value)} />
                        <label htmlFor="floatingInput">First Name</label>
                    </div>
                    <div className="form-floating">
                        <input className="form-control" id="floatingInput" placeholder="name@example.com" value={surname} onChange={(e) => setSurname(e.target.value)} />
                        <label htmlFor="floatingInput">Surname</label>
                    </div>
                    <div className="form-floating">
                        <input className="form-control" id="floatingInput" placeholder="name@example.com" value={username} onChange={(e) => setUsername(e.target.value)} />
                        <label htmlFor="floatingInput">Username</label>
                    </div>
                    <div className="form-floating">
                        <input type="password" className="form-control" id="floatingPassword" placeholder="Password" onChange={(e) => setPassword(e.target.value)} />
                        <label htmlFor="floatingPassword">Password</label>
                    </div>
                    <button className="btn btn-primary w-100 py-2" type="submit">Register</button>
                    <p id="error">{info}</p>
                    <p className="mt-5 mb-3 text-body-secondary">&copy; 2024</p>
                </form>
            </main>
            <script src="../assets/dist/js/bootstrap.bundle.min.js"></script>
        </>
    )
}

export default Register;