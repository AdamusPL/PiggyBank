using Microsoft.EntityFrameworkCore;
using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;
using PiggyBank.Server.Utils;

namespace PiggyBank.Server.Repositories
{
    internal interface IRoomsRepository
    {
        List<Room> GetRooms();
        void JoinRoom(RoomOperationDto roomOperationDto);
        void LeaveRoom(RoomOperationDto roomOperationDto);
        List<Room_RoomUser> GetUserRooms(int userId);
        void CreateRoom(Room room);
    }

    internal class RoomsRepository : IRoomsRepository
    {
        private readonly AppDbContext _dbContext;

        public RoomsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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

            _dbContext.Room.Add(room);
            _dbContext.SaveChanges();

        }

        public List<Room> GetRooms()
        {
            return _dbContext.Room.ToList();
        }

        public List<Room_RoomUser> GetUserRooms(int userId)
        {
            return _dbContext.Room_RoomUser.FromSqlRaw("SELECT * FROM Room_RoomUser WHERE RoomUserId = {0}", userId).ToList();
        }

        public void JoinRoom(RoomOperationDto roomOperationDto)
        {
            _dbContext.AddRoomUserToRoom(roomOperationDto.RoomId, roomOperationDto.RoomUserId);
        }

        public void LeaveRoom(RoomOperationDto roomOperationDto)
        {
            _dbContext.RemoveRoomUserFromRoom(roomOperationDto.RoomId, roomOperationDto.RoomUserId);
        }
    }
}
