using AutoMapper;
using Grpc.Core;
using MediaLibrary.Contracts;
using MediaLibrary.Server.Services;
using MediaLibrary.Shared.Model;

namespace MediaLibrary.Server.Contracts;

public class PersonContractService : PersonContract.PersonContractBase, IContractService<Person>
{
    private readonly PersonService _personService;
    private readonly IMapper _mapper;

    public PersonContractService(PersonService personService, IMapper mapper)
    {
        _personService = personService;
        _mapper = mapper;
    }
    public override async Task<CreateResponse> Create(Person request, ServerCallContext context)
    {
        var model = _mapper.Map<PersonModel>(request);
        var created = await _personService.CreateAsync(model);
        return new CreateResponse { Id = created.Id, Path = $"/persons/{created.Id}" };
    }

    public override async Task<GenericResponse> Delete(ItemRequest request, ServerCallContext context)
    {
        await _personService.DeleteAsync(request.Id);
        return new GenericResponse { Success = true };
    }

    public override async Task<Person> Get(ItemRequest request, ServerCallContext context)
    {
        var model = await _personService.GetByIdAsync(request.Id);
        return _mapper.Map<Person>(model);
    }

    public override async Task GetList(Empty request, IServerStreamWriter<Person> responseStream, ServerCallContext context)
    {
        var data = await _personService.GetAllAsync();
        foreach (var item in data)
        {
            var model = _mapper.Map<Person>(item);
            await responseStream.WriteAsync(model);
        }
    }

    public override async Task<GenericResponse> Update(Person request, ServerCallContext context)
    {
        var model = _mapper.Map<PersonModel>(request);
        await _personService.UpdateAsync(request.Id, model);
        return new GenericResponse { Success = true };
    }
}
