using Isopoh.Cryptography.Argon2;
using System;
using System.Security.Cryptography;
using System.Text;

namespace TaskManager.Security
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password, string salt)
        {
            var config = new Argon2Config
            {
                Type = Argon2Type.HybridAddressing, // Argon2id
                Version = Argon2Version.Nineteen,
                TimeCost = 4,
                MemoryCost = 1 << 14,
                Lanes = 2,
                Threads = Environment.ProcessorCount,
                Salt = Encoding.UTF8.GetBytes(salt),
                Password = Encoding.UTF8.GetBytes(password),
                HashLength = 32
            };

            using var hasher = new Argon2(config);
            using var hashBytes = hasher.Hash();

            // Zet de hash (bytes) om naar een base64-string
            return Convert.ToBase64String(hashBytes.Buffer);
        }

        public static bool Verify(string hashedPassword, string password)
        {
            return Argon2.Verify(hashedPassword, password);
        }

        public static string GenerateSalt(int size = 16)
        {
            var salt = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }
}