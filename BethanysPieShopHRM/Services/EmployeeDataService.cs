using BethanysPieShopHRM.Contracts.Repositories;
using BethanysPieShopHRM.Contracts.Services;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.AspNetCore.Hosting;

namespace BethanysPieShopHRM.Services;

public class EmployeeDataService : IEmployeeDataService
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmployeeDataService(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        this.employeeRepository = employeeRepository;
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Employee> AddEmployee(Employee employee)
    {
        return await employeeRepository.AddEmployee(employee);
    }

    public async Task DeleteEmployee(int employeeId)
    {
        await employeeRepository.DeleteEmployee(employeeId);
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        return await employeeRepository.GetAllEmployees();
    }

    public async Task<Employee> GetEmployeeDetails(int employeeId)
    {
        return await employeeRepository.GetEmployeeById(employeeId);
    }

    public async Task UpdateEmployee(Employee employee)
    {
        if (employee.ImageContent != null)
        {
            string currentUrl = _httpContextAccessor.HttpContext.Request.Host.Value;
            var path = $"{_webHostEnvironment.WebRootPath}\\uploads\\{employee.ImageName}";
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(employee.ImageContent, 0, employee.ImageContent.Length);
            fileStream.Close();

            employee.ImageName = $"https://{currentUrl}/uploads/{employee.ImageName}";
        }

        await employeeRepository.UpdateEmployee(employee);
    }
}