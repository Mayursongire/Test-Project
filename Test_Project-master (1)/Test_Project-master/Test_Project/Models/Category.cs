public class Category
{
    [Key]
    [Display(Name ="Category ID")]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(500)]
    [Display(Name ="Category Name")]
    public string CategoryName { get; set; }

    [Display(Name = "Is Active")]
    public bool IsActive { get; set; }  

    public virtual ICollection<Product> Products { get; set; }
}
