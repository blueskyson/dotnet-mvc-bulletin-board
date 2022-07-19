namespace BulletinBoard.Models;

public interface IDbContext {
    public User? userExists(User user);
}