using BethanysPieShopHRM.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.Components.Pages;

public partial class EmployeeDetail
{
    [Parameter]
    public int EmployeeId { get; set; }

    public Employee Employee { get; set; } = new Employee();

    protected override void OnInitialized()
    {
        Employee = MockDataService.Employees.FirstOrDefault(e => e.EmployeeId == EmployeeId) ?? new Employee();
    }

    private void ChangeHolidayState()
    {
        Employee.IsOnHoliday = !Employee.IsOnHoliday;
    }
}