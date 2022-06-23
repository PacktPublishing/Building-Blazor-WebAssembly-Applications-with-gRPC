using Microsoft.AspNetCore.Components;
using MediaLibrary.Client.Shared.Model;
using Grpc.Core;
using MediaLibrary.Contracts;
using AutoMapper;

namespace MediaLibrary.Client.Shared
{
    public partial class GrpcDataView<TItem, TContractItem, TContractClient>
        where TItem : class, MediaLibrary.Shared.Model.IModel, new()
        where TContractItem : class, new()
        where TContractClient : ClientBase<TContractClient>, IContractClient<TContractItem>
    {
        [Inject]
        public NavigationManager Navigation { get; set; } = null!;

        [Inject]
        public TContractClient Service { get; set; } = null!;

        [Inject]
        public IMapper Mapper { get; set; } = null!;


        public Table<TItem> Data { get; set; } = new Table<TItem>();

        protected override async Task OnInitializedAsync()
        {
            var type = typeof(TItem);
            Data.Columns = type.GetProperties().Select(x => new TableColumn { Name = x.Name, PropertyInfo = x });

            var stream = Service.GetList(new Empty()).ResponseStream;

            while (await stream.MoveNext(default))
            {
                var item = Mapper.Map<TItem>(stream.Current);
                var row = new TableRow<TItem>(item);

                foreach (var column in Data.Columns)
                {
                    var value = column.PropertyInfo.GetValue(item);
                    row.Values.Add(new TableCell { Value = value });
                }

                Data.Rows.Add(row);
            }
        }

        public string GetDetailUrl(int id)
            => $"{Navigation.ToAbsoluteUri(Navigation.Uri).LocalPath}/{id}";
    }
}