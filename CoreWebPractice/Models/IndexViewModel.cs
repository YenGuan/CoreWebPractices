using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace CoreWebPractice.Models
{
    public class IndexViewModel
    {
        [Display(Name = "ModelExample")]
        [Required(ErrorMessage = "RequiredErrorMessage")]
        [StringLength(maximumLength: 50, ErrorMessage = "StringLengthErrorMessage", MinimumLength = 8)]
        public string LangString { get; set; }
    }
}
