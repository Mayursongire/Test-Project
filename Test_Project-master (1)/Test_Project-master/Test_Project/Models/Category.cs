using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_Project.Models
{
    public class Category
    {
        [Key]
        [Display(Name ="Category ID")]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }
    }
}