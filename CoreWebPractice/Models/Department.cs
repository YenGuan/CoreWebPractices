using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebPractice.Models
{
    public partial class Department
    {
        public Department()
        {
            Course = new HashSet<Course>();
        }

        [Key]
        [Column("DepartmentID")]
        public int DepartmentId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column("InstructorID")]
        public int? InstructorId { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        [ForeignKey(nameof(InstructorId))]
        [InverseProperty(nameof(Person.Department))]
        public virtual Person Instructor { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<Course> Course { get; set; }
    }
}
