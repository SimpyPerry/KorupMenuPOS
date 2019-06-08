using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KorupMenuPOS.Model
{
    public class Product
    {
        [PrimaryKey]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }
        
        public string Description { get; set; }

        public int Sku { get; set; }

        [NotNull]
        public double Price { get; set; }
        [NotNull]
        public int  CategoryId { get; set; }
    }
}
