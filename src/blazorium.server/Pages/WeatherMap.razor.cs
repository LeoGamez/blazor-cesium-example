using blazorium.Server.Services;
using blazorium.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace blazorium.server.Pages
{
    public partial class WeatherMap
    {
        private List<WeatherPoint> weatherPoints = new List<WeatherPoint>();

        [Inject]
        private IJSRuntime js { get; set; }

        [Inject]
        private WeatherService weatherService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await js.InvokeVoidAsync("createViewer");
                weatherService.SetCallBack(LoadPoints);
            }
        }

        public async Task LoadPoints(List<WeatherPoint> points)
        {
            weatherPoints = points;
            await js.InvokeVoidAsync("removeAllEntities");

            if (weatherPoints != null)
            {
                foreach (var p in points.ToArray())
                {
                    await js.InvokeVoidAsync("addPoint", p);
                }

            }
            await InvokeAsync(StateHasChanged);
        }
    }
}
