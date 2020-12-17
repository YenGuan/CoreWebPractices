using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebPractice.Models
{
    public partial class Person
    {
        public Person()
        {
            CourseInstructor = new HashSet<CourseInstructor>();
            Department = new HashSet<Department>();
            Enrollment = new HashSet<Enrollment>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} 為必填")]
        [StringLength(50, ErrorMessage = "{0} 最大長度為{1}")]
        [Display(Name = "姓")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "{0} 為必填")]
        [StringLength(50, ErrorMessage = "{0} 最大長度為{1}")]
        [Display(Name = "名")]
        public string FirstName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HireDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EnrollmentDate { get; set; }
        [Required(ErrorMessage = "{0} 為必填")]
        [StringLength(128, ErrorMessage = "{0} 最大長度為{1}")]
        public string Discriminator { get; set; }

        [InverseProperty("Instructor")]
        public virtual OfficeAssignment OfficeAssignment { get; set; }
        [InverseProperty("Instructor")]
        public virtual ICollection<CourseInstructor> CourseInstructor { get; set; }
        [InverseProperty("Instructor")]
        public virtual ICollection<Department> Department { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
