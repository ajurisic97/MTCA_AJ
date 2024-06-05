using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCA.Application.Commons.Models;
public class ProductInfo
{
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal DiscountAmount { get; set; } = 0;
    public decimal DiscountPercentage { get; set; } = 0;
    public decimal Price { get; set; } 
    public decimal TotalPrice { get; set; }
    public string? Remark { get; set; }
    public bool Warranty { get; set; }
    public string? SerialNumber { get; set; }
    public List<ProductInfo> ProductList { get; set; } = new List<ProductInfo>();

}

