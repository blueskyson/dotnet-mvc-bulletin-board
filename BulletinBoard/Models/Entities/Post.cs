using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.Entities;

public class Post {
    public int Id { get; set; }
    public int UserId { get; set; }

    [DataType(DataType.Date)]
    public DateTime SubmitTime { get; set; }
    public string? Text { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}