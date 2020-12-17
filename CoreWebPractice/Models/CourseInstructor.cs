using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebPractice.Models
{
    public partial class CourseInstructor
    {
        [Key]
        [Column("CourseID")]
        public int CourseId { get; set; }
        [Key]
        [Column("InstructorID")]
        public int InstructorId { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("CourseInstructor")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(InstructorId))]
        [InverseProperty(nameof(Person.CourseInstructor))]
        public virtual Person Instructor { get; set; }
    }
}
