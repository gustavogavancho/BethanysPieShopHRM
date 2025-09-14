using BethanysPieShopHRM.Services;
using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.Components.Pages;

public partial class EmployeeOverview
{
    public List<Employee> Employees { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        Employees = MockDataService.Employees!;
    }
}