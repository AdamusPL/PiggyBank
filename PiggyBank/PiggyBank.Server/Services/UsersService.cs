using PiggyBank.Server.Dtos;
using PiggyBank.Server.Models;
using PiggyBank.Server.Repositories;
using PiggyBank.Server.Utils;

namespace PiggyBank.Server.Services
{
    public interface IUsersService
    {
        UsersDto GetUser(string username, string password);
        bool RegisterUser(string username, string password, string firstName, string surname);
    }

    internal class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public UsersDto GetUser(string username, string password)
        {
            Users user = _usersRepository.GetUser(username, password);
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
            Users existingUser = _usersRepository.GetUser(username, password);

            if (existingUser == null)
            {
                RoomUser roomUser = new RoomUser
                {
                    FirstName = firstName,
                    Surname = surname
                };

                _usersRepository.AddRoomUser(roomUser);

                byte[] salt = PasswordManager.GenerateSalt();
                password = PasswordManager.HashPassword(password, salt);

                Users user = new Users
                {
                    Username = username,
                    Password = password,
                    Salt = Convert.ToBase64String(salt),
                    RoomUserId = roomUser.Id
                };

                _usersRepository.AddUser(user);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
