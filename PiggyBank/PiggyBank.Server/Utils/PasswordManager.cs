using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Security.Cryptography;
using PiggyBank.Server.Models;

namespace PiggyBank.Server.Utils
{
    public class PasswordManager
    {
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public static string HashPassword(string password, byte[] salt)
        {
            var sha256 = new SHA256Managed();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

            byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
            Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
            Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

            return Convert.ToBase64String(hashedPasswordWithSalt);
        }

        public static string CheckPassword(string password, string salt)
        {
            string storedHashedPassword = password;
            byte[] storedSaltBytes = Convert.FromBase64String(salt);

            // Convert the stored salt and entered password to byte arrays
            // byte[] storedSaltBytes = Convert.FromBase64String(user.Salt);
            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(password);

            // Concatenate entered password and stored salt
            byte[] saltedPassword = new byte[enteredPasswordBytes.Length + storedSaltBytes.Length];
            Buffer.BlockCopy(enteredPasswordBytes, 0, saltedPassword, 0, enteredPasswordBytes.Length);
            Buffer.BlockCopy(storedSaltBytes, 0, saltedPassword, enteredPasswordBytes.Length, storedSaltBytes.Length);

            return HashPassword(password, storedSaltBytes);
        }
    }
}
