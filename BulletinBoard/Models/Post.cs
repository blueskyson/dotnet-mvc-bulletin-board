using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Models;

public class Post {
    public int Id { get; set; }
    public int UserId { get; set; }

    [DataType(DataType.Date)]
    public DateTime SubmitTime { get; set; }
    public string? Text { get; set; }
}