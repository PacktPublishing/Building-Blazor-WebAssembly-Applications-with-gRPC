namespace MediaLibrary.Client.Shared.Model;
public class TableRow<TItem>
{
    public TableRow(TItem originValue)
    {
        OriginValue = originValue;
    }

    public List<TableCell> Values { get; set; } = new();
    public TItem OriginValue { get; set; }
}
