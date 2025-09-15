using BethanysPieShopHRM.Contracts.Repositories;
using BethanysPieShopHRM.Contracts.Services;
using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.Services;

public class EmployeeDataService : IEmployeeDataService
{
    private readonly IEmployeeRepository employeeRepository;

    public EmployeeDataService(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        return await employeeRepository.GetAllEmployees();
    }

    public async Task<Employee> GetEmployeeDetails(int employeeId)
    {
        return await employeeRepository.GetEmployeeById(employeeId);
    }
}