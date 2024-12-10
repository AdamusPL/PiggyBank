using PiggyBank.Models;
using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;
using PiggyBank.Server.Repositories;

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
            _roomsRepository.CreateRoom(room);
        }
    }
}
