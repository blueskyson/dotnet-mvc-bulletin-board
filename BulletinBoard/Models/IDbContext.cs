namespace BulletinBoard.Models;

public interface IDbContext {
    public User? UserExists(User user);
    public bool UserNameExists(string name);
}