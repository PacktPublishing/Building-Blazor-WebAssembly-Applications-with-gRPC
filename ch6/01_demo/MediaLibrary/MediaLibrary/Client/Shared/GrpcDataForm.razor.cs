using Microsoft.AspNetCore.Components;
using Grpc.Core;
using MediaLibrary.Contracts;
using AutoMapper;

namespace MediaLibrary.Client.Shared
{
    public partial class GrpcDataForm<TModel, TContractItem, TContractClient>
        where TModel : MediaLibrary.Shared.Model.IModel, new()
        where TContractItem : class, new()
        where TContractClient : ClientBase<TContractClient>, IContractClient<TContractItem>
    {
        [Inject]
        NavigationManager Navigation { get; set; } = null!;

        [Inject]
        public TContractClient Service { get; set; } = null!;

        [Inject]
        public IMapper Mapper { get; set; } = null!;

        [Parameter]
        [EditorRequired]
        public int Id { get; set; }

        [Parameter]
        public RenderFragment<TModel> ChildContent { get; set; } = null!;

        public TModel Model { get; set; } = new();
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await GetModel();
        }

        private async Task GetModel()
        {
            var grpcItem = Id > 0 ? await Service.GetAsync(new ItemRequest { Id = Id }) : new();
            Model = Mapper.Map<TModel>(grpcItem);
        }

        private async Task SaveItem()
        {
            var data = Mapper.Map<TContractItem>(Model);

            if (Id <= 0)
            {
                var response = await Service.CreateAsync(data);

                if (response.Id > 0)
                {
                    Navigation.NavigateTo(response.Path, replace: true);
                }
            }
            else
            {
                await Service.UpdateAsync(data);
            }
        }
    }
}