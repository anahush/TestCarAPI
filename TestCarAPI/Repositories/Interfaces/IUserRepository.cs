using TestCarAPI.Models.User;

namespace TestCarAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserModel>> GetAllUsersAsync();
        public Task<UserModel> GetUserByLoginAndPasswordAsync(string username, string password);
    }
}
