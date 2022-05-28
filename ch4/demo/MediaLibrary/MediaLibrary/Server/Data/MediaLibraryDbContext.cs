using Microsoft.EntityFrameworkCore;

namespace MediaLibrary.Server.Data;
public class MediaLibraryDbContext : DbContext
{
    public MediaLibraryDbContext() { }

    public MediaLibraryDbContext(DbContextOptions<MediaLibraryDbContext> options) : base(options) { }

#nullable disable
    public DbSet<Person> Persons { get; set; }
    public DbSet<Movie> Movies { get; set; }
#nullable enable

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(b =>
        {
            b.Property(x => x.Name).IsRequired();
            b.Navigation(x => x.Movies).AutoInclude();
        });

        modelBuilder.Entity<Movie>(b =>
        {
            b.Property(x => x.Name).IsRequired();
            b.HasOne(x => x.Director).WithMany().HasForeignKey(x => x.DirectorId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.MusicComposer).WithMany().HasForeignKey(x => x.MusicComposerId).OnDelete(DeleteBehavior.Restrict);
            b.Navigation(x => x.Director).AutoInclude();
            b.Navigation(x => x.MusicComposer).AutoInclude();
            b.Navigation(x => x.Actors).AutoInclude();
        });

        modelBuilder.Entity<MovieCategory>(b =>
        {
            b.HasKey(x => new { x.Category, x.MovieId });
        });

        modelBuilder.Entity<MovieActor>(b =>
        {
            b.HasKey(x => new { x.MovieId, x.PersonId });
        });
    }
}