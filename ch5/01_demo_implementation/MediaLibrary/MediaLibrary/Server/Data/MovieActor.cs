namespace MediaLibrary.Server.Data;

public class MovieActor
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
    public int PersonId { get; set; }
    public Person Person { get; set; } = null!;
}
