import logo from '/images/logo.png';
import profilePicture from '/images/profile-picture.png';
import { Link, useNavigate } from 'react-router-dom';
import '../css/Layout.css';

export function Layout({ children }) {

    const navigate = useNavigate();

    const isLoggedIn = () => {
        return localStorage.getItem('user') !== null;
    };

    const handleSignOut = async () => {
        try {
            localStorage.removeItem('token');
            localStorage.removeItem('user');
            navigate('/');
        } catch (error) {
            console.error(error);
        }
    }

    return (
        <>
            <header className="p-3 mb-3 border-bottom">
                <div className="container">
                    <div className="d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start">
                        <Link to="/" className="d-flex align-items-center mb-2 mb-lg-0 link-body-emphasis text-decoration-none">
                            <img className="bi me-2" width="40" height="32" role="img" aria-label="Bootstrap" src={logo} />
                        </Link>

                        <ul className="nav col-12 col-lg-auto me-lg-auto mb-2 justify-content-center mb-md-0">
                            <Link to='/' className="nav-link px-2 link-secondary">Home page</Link>
                            {isLoggedIn() ? (
                                <>
                                    <Link to='/expenses' className="nav-link px-2 link-body-emphasis">Expenses</Link>
                                    <Link to='/available-rooms' className="nav-link px-2 link-body-emphasis">Rooms</Link>
                                </>
                            ) : (
                                <>
                                    <Link to='/login' className="nav-link px-2 link-body-emphasis">Login</Link>
                                    <Link to='/register' className="nav-link px-2 link-body-emphasis">Register</Link>
                                </>
                            )}
                        </ul>

                        <form className="col-12 col-lg-auto mb-3 mb-lg-0 me-lg-3" role="search">
                            <input type="search" className="form-control" placeholder="Search..." aria-label="Search" />
                        </form>

                        {isLoggedIn() && (<div className="dropdown text-end">
                            <Link to="#" className="d-block link-body-emphasis text-decoration-none dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
                                <img src={profilePicture} alt="mdo" width="32" height="32" className="rounded-circle" />
                            </Link>
                            <ul className="dropdown-menu text-small">
                                <Link to='/profile' className="dropdown-item">Profile</Link>
                                <li><hr className="dropdown-divider" /></li>
                                <li><Link className="dropdown-item" onClick={handleSignOut}>Sign out</Link></li>
                            </ul>
                        </div>)}
                    </div>
                </div>
            </header>
            <main className="container mt-5 pt-3">
                {children}
            </main>
        </>
    )
}

export default Layout;