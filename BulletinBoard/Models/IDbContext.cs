namespace BulletinBoard.Models;

public interface IDbContext {
    public User? userExists(User user);
    public bool userNameExists(string name);
}