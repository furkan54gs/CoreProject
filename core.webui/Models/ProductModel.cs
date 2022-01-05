using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using core.entity;

namespace core.webui.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Ürün Adı", Prompt = "Ürün adını yazın.")]
        [Required(ErrorMessage = "Name zorunlu bir alandır.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Ürün ismi 5-10 karakter aralığında olmalıdır.")]
        public string Name { get; set; }

        // [Required(ErrorMessage="Url zorunlu bir alan.")]
        public string Url { get; set; }

        // [Required(ErrorMessage="Price zorunlu bir alan.")]
        [Display(Name = "Fiyat")]
        [Range(1, 999999.99, ErrorMessage = "Ücret için 1-1000000 arasında değer girmelisiniz.")]
        public decimal? Price { get; set; }

        [Display(Name = "Puan")]
        [Range(0, 4.99, ErrorMessage = "Puan için 0-5 arasında değer girmelisiniz.")]
        public double? Rate { get; set; }

        [Display(Name = "Stok")]
        public int Stock { get; set; }

        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Description zorunlu bir alan.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Description 5-200 karakter aralığında olmalıdır.")]
        public string Description { get; set; }

        // [Required(ErrorMessage = "ImageUrl zorunlu bir alandır.")]
        public string ImageUrl { get; set; }

        [Display(Name = "Onay")]
        public bool IsApproved { get; set; }

        [Display(Name = "Anasayfa")]
        public bool IsHome { get; set; }
        public List<Category> SelectedCategories { get; set; }

        [Display(Name = "Resimler")]
        public List<Image> Images { get; set; }
    }
}