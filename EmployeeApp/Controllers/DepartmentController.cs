using EmployeeApp.Models;
using EmployeeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeApp.Controllers;

public class DepartmentController : Controller
{
    private readonly DepartmentService _service;
    private readonly EmployeeService _employeeService;
    public DepartmentController(DepartmentService service , 
        EmployeeService employeeService)
    {
        _service=service;
        _employeeService = employeeService;
    }
    public async Task <IActionResult> Index()
    {
        var emps = await _service.GetAsync();
        return View(emps);
    }

    public async Task<IActionResult> Details(int Id)
    {
        var emps = await _service.GetAsync(Id);
        ViewBag.ManagerId = new SelectList(await _employeeService.GetAsync(), "Id", "Name");
        return View(emps);
    }
    
    public async Task<IActionResult> Create()
    {
        ViewBag.ManagerId = new SelectList(await _employeeService.GetAsync(), "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Department department)
    {
       
        var emps = await _service.AddDepartmentAsync(department);
        return RedirectToAction("Index");
        //return View(emps);
    }

    public async Task<IActionResult> Edit(int Id)
    {
        var emps = await _service.GetAsync(Id);

        ViewBag.ManagerId = new SelectList(await _employeeService.GetAsync(), "Id", "Name", emps.ManagerId);

        return View(emps);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Department department)
    {
        var emps = await _service.EditDepartmentAsync(department);
        return RedirectToAction("Index","Department");
    }

    public async Task<IActionResult> Delete(int Id)
    {
        var emps = await _service.GetAsync(Id);
        return View(emps);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Department department)
    {
        var emps = await _service.Delete(department);
        return RedirectToAction("Index");
    }

}
