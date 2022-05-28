using MediaLibrary.Server.Services;

namespace MediaLibrary.Server.Controllers;

public class MovieController : BaseController<Shared.Model.MovieModel, Data.Movie, MovieService>
{
    public MovieController(MovieService service) : base(service, "/movies")
    {
    }
}
