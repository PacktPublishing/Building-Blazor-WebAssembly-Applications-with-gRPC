using AutoMapper;
using MediaLibrary.Server.Data;

namespace MediaLibrary.Server.Services;
public class PersonService : BaseService<Person, Shared.Model.PersonModel>
{
    public PersonService(MediaLibraryDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }
}
