namespace BulletinBoard.Models;

public interface IDbContext {
    public User? UserExists(User user);
    public bool UserNameExists(string name);
    public bool CreateUser(User user);
    public Task<List<Post>> GetAllPostsAsync();
    public Task<List<PostWithDisplayName>> GetAllPostsWithDisplayNamesAsync();
    public Task<List<User>> GetAllUsersAsync();
    public User? GetUserById(int id);
    public bool CreatePost(Post post);
    public bool UpdateUser(User user);
}