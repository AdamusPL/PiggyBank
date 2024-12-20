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
        void CreateRoom(Room room);
    }
    internal class RoomsService : IRoomsService
    {
        private readonly IRoomsRepository _roomsRepository;

        public RoomsService(IRoomsRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }

        public List<Room> GetRooms()
        {
            return _roomsRepository.GetRooms();
        }

        public void JoinRoom(RoomOperationDto roomOperationDto)
        {
            _roomsRepository.JoinRoom(roomOperationDto);
        }

        public void LeaveRoom(RoomOperationDto roomOperationDto)
        {
            _roomsRepository.LeaveRoom(roomOperationDto);
        }

        public List<Room_RoomUser> GetUserRooms(int userId)
        {
            return _roomsRepository.GetUserRooms(userId);
        }

        public void CreateRoom(Room room)
        {
            if (room.Password == "")
            {
                room.Password = null;
            }
            else
            {
                byte[] salt = PasswordManager.GenerateSalt();
                string hashedPassword = PasswordManager.HashPassword(room.Password, salt);
                room.Password = hashedPassword;
            }
            _roomsRepository.CreateRoom(room);
        }
    }
}
