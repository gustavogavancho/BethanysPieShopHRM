using BethanysPieShopHRM.Services;
using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.Components.Pages;

public partial class EmployeeOverview
{
    public List<Employee> Employees { get; set; } = default!;
    private Employee? _selectedEmployee;

    protected override async Task OnInitializedAsync()
    {
        Employees = MockDataService.Employees!;
    }

    private void ShowQuickViewPopup(Employee selectedEmployeee)
    {
        _selectedEmployee = selectedEmployeee;
    }
}