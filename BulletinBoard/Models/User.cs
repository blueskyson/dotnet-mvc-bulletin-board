using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BulletinBoard.Models;

public class User {
    public int Id { get; set; }
    [StringLength(20, MinimumLength = 1)]
    [Required]
    public string? Name { get; set; }

    [StringLength(20, MinimumLength = 1)]
    public string? DisplayName { get; set; }

    [DataType(DataType.Date)]
    public DateTime RegisterDate { get; set; }
    
    [StringLength(20, MinimumLength = 1)]
    [Required]
    public string? Password { get; set; }
}