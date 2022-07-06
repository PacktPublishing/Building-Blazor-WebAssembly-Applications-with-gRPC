using AutoMapper;
using Grpc.Core;
using MediaLibrary.Contracts;
using MediaLibrary.Server.Services;
using MediaLibrary.Shared.Model;

namespace MediaLibrary.Server.Contracts;
public class MovieContractService : MovieContract.MovieContractBase, IContractService<Movie>
{
    private readonly MovieService _movieService;
    private readonly IMapper _mapper;

    public MovieContractService(MovieService movieService, IMapper mapper)
    {
        _movieService = movieService;
        _mapper = mapper;
    }

    public override async Task<CreateResponse> Create(Movie request, ServerCallContext context)
    {
        var model = _mapper.Map<MovieModel>(request);
        var created = await _movieService.CreateAsync(model);
        return new CreateResponse { Id = created.Id, Path = $"/movies/{created.Id}" };
    }

    public override async Task<GenericResponse> Delete(ItemRequest request, ServerCallContext context)
    {
        await _movieService.DeleteAsync(request.Id);
        return new GenericResponse { Success = true };
    }

    public override async Task<Movie> Get(ItemRequest request, ServerCallContext context)
    {
        var model = await _movieService.GetByIdAsync(request.Id);
        return _mapper.Map<Movie>(model);
    }

    public override async Task GetList(Empty request, IServerStreamWriter<Movie> responseStream, ServerCallContext context)
    {
        var data = await _movieService.GetAllAsync();
        foreach (var item in data)
        {
            var model = _mapper.Map<Movie>(item);
            await responseStream.WriteAsync(model);
        }
    }

    public override async Task<GenericResponse> Update(Movie request, ServerCallContext context)
    {
        var model = _mapper.Map<MovieModel>(request);
        await _movieService.UpdateAsync(request.Id, model);
        return new GenericResponse { Success = true };
    }
}
