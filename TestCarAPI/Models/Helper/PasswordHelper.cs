using System.Security.Cryptography;

namespace TestCarAPI.Models.Helper
{
    public static class PasswordHelper
    {
        public static bool VerifyPassword(string enteredPassword, string hash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
            return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == hash;
        }
    }
}
