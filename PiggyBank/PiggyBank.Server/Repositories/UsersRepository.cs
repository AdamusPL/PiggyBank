using PiggyBank.Server.Models;

namespace PiggyBank.Server.Repositories
{
    internal interface IUsersRepository
    {
        Users GetUser(string username, string password);
        void AddRoomUser(RoomUser roomUser);
        void AddUser(Users user);
    }
    internal class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _dbContext;

        public UsersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Users GetUser(string username, string password)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Username == username);
        }

        public void AddRoomUser(RoomUser roomUser)
        {
            _dbContext.RoomUser.Add(roomUser);
            _dbContext.SaveChanges();
        }

        public void AddUser(Users user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
