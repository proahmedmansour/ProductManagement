using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Products
{
    public class CreateUpdateProductDto
    {
        [Required]
        [StringLength(ProductConsts.MaxNameLength)]
        public string Name { get; set; }

        public float Price { get; set; }

        public bool IsFreeCargo { get; set; }

        public DateTime ReleaseDate { get; set; }

        public ProductStockState StockState { get; set; }

        public Guid CategoryId { get; set; }
    }
}