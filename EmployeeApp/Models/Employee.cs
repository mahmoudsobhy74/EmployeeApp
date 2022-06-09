using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Models;

[Table("Employee", Schema = "emp")]
public class Employee
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Display(Name = "Employee ID")]
    public int EmployeeID { get; set; }

    [Required, MaxLength(100)]
    [Column(TypeName = "varchar(150)")]
    [Display(Name = "Employee Name")]
    public string EmployeeName { get; set; }

    [Required]
    [Display(Name = "Salary")]
    public decimal Salary { get; set; }

    [ForeignKey(nameof(Models.Employee))]
    public int? ManagerId { get; set; }

    [ForeignKey(nameof(Models.Department))]
    [Required]
    public int DepartmentId { get; set; }

    [Display(Name = "Department")]
    [NotMapped]
    public string DepartmentName { get; set; }

    [NotMapped]
    public Department Department { get; set; }

    [NotMapped]
    public Employee Manager { get; set; }
}
