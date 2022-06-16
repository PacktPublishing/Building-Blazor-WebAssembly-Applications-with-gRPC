using MediaLibrary.Server.Services;

namespace MediaLibrary.Server.Controllers;

public class PersonController : BaseController<Shared.Model.PersonModel, Data.Person, PersonService>
{
    public PersonController(PersonService service) : base(service, "/persons")
    {
    }
}