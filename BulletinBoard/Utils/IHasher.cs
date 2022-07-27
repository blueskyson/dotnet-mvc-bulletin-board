namespace BulletinBoard.Utils;

/// <summary>
/// Hash password.
/// </summary>
public interface IHasher {
    /// <summary>
    /// Generate a random salt.
    /// </summary>
    /// <returns>A base64 string of a salt.</returns>
    public string GenerateSaltBase64();

    /// <summary>
    /// Generate a Pbkdf2 hash.
    /// </summary>
    /// <param name="password">Raw password.</param>
    /// <param name="saltBase64">Salt</param>
    /// <returns>Hashed password.</returns>
    public string GenerateHashBase64(string password, string saltBase64);
}