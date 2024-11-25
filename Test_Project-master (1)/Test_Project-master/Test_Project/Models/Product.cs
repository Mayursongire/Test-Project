using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_Project.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Invalid Product Name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Category ID")]
        [Required]
        public int ProductCategoryId { get; set; }


        

        
    }
}