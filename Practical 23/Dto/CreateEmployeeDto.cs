using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Practical_23.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Practical_23.Dto
{
    public class CreateEmployeeDto
    {
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name length must be between 3 and 50")]
        public string? Name { get; set; }

        [Required]
        [Column(TypeName = "DECIMAL(10,2)")]
        [Range(1, double.MaxValue, ErrorMessage = "Salary value out of range")]
        public decimal Salary { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Email length must be between 10 and 100")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime JoinDate { get; set; }

        [Required]
        [EnumDataType(typeof(Department), ErrorMessage = "DepartmentId must be 0 for IT, 1 for Admin, 2 for HR, 3 for Sales, 4 for OnSite")]
        public Department DepartmentId { get; set; }
    }
}
