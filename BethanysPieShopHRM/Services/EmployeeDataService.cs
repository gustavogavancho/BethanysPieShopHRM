using BethanysPieShopHRM.Client;
using BethanysPieShopHRM.Contracts.Repositories;
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
        if (employee.ImageContent is { Length: > 0 } && !string.IsNullOrWhiteSpace(employee.ImageName))
        {
            // Determine a valid web root (handle cases where WebRootPath is null)
            var webRoot = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrWhiteSpace(webRoot))
            {
                webRoot = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot");
            }

            // Ensure uploads folder exists
            var uploadsFolder = Path.Combine(webRoot, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            // Use only the file name to prevent path traversal
            var safeFileName = Path.GetFileName(employee.ImageName);
            var filePath = Path.Combine(uploadsFolder, safeFileName);

            // Write the file safely
            await using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await fileStream.WriteAsync(employee.ImageContent, 0, employee.ImageContent.Length);
            }

            // Prefer a relative path that works regardless of scheme/host
            var relativePath = $"/uploads/{safeFileName}";

            // If HttpContext is available, you can build an absolute URL; otherwise, keep it relative
            var scheme = _httpContextAccessor.HttpContext?.Request?.Scheme;
            var host = _httpContextAccessor.HttpContext?.Request?.Host.Value;
            employee.ImageName = !string.IsNullOrEmpty(scheme) && !string.IsNullOrEmpty(host)
                ? $"{scheme}://{host}{relativePath}"
                : relativePath;
        }

        await employeeRepository.UpdateEmployee(employee);
    }
}