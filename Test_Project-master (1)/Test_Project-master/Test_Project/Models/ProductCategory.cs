using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Project.Models
{
    public class ProductCategoryViewModel
    {
        [Display(Name ="Product Id")]
        public int ProductId { get; set; }

        [Display(Name ="Product Name")]
        public string ProductName { get; set; }

        [Display(Name ="Category Id")]
        public int CategoryId { get; set; }

        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }
    }

}