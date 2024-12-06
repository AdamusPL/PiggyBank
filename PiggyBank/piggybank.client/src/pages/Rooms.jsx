import { useEffect, useState } from 'react';
import '../css/Rooms.css';
import ModalPassword from '../components/ModalPassword';
import ModalPrimary from '../components/ModalPrimary';

export function Rooms() {
    const [rooms, setRooms] = useState([]);
    const [userRooms, setUserRooms] = useState([]);
    const [roomName, setRoomName] = useState('');
    const [password, setPassword] = useState('');

    useEffect(() => {
        populateRoomsData();
        populateUserRoomsData();
    }, []);

    async function populateRoomsData() {
        const response = await fetch('rooms');
        const data = await response.json();
        setRooms(data);
    }

    async function populateUserRoomsData() {
        debugger;
        const userId = JSON.parse(localStorage.getItem('user')).id;
        const response = await fetch(`rooms/GetUserRooms?userId=${userId}`);
        const data = await response.json();
        setUserRooms(data);
        console.log(data);
    }

    async function handleCreateRoom() {
        const room = {
            name: roomName,
            password: password
        };

        try {
            const response = await fetch('rooms/create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(room),
            });

            if (response.ok) {
                console.log("Room created");
            } else {
                console.error('Failed to join room');
            }
        } catch (error) {
            console.error('Error:', error);
        }

    }

    return (
        <div>
            <h1 id="tableLabel">Available Rooms</h1>
            <table className="table table-striped" aria-labelledby="tableLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                    </tr>
                </thead>
                <tbody>
                    {rooms.map(room =>
                        <tr key={room.id}>
                            <td>{room.id}</td>
                            <td>{room.name}</td>
                            {userRooms.length > 0 && userRooms.some(userRoom => userRoom.roomId === room.id) ?
                                <ModalPrimary isJoined={true} room={room} userRooms={userRooms} setUserRooms={setUserRooms} />
                                :
                                room.password ?
                                    <ModalPassword room={room} setUserRooms={setUserRooms} />
                                    :
                                    <ModalPrimary isJoined={false} room={room} setUserRooms={setUserRooms} />
                            }
                        </tr>
                    )}
                </tbody>
            </table>
            <div className="new-expenses">
                <h3>Create new room</h3>
                <form className="expense-form" onSubmit={handleCreateRoom}>
                    <p>Name</p>
                    <input type="search" onChange={(e) => setRoomName(e.target.value)} />
                    <p>Password (leave empty if none)</p>
                    <input type="password" onChange={(e) => setPassword(e.target.value)} />
                    <button className="btn btn-outline-secondary" type="submit">Add</button>
                </form>
            </div>
        </div>
    );
}

export default Rooms;