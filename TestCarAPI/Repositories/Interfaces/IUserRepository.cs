using TestCarAPI.Models.User;
using TestCarAPI.Models.UserData;

namespace TestCarAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserModel>> GetAllUsersAsync();
        public Task<UserModel> GetUserByLoginAndPasswordAsync(string username, string password);
    }
}
