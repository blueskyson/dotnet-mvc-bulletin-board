using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Models;

public class User {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? DisplayName { get; set; }

    [DataType(DataType.Date)]
    public DateTime RegisterDate { get; set; }
    public string? Password { get; set; }
}