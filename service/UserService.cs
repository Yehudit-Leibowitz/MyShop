using Entity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System.Text.Json;

namespace service
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);

        }

        public async Task<User> AddUser(User user)
        {
            return (CheckPassword(user.Password) > 2)?  await userRepository.AddUser(user): null;


        }
        public async Task<User> UpdateUser(int id, User userToUpdate)
        {
            return (CheckPassword(userToUpdate.Password) > 2) ?
                await userRepository.UpdateUser(id, userToUpdate) : null;


        }


        public async Task<User> LogIn(string userName, string password)
        {
            return await userRepository.LogIn(userName, password);
        }
        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);
            return result.Score;
        }
    }
}
