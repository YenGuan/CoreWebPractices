﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebPractice.Models
{
    public partial class Enrollment
    {
        [Key]
        [Column("EnrollmentID")]
        public int EnrollmentId { get; set; }
        [Column("CourseID")]
        public int CourseId { get; set; }
        [Column("StudentID")]
        public int StudentId { get; set; }
        public int? Grade { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Enrollment")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(StudentId))]
        [InverseProperty(nameof(Person.Enrollment))]
        public virtual Person Student { get; set; }
    }
}
