
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApp.Models;

[Table("Department", Schema = "dbo")]
public class Department
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    [Display(Name = "Department ID")]
    public int DepartmentId { get; set; }

    [Required, MaxLength(150)]
    [Column(TypeName = "varchar(150)")]
    [Display(Name = "Department Name")]
    public string DepartmentName { get; set; }

    [ForeignKey(nameof(Models.Employee))]
    public int? ManagerId { get; set; }

    [NotMapped]
    public Employee? Manager { get; set; }

    [NotMapped]
    public List<Employee> Employees { get; set; }
}
