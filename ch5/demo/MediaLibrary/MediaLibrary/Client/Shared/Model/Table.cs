namespace MediaLibrary.Client.Shared.Model;
public class Table<TItem>
{
    public IEnumerable<TableColumn> Columns { get; set; } = new List<TableColumn>();
    public List<TableRow<TItem>> Rows { get; set; } = new();
}
