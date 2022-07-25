namespace BulletinBoard.Utils;

public interface IHasher {
    public string GenerateSaltBase64();
    public string GenerateHashBase64(string password, string saltBase64);
}