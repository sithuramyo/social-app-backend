using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseService.DataModels.OtpLogs;

[Table("Tbl_OtpLog")]
public class OtpLog
{
    [Key]
    public long Id { get; set; }
    public string Otp { get; set; }
    public string UserId { get; set; }
    public DateTime OtpExpires { get; set; }
}