import { Routes, Route, BrowserRouter } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './pages/Home';
import { Expenses } from './pages/Expenses';
import { Login } from './pages/Login';
import { Register } from './pages/Register';
import { Rooms } from './pages/Rooms';
import { Profile } from './pages/Profile'
function App() {
    return (
        <>
            <BrowserRouter>
                <Layout>
                    <Routes>
                        <Route path='/' element={<Home />}></Route>
                        <Route path='expenses' element={<Expenses />}></Route>
                        <Route path='login' element={<Login />}></Route>
                        <Route path='register' element={<Register />}></Route>
                        <Route path='available-rooms' element={<Rooms />}></Route>
                        <Route path='profile' element={<Profile /> }></Route>
                    </Routes>
                </Layout>
            </BrowserRouter>
        </>
    )
}

export default App;