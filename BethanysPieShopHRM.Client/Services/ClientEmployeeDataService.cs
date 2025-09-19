using BethanysPieShopHRM.Client.Helper;
using BethanysPieShopHRM.Shared.Domain;
using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace BethanysPieShopHRM.Client.Services;

public class ClientEmployeeDataService : IEmployeeDataService
{
    private readonly HttpClient httpClient;
    private readonly ILocalStorageService localStorageService;

    public ClientEmployeeDataService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        this.httpClient = httpClient;
        this.localStorageService = localStorageService;
    }

    public Task<Employee> AddEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEmployee(int employeeId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        bool employeeExpirationExists = await localStorageService.ContainKeyAsync(LocalStorageConstants.EmployeesListExpirationKey);
        if (employeeExpirationExists)
        {
            DateTime employeeListExpiration = await localStorageService.GetItemAsync<DateTime>(LocalStorageConstants.EmployeesListExpirationKey);
            if (employeeListExpiration > DateTime.Now)//get from local storage
            {
                if (await localStorageService.ContainKeyAsync(LocalStorageConstants.EmployeesListKey))
                {
                    return await localStorageService.GetItemAsync<List<Employee>>(LocalStorageConstants.EmployeesListKey);
                }
            }
        }

        var list = await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>
                (await httpClient.GetStreamAsync("api/employee"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        await localStorageService.SetItemAsync(LocalStorageConstants.EmployeesListKey, list);
        await localStorageService.SetItemAsync(LocalStorageConstants.EmployeesListExpirationKey, DateTime.Now.AddMinutes(1));

        return list;
    }

    public Task<Employee> GetEmployeeDetails(int employeeId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }
}