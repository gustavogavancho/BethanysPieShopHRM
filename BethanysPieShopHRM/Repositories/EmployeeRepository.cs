using BethanysPieShopHRM.Contracts.Repositories;
using BethanysPieShopHRM.Data;
using BethanysPieShopHRM.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopHRM.Repositories;

public class EmployeeRepository : IEmployeeRepository, IDisposable
{
    private readonly AppDbContext _appDbContext;

    public EmployeeRepository(IDbContextFactory<AppDbContext> DbFactory)
    {
        _appDbContext = DbFactory.CreateDbContext();
    }

    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        //return await Task.FromResult(_appDbContext.Employees);
        return await _appDbContext.Employees.ToListAsync();
    }

    public async Task<Employee> GetEmployeeById(int employeeId)
    {
        return await _appDbContext.Employees.FirstOrDefaultAsync(c => c.EmployeeId == employeeId);
    }

    public void Dispose()
    {
        _appDbContext.Dispose();
    }
}