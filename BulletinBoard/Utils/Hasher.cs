using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BulletinBoard.Utils;

/// <summary>
/// Hash password.
/// </summary>
public class Hasher : IHasher
{
    /// <summary>
    /// Generate a random salt.
    /// </summary>
    /// <returns>A base64 string of a salt.</returns>
    public string GenerateSaltBase64()
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        return Convert.ToBase64String(salt);
    }

    /// <summary>
    /// Generate a Pbkdf2 hash.
    /// </summary>
    /// <param name="password">Raw password.</param>
    /// <param name="saltBase64">Salt</param>
    /// <returns>Hashed password.</returns>
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