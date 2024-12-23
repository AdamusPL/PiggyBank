import { useState } from "react";

export default function ModalPassword({ room, setUserRooms }) {
    const [isOpened, setIsOpened] = useState(false);
    const [roomPassword, setRoomPassword] = useState("");
    const [info, setInfo] = useState("");

    async function checkPasswordToRoom() {
        if (room.password === roomPassword) {
            joinRoomWithPassword();
            setInfo("Successfully joined the room");
            setUserRooms(prevUserRooms => [...prevUserRooms, { roomId: room.id }]);
        }
        else {
            setInfo("Wrong password!");
        }
    }

    function closeModal() {
        ;
        setIsOpened(false);
    }

    function openModal() {
        setIsOpened(true);
    }

    async function joinRoomWithPassword() {
        const roomUserId = JSON.parse(localStorage.getItem("user")).id
        const roomUser = {
            Id: roomUserId,
            FirstName: "Marek",
            Surname: "Lesny"
        };
        try {
            const response = await fetch('rooms/join', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ RoomId: room.id, Room: room, RoomUserId: roomUserId, RoomUser: roomUser }),
            });

            if (response.ok) {
                setInfo("Successfully joined room: " + room.name);
            } else {
                setInfo('Failed to join room');
            }
        } catch (error) {
            setInfo('Error:', error);
        }
    }

    return (<>
        <button
            className="btn btn-outline-secondary"
            type="button"
            onClick={openModal}>
            Join
        </button>
        <dialog id="modal-password" open={isOpened}>
            <div className="close-modal">
                <button className="btn btn-primary rounded-circle p-2 lh-1" type="button" onClick={closeModal}>
                    X
                    <span className="visually-hidden">Dismiss</span>
                </button>
            </div>
            <p>Please enter password:</p>
            <input id="room-password" type="password" defaultValue={roomPassword} onChange={(e) => setRoomPassword(e.target.value)}></input>
            <div id="center">
                <button id="confirm-password" className="btn btn-outline-secondary" onClick={checkPasswordToRoom}>Enter</button>
            </div>
            <p>{info}</p>
        </dialog>
    </>)
}