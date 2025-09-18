using BethanysPieShopHRM.Shared.Domain;
using System.Net.Http.Json;

namespace BethanysPieShopHRM.Client.Services;

public class ClientEmployeeDataService : IEmployeeDataService
{
    private readonly HttpClient httpClient;

    public ClientEmployeeDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
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
        var list = await httpClient.GetFromJsonAsync<IEnumerable<Employee>>("api/employee");
        return list ?? Enumerable.Empty<Employee>();
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