using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KorupMenuPOS.Model
{
    public class Product
    {
        [PrimaryKey]
        public int ProductId { get; set; }

        [NotNull]
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }

        
        public int UnitOfMeasureId { get; set; }
        public double BuyingPrice { get; set; }
        [NotNull]
        public double SellingPrice { get; set; }

        public int BranchId { get; set; }
        public int CurrencyId { get; set; }
        [NotNull]
        public int  CategoryID { get; set; }
    }
}
