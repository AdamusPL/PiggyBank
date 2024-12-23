using Microsoft.EntityFrameworkCore;
using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;

namespace PiggyBank.Server.Repositories
{
    internal interface IRoomsRepository
    {
        List<Room> GetRooms();
        void JoinRoom(Room_RoomUser roomRoomUser);
        void LeaveRoom(Room_RoomUser roomRoomUser);
        List<Room_RoomUser> GetUserRooms(int userId);
        void CreateRoom(Room room);
        List<Room> GetRoom(int roomUserId);
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

        public void JoinRoom(Room_RoomUser roomRoomUser)
        {
            _dbContext.Room_RoomUser.Add(roomRoomUser);
            _dbContext.SaveChanges();
        }

        public void LeaveRoom(Room_RoomUser roomRoomUser)
        {
            _dbContext.Room_RoomUser.Remove(roomRoomUser);
            _dbContext.SaveChanges();
        }

        public List<Room> GetRoom(int roomUserId)
        {
            return _dbContext.Room
                .Where(r => r.Room_RoomUsers.Any(ru => ru.RoomUserId == roomUserId))
                .Include(r => r.Expenses)
                    .ThenInclude(e => e.Items)
                .ToList();
        }
    }
}
