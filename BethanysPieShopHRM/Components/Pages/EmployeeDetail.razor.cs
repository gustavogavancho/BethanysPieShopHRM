using BethanysPieShopHRM.Client.Services;
using BethanysPieShopHRM.Contracts.Services;
using BethanysPieShopHRM.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BethanysPieShopHRM.Components.Pages;

public partial class EmployeeDetail
{
    [Parameter]
    public int EmployeeId { get; set; }

    [Inject]
    public IEmployeeDataService? EmployeeDataService { get; set; }

    [Inject]
    public ITimeRegistrationDataService? TimeRegistrationDataService { get; set; }

    public List<TimeRegistration> TimeRegistrations { get; set; } = [];

    public Employee Employee { get; set; } = new Employee();
    private float itemHeight = 50;
    protected IQueryable<TimeRegistration>? itemsQueryable;
    protected int queryableCount = 0;
    public PaginationState pagination = new() { ItemsPerPage = 10 };

    protected async override Task OnInitializedAsync()
    {
        Employee = await EmployeeDataService.GetEmployeeDetails(EmployeeId);
        //TimeRegistrations = await TimeRegistrationDataService.GetTimeRegistrationsForEmployee(EmployeeId);
        itemsQueryable = (await TimeRegistrationDataService.GetTimeRegistrationsForEmployee(EmployeeId)).AsQueryable();
        queryableCount = itemsQueryable.Count();

    }

    private void ChangeHolidayState()
    {
        Employee.IsOnHoliday = !Employee.IsOnHoliday;
    }

    public async ValueTask<ItemsProviderResult<TimeRegistration>> LoadTimeRegistrations(ItemsProviderRequest request)
    {
        int totalNumberOfTimeRegistrations = await TimeRegistrationDataService.GetTimeRegistrationCountForEmployeeId(EmployeeId);

        var numberOfTimeRegistrations = Math.Min(request.Count, totalNumberOfTimeRegistrations - request.StartIndex);
        var listItems = await TimeRegistrationDataService.GetPagedTimeRegistrationsForEmployee(EmployeeId, numberOfTimeRegistrations, request.StartIndex);

        return new ItemsProviderResult<TimeRegistration>(listItems, totalNumberOfTimeRegistrations);
    }
}