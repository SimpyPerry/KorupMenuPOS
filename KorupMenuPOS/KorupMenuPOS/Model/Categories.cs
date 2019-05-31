using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace KorupMenuPOS.Model
{
    public class Categories
    {
        [PrimaryKey]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        
        public string Description { get; set; }
       
        [Ignore]
        public List<Product> Products { get; set; }
    }
}
