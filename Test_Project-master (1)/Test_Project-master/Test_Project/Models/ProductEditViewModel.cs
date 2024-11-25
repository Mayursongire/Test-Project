using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_Project.Models
{
    public class ProductEditViewModel
    {
        [Display(Name ="Product Id")]
        public int ProductId { get; set; }

        [Display(Name ="Product Name")]
        public string ProductName { get; set; }

        [Display(Name ="Category Id")]
        public int CategoryId { get; set; }  // The currently selected category

        public IEnumerable<SelectListItem> Categories { get; set; }  // Categories for the dropdown
    }

}