namespace MediaLibrary.Server.Data;

[UseCustomGenerator(false)]
public class Person : BaseEntity
{
    public DateTime BirthDay { get; set; }
    public string BirthPlace { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public List<MovieActor> Movies { get; set; } = new List<MovieActor>();
}
