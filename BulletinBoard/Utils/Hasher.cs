using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BulletinBoard.Utils;

public class Hasher : IHasher
{
    public string GenerateSaltBase64()
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        return Convert.ToBase64String(salt);
    }

    public string GenerateHashBase64(string password, string saltBase64)
    {
        byte[] salt = Convert.FromBase64String(saltBase64);
        byte[] hashed = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8
        );
        return Convert.ToBase64String(hashed);
    }
}