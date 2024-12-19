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
        Users GetUser(string username, string password);
        bool RegisterUser(string username, string password, string firstName, string surname);
    }
    internal class UsersRepository : IUsersRepository
    {
        public Users GetUser(string username, string password)
        {
            using (var dbContext = new DbContext())
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    string enteredPasswordHash = PasswordManager.CheckPassword(password, user.Salt);
                    if (user.Password == enteredPasswordHash) {
                        var userFinal = new Users()
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
        }

        public bool RegisterUser(string username, string password, string firstName, string surname) 
        {
            using (var dbContext = new DbContext())
            {
                var existingUser = dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (existingUser == null)
                {
                    RoomUser roomUser = new RoomUser
                    {
                        FirstName = firstName,
                        Surname = surname
                    };

                    dbContext.RoomUser.Add(roomUser);
                    dbContext.SaveChanges();

                    byte[] salt = PasswordManager.GenerateSalt();
                    password = PasswordManager.HashPassword(password, salt);

                    UsersDto user = new UsersDto
                    {
                        Username = username,
                        Password = password,
                        Salt = Convert.ToBase64String(salt),
                        RoomUserId = roomUser.Id
                    };
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
