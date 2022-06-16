using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using MediaLibrary.Client;
using MediaLibrary.Client.Shared;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using MediaLibrary.Client.Shared.Model;
using Grpc.Core;
using MediaLibrary.Contracts;

namespace MediaLibrary.Client.Shared
{
    public partial class GrpcDataView<TItem, TContractClient>
    where TItem : class, MediaLibrary.Shared.Model.IModel, new()
        where TContractClient : ClientBase<TContractClient>, IContractClient<TItem>
    {
        [Inject]
        public NavigationManager Navigation { get; set; } = null!;

        /// <summary>
        /// Required service to get data.
        /// </summary>
        [EditorRequired]
        [Parameter]
        public TContractClient Service { get; set; } = null!;

        public Table<TItem> Data { get; set; } = new Table<TItem>();

        protected override async Task OnInitializedAsync()
        {
            var type = typeof(TItem);
            Data.Columns = type.GetProperties().Select(x => new TableColumn { Name = x.Name, PropertyInfo = x });

            var stream = Service.GetList(new Empty()).ResponseStream;

            while (await stream.MoveNext(default))
            {
                var item = stream.Current;
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