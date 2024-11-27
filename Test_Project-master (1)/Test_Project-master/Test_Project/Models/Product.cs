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

    [ForeignKey("ProductCategoryId")]
    public virtual Category Category { get; set; }  
}
