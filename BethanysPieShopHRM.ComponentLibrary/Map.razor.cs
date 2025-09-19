using BethanysPieShopHRM.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BethanysPieShopHRM.ComponentLibrary;

public partial class Map
{
    string elementId = $"map-{Guid.NewGuid():D}";

    [Parameter]
    public double Zoom { get; set; }

    [Parameter]
    public List<Marker> Markers { get; set; } = new();

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        // Ensure JS is called with a non-null markers array
        await JSRuntime.InvokeVoidAsync("deliveryMap.showOrUpdate", elementId, Markers ?? []);
    }
}