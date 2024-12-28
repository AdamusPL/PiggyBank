using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;
using PiggyBank.Server.Repositories;
using PiggyBank.Server.Utils;

namespace PiggyBank.Server.Services
{
    public interface IRoomsService
    {
        List<Room> GetRooms();
        void JoinRoom(RoomOperationDto roomOperationDto);
        void LeaveRoom(RoomOperationDto roomOperationDto);
        List<Room_RoomUser> GetUserRooms(int userId);
        void CreateRoom(NewRoomDto room);
    }
    internal class RoomsService : IRoomsService
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IUsersRepository _usersRepository;

        public RoomsService(IRoomsRepository roomsRepository, IUsersRepository usersRepository)
        {
            _roomsRepository = roomsRepository;
            _usersRepository = usersRepository;
        }

        public List<Room> GetRooms()
        {
            return _roomsRepository.GetRooms();
        }

        public void JoinRoom(RoomOperationDto roomOperationDto)
        {
            RoomUser roomUser = _roomsRepository.GetRoomUser(roomOperationDto.UserId);
            var roomRoomUser = new Room_RoomUser
            {
                RoomId = roomOperationDto.RoomId,
                RoomUserId = roomUser.Id
            };
            _roomsRepository.JoinRoom(roomRoomUser);
        }

        public void LeaveRoom(RoomOperationDto roomOperationDto)
        {
            RoomUser roomUser = _roomsRepository.GetRoomUser(roomOperationDto.UserId);
            var roomRoomUser = new Room_RoomUser
            {
                RoomId = roomOperationDto.RoomId,
                RoomUserId = roomUser.Id
            };

            _roomsRepository.LeaveRoom(roomRoomUser);
        }

        public List<Room_RoomUser> GetUserRooms(int userId)
        {
            RoomUser roomUser = _roomsRepository.GetRoomUser(userId);
            return _roomsRepository.GetUserRooms(roomUser.Id);
        }

        public void CreateRoom(NewRoomDto room)
        {
            Room room1;
            if (room.Password == "")
            {
                room1 = new Room() {
                    Name = room.Name,
                    Password = null
                };
            }
            else
            {
                byte[] salt = PasswordManager.GenerateSalt();
                string hashedPassword = PasswordManager.HashPassword(room.Password, salt);
                room1 = new Room()
                {
                    Name = room.Name,
                    Password = hashedPassword,
                    Salt = Convert.ToBase64String(salt)
                };
            }
            _roomsRepository.CreateRoom(room1);
        }
    }
}
