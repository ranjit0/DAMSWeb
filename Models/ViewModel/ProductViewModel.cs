using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAMSWeb.Models.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> SellingPrice { get; set; }
        public string Photo { get; set; }

   }
}