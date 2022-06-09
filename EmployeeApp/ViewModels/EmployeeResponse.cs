namespace EmployeeApp.ResponseModels;

public class EmployeeResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Salary { get; set; }
    public string Department { get; set; }
    public string Manager { get; set; }
}