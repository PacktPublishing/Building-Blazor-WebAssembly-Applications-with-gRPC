namespace MediaLibrary.Server.Data;

public class Movie : BaseEntity
{
    public List<MovieCategory> Categories { get; set; } = new List<MovieCategory>();
    public int Year { get; set; }
    public string? Description { get; set; }
    public Person? Director { get; set; }
    public int? DirectorId { get; set; }
    public Person? MusicComposer { get; set; }
    public int? MusicComposerId { get; set; }
    public List<MovieActor> Actors { get; set; } = new List<MovieActor>();
}
