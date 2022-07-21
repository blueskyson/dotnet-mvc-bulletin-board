using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Models;

public class ReplyWithDisplayName {
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime SubmitTime { get; set; }
    public string? Text { get; set; }
    public string? DisplayName { get; set; }
}