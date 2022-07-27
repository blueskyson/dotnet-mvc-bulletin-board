using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Models.Entities;

/// <summary>
/// Reply Entity.
/// The UserId and PostId are foreign keys. The User and Post are navigation properties.
/// </summary>
public class Reply
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }

    [DataType(DataType.Date)]
    public DateTime SubmitTime { get; set; }
    public string? Text { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [ForeignKey("PostId")]
    public Post? Post { get; set; }
}