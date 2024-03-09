using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Log : Base
{
    [Required]
    public Guid LogTypeId { get; set; }
    [Required]
    public string Message { get; set; }
    [Required]
    public DateTimeOffset CreatedDate { get; set; }

    [ForeignKey("LogTypeId")]
    public LogType LogType { get; set;}
}

