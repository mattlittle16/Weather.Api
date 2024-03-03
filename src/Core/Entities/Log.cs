using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Etities;

public class Log 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid LogId { get; set; }
    [Required]
    public Guid LogTypeId { get; set; }
    [Required]
    public string Message { get; set; }
    [Required]
    public DateTimeOffset CreatedDate { get; set; }

    [ForeignKey("LogTypeId")]
    public LogType LogType { get; set;}
}

