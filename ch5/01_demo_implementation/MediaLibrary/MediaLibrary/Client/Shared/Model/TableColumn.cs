using System.Reflection;
namespace MediaLibrary.Client.Shared.Model;
public class TableColumn
{
    public string Name { get; set; } = string.Empty;
    public PropertyInfo PropertyInfo { get; set; } = null!;
}
