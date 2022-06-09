using EmployeeApp.Models;
using EmployeeApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Services;

public class DepartmentService
{
    private readonly ApplicationDbContext _context;

    public DepartmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Department> AddDepartmentAsync(Department department)
    {
        if (department == null)
        {
            throw new ArgumentNullException(nameof(Department));
        }

        try
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(department)} could not be saved: {ex.Message}");
        }
    }

    public async Task<Department> GetAsync(int id)
    {

        var res = _context.Departments
            .Include(e => e.Manager);

        return res.FirstOrDefault(e => e.DepartmentId == id);

    }

    public async Task<List<DepartmentResponse>> GetAsync()
    {
        var emps = await _context.Departments
            .Include(e => e.Manager)
            .ToListAsync();

        return emps.Select(e => new DepartmentResponse
        {
            Id = e.DepartmentId,
            Name = e.DepartmentName,
            Manager = e.Manager.EmployeeName
        }).ToList();
    }
    public async Task<List<DepartmentResponse>> FillSelectList ()
    {
        var emps = await _context.Departments
            .Include(e => e.Manager)
            .ToListAsync();

        return emps.Select(e => new DepartmentResponse
        {
            Id = e.DepartmentId,
            Name = e.DepartmentName,
            Manager = e.Manager.DepartmentName
        }).ToList();
    }

    public async Task<Department> EditDepartmentAsync(Department department)
    {
        if (department == null)
        {
            throw new ArgumentNullException(nameof(Department));
        }

        try
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return department;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(Department)} could not be saved: {ex.Message}");
        }
    }

    public async Task<Department> Delete(Department department)
    {
        if (department == null)
        {
            throw new ArgumentNullException(nameof(Department));
        }

        try
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return department;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(Department)} could not be saved: {ex.Message}");
        }
    }
}