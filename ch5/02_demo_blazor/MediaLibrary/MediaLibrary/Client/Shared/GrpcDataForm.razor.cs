using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;

namespace MediaLibrary.Client.Shared
{
    public partial class GrpcDataForm<TModel>
        where TModel : MediaLibrary.Shared.Model.IModel, new()
    {
        [Inject]
        HttpClient Http { get; set; } = null!;

        [Inject]
        NavigationManager Navigation { get; set; } = null!;

        [Parameter]
        [EditorRequired]
        public string ApiPath { get; set; } = string.Empty;

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
            Model = await Http.GetFromJsonAsync<TModel>($"rest/{ApiPath}/{Id}") ?? new();
        }

        private async Task SaveItem()
        {
            HttpResponseMessage response = Id <= 0 ?
                await Http.PostAsJsonAsync($"rest/{ApiPath}", Model) :
                await Http.PutAsJsonAsync($"rest/{ApiPath}/{Id}", Model);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    if (response.Headers.TryGetValues("location", out var urls))
                    {
                        Navigation.NavigateTo(urls.First(), replace: true);
                    }
                }

                await GetModel();
            }
            else
            {
                _errorMessage = await response.Content.ReadAsStringAsync();
            }
        }
    }
}