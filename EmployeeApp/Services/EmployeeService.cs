using EmployeeApp.Models;
using EmployeeApp.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Services;

public class EmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Employee> AddEmployeeAsync(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        try
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(employee)} could not be saved: {ex.Message}");
        }
    }

    public async Task<Employee> GetAsync(int id)
    {

        var res = _context.Employees
            .Include(e => e.Manager)
            .Include(e => e.Department);

        return  res.FirstOrDefault(e => e.EmployeeID == id);
    }

    public async Task<List<EmployeeResponse>> GetAsync()
    {
        var emps = await _context.Employees
            .Include(e => e.Manager)
            .Include(e => e.Department)
            .ToListAsync();

        return emps.Select(e => new EmployeeResponse
        {
            Id = e.EmployeeID,
            Name = e.EmployeeName,
            Salary = e.Salary,
            Department = e.Department.DepartmentName,
            Manager = e.Manager.EmployeeName
        }).ToList();
    }

    public async Task<Employee> EditEmployeeAsync(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        try
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(employee)} could not be saved: {ex.Message}");
        }
    }

    public async Task<Employee> Delete(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        try
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return employee;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(employee)} could not be saved: {ex.Message}");
        }
    }
}