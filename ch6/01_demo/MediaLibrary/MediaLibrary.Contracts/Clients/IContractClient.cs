using Grpc.Core;

namespace MediaLibrary.Contracts;

public interface IContractClient<TItem>
    where TItem : class, new()
{
    AsyncUnaryCall<CreateResponse> CreateAsync(TItem request, Metadata? headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
    AsyncUnaryCall<GenericResponse> UpdateAsync(TItem request, Metadata? headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
    AsyncUnaryCall<TItem> GetAsync(ItemRequest request, Metadata? headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
    AsyncServerStreamingCall<TItem> GetList(Empty request, Metadata? headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
    AsyncUnaryCall<GenericResponse> DeleteAsync(ItemRequest request, Metadata? headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default);
}