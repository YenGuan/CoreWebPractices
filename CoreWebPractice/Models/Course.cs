using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebPractice.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseInstructor = new HashSet<CourseInstructor>();
            Enrollment = new HashSet<Enrollment>();
        }
        
 
        [Key]
        [Column("CourseID")]
        public int CourseId { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public int Credits { get; set; }
        [Column("DepartmentID")]
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Course")]
        public virtual Department Department { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<CourseInstructor> CourseInstructor { get; set; }
        [InverseProperty("Course")]
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
