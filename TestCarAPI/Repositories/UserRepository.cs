using Dapper;

using TestCarAPI.Context;
using TestCarAPI.Models.Helper;
using TestCarAPI.Models.User;
using TestCarAPI.Repositories.Interfaces;

namespace TestCarAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var query = @"SELECT * FROM Users";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<UserModel>(query);
                return users.ToList();
            }
        }

        public async Task<UserModel> GetUserByLoginAndPasswordAsync(string username, string password)
        {
            var query = $@"SELECT * FROM Users WHERE UserName = {username};";
            using (var connection = _context.CreateConnection())
            {
                var queryResult = await connection.QueryAsync<UserModel>(query);
                var user = queryResult.FirstOrDefault();

                if (user != null)
                {
                    var doesUserExist = PasswordHelper.VerifyPassword(password, user.PasswordHash, user.Salt);
                    if (doesUserExist)
                    {
                        return user;
                    }
                }

                return null;
            }
        }
    }
}
