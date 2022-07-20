namespace BulletinBoard.Models;

public interface IDbContext {
    public User? UserExists(User user);
    public bool UserNameExists(string name);
    public bool CreateUser(User user);
    public Task<List<Post>> GetAllPostsAsync();
    public Task<List<User>> GetAllUsersAsync();

    public bool CreatePost(Post post);
}