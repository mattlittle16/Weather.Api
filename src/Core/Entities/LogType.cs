using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Enums;

namespace Core.Entities;

public class LogType : Base
{
    [Required]
    public LogTypeEnum Type { get; set; }
}

