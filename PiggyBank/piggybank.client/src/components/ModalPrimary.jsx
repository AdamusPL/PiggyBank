import { useState } from "react";

export default function ModalPrimary({ isJoined, room, setUserRooms }) {
    const [isOpened, setIsOpened] = useState(false);
    function closeModal() {
        setIsOpened(false);
    }

    async function joinRoom() {
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
                setUserRooms(prevUserRooms => [...prevUserRooms, { roomId: room.id }]);
                setIsOpened(true);
            } else {
                console.error('Failed to join room');
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }

    async function leaveRoom() {
        const roomUserId = JSON.parse(localStorage.getItem("user")).id
        const roomUser = {
            Id: roomUserId,
            FirstName: "Marek",
            Surname: "Lesny"
        };
        try {
            const response = await fetch('rooms/leave', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ RoomId: room.id, Room: room, RoomUserId: roomUserId, RoomUser: roomUser }),
            });

            if (response.ok) {
                setUserRooms(prevUserRooms => prevUserRooms.filter(userRoom => userRoom.roomId !== room.id));
                setIsOpened(true);
            } else {
                console.error('Failed to leave room');
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }

    return (<>
        {!isJoined ?
            <button
                className="btn btn-outline-secondary"
                type="button"
                onClick={joinRoom}>
                Join
            </button>
            :
            <button
                className="btn btn-outline-secondary"
                type="button"
                onClick={leaveRoom}>
                Leave
            </button>
        }
        <dialog id="modal" open={isOpened}>
            <div class="close-modal">
                <button class="btn btn-primary rounded-circle p-2 lh-1" type="button" onClick={closeModal}>
                    X
                    <span class="visually-hidden">Dismiss</span>
                </button>
            </div>
            {isJoined ?
                <p id="info">Successfully joined room</p>
                :
                <p id="info">Successfully left room</p>
            }
        </dialog>
    </>)
}