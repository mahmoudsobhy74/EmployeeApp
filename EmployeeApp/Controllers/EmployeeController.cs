using EmployeeApp.Models;
using EmployeeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeApp.Controllers;

public class EmployeeController : Controller
{
    private readonly EmployeeService _service;
    private readonly DepartmentService _departmentService;
    public EmployeeController(EmployeeService service,
        DepartmentService departmentService)
    {
        _service = service;
        _departmentService = departmentService;
    }
    public async Task<IActionResult> Index()
    {
        var emps = await _service.GetAsync();
        return View(emps);
    }

    public async Task<IActionResult> Details(int Id)
    {
        var emps = await _service.GetAsync(Id);

        ViewBag.DepartmentId = new SelectList(await _departmentService.GetAsync(), "Id", "Name", emps.DepartmentId);
        ViewBag.ManagerId = new SelectList(await _service.GetAsync(), "Id", "Name", emps.ManagerId);
        return View(emps);
    }

    public async Task<IActionResult> Create()
    {

        ViewBag.DepartmentId = new SelectList(await _departmentService.GetAsync(), "Id", "Name");
        ViewBag.ManagerId = new SelectList(await _service.GetAsync(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee employee)
    {
       
        var emps = await _service.AddEmployeeAsync(employee);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int Id)
    {
        var emps = await _service.GetAsync(Id);

        ViewBag.DepartmentId = new SelectList(await _departmentService.GetAsync(), "Id", "Name", emps.DepartmentId);
        ViewBag.ManagerId = new SelectList(await _service.GetAsync(), "Id", "Name", emps.ManagerId);

        return View(emps);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Employee employee)
    {
        var emps =  await _service.EditEmployeeAsync(employee);

        return RedirectToAction("Index", "Employee");

    }
    public async Task<IActionResult> Delete(int Id)
    {
        var emps = await _service.GetAsync(Id);
        return View(emps);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(Employee employee)
    {
        var emps = await _service.Delete(employee);

        return  RedirectToAction("Index");

    }

}
