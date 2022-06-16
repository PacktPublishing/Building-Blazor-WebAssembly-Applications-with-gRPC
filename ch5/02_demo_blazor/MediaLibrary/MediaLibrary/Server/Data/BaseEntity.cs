using System.ComponentModel.DataAnnotations;

namespace MediaLibrary.Server.Data;
public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
