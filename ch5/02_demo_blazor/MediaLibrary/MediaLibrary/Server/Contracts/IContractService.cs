using Grpc.Core;
using MediaLibrary.Contracts;

namespace MediaLibrary.Server.Contracts;

public interface IContractService<T>
{
    Task<CreateResponse> Create(T request, ServerCallContext context);
    Task<GenericResponse> Delete(ItemRequest request, ServerCallContext context);
    Task<T> Get(ItemRequest request, ServerCallContext context);
    Task GetList(Empty request, IServerStreamWriter<T> responseStream, ServerCallContext context);
    Task<GenericResponse> Update(T request, ServerCallContext context);
}
