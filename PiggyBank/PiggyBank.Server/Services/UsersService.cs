using PiggyBank.Server.Dtos;
using PiggyBank.Server.Repositories;

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

        public UsersDto GetUser(string username, string password) {
            return _usersRepository.GetUser(username, password);
        }

        public bool RegisterUser(string username, string password, string firstName, string surname)
        {
            return _usersRepository.RegisterUser(username, password, firstName, surname);
        }
    }
}
