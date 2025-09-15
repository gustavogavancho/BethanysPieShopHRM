using BethanysPieShopHRM.Contracts.Services;
using BethanysPieShopHRM.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.Components.Pages;

public partial class EmployeeOverview
{
    public List<Employee> Employees { get; set; } = default!;
    private Employee? _selectedEmployee;

    private string Title = "Employee overwiew";

    [Inject]
    public IEmployeeDataService EmployeeDataService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
    }

    private void ShowQuickViewPopup(Employee selectedEmployeee)
    {
        _selectedEmployee = selectedEmployeee;
    }
}