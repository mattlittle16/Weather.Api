using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Etities;

public class LogType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid LogTypeId { get; set; }
    [Required]
    public LogTypeEnum Type { get; set; }
}

