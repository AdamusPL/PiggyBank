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
        public void CreateRoom(Room room)
        {
            using (var dbContext = new DbContext())
            {
                if(room.Password == "")
                {
                    room.Password = null;
                }
                else
                {
                    byte[] salt = PasswordManager.GenerateSalt();
                    string hashedPassword = PasswordManager.HashPassword(room.Password, salt);
                    room.Password = hashedPassword;
                }

                dbContext.Room.Add(room);
                dbContext.SaveChanges();
            }
        }

        public List<Room> GetRooms()
        {
            using (var dbContext = new DbContext())
            {
                return dbContext.Room.ToList();
            }
        }

        public List<Room_RoomUser> GetUserRooms(int userId)
        {
            using (var dbContext = new DbContext())
            {
                return dbContext.Room_RoomUser.FromSqlRaw("SELECT * FROM Room_RoomUser WHERE RoomUserId = {0}", userId).ToList();
            }
        }

        public void JoinRoom(RoomOperationDto roomOperationDto)
        {
            using (var dbContext = new DbContext())
            {
                dbContext.AddRoomUserToRoom(roomOperationDto.RoomId, roomOperationDto.RoomUserId);
            }
        }

        public void LeaveRoom(RoomOperationDto roomOperationDto)
        {
            using (var dbContext = new DbContext())
            {
                dbContext.RemoveRoomUserFromRoom(roomOperationDto.RoomId, roomOperationDto.RoomUserId);
            }
        }
    }
}
