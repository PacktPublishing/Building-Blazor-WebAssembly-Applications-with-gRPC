using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace MediaLibrary.Shared;
public class SharedMapperProfile : Profile
{
    public SharedMapperProfile()
    {
        CreateMap<DateTime, Timestamp>()
            .ConvertUsing(x => Timestamp.FromDateTime(DateTime.SpecifyKind(x, DateTimeKind.Utc)));

        CreateMap<Timestamp, DateTime>()
            .ConvertUsing(x => x.ToDateTime());

        CreateMap<Contracts.Person, Model.PersonModel>().ReverseMap();
        CreateMap<Contracts.Movie, Model.MovieModel>();
        CreateMap<Model.MovieModel, Contracts.Movie>()
            .ForMember(x => x.Categories, map => map.MapFrom(s => s.Categories));
    }
}
