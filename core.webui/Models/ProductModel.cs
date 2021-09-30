using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using core.entity;

namespace core.webui.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        // [Display(Name="Name",Prompt="Enter product name")]
        [Required(ErrorMessage = "Name zorunlu bir alandır.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Ürün ismi 5-10 karakter aralığında olmalıdır.")]
        public string Name { get; set; }

        // [Required(ErrorMessage="Url zorunlu bir alan.")]
        public string Url { get; set; }

        // [Required(ErrorMessage="Price zorunlu bir alan.")]

        [Range(1, 999999.99, ErrorMessage = "Ücret için 1-1000000 arasında değer girmelisiniz.")]
        public double? Price { get; set; }

        [Range(0, 4.99, ErrorMessage = "Puan için 0-5 arasında değer girmelisiniz.")]
        public double? Rate { get; set; }
        public int Stock { get; set; }

        [Required(ErrorMessage = "Description zorunlu bir alan.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Description 5-200 karakter aralığında olmalıdır.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ImageUrl zorunlu bir alandır.")]
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}