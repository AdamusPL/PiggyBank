using PiggyBank.Models;
using PiggyBank.Server.Models;
using System.Text;
using System.Security.Cryptography;
using System.Drawing;
using PiggyBank.Server.Dtos;
using PiggyBank.Server.Utils;

namespace PiggyBank.Server.Repositories
{
    internal interface IUsersRepository
    {
        UsersDto GetUser(string username, string password);
        bool RegisterUser(string username, string password, string firstName, string surname);
    }
    internal class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _dbContext;

        public UsersRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UsersDto GetUser(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                string enteredPasswordHash = PasswordManager.CheckPassword(password, user.Salt);
                if (user.Password == enteredPasswordHash)
                {
                    var userFinal = new UsersDto()
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Password = user.Password,
                        RoomUserId = user.RoomUserId,
                    };
                    return userFinal;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public bool RegisterUser(string username, string password, string firstName, string surname)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Username == username);
            if (existingUser == null)
            {
                RoomUser roomUser = new RoomUser
                {
                    FirstName = firstName,
                    Surname = surname
                };

                _dbContext.RoomUser.Add(roomUser);
                _dbContext.SaveChanges();

                byte[] salt = PasswordManager.GenerateSalt();
                password = PasswordManager.HashPassword(password, salt);

                Users user = new Users
                {
                    Username = username,
                    Password = password,
                    Salt = Convert.ToBase64String(salt),
                    RoomUserId = roomUser.Id
                };
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
