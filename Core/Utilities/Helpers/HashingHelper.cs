using System.Security.Cryptography;
using System.Text;

namespace Library.Core.Utilities.Helpers
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);

        }

        public static string stringToHash(string value)
        {
            using var myHash = SHA256.Create();

            var byteArray = Encoding.UTF8.GetBytes(value);

            var byteArrayResult = myHash.ComputeHash(byteArray);
            return string.Concat(Array.ConvertAll(byteArrayResult, h => h.ToString("X2")));
        }

    }
}
