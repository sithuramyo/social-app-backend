using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DatabaseService.AppContextModels;

namespace DatabaseService.DataModels.Authentication;

[Table("Tbl_Users")]
public class Users : BaseDataModel
{
    [Key]
    public long Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? ProfileImagePath { get; set; }
}