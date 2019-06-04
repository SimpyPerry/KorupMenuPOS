using KorupMenuPOS.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorupMenuPOS.Data
{
    public class ProductDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public ProductDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Product>().Wait();
        }

        public List<Product> Products { get; set; }

        public async Task<int> CreateOrUpdateProductDB()
        {
            Products = new List<Product>();
            Products = await App.Restmanager.GetAllProducts();

            int count = await _database.Table<Product>().CountAsync();
            
            Product challenge = new Product()
            {
               
                      ProductName = "Udfordringen!!!",
                       ProductId = 999,
                       Description = "Er du mand eller kvinde nok til at spise denne enorme bøf, og kan du gøre det hurtigere end dine venner?",
                       SellingPrice = 129.95,
                       CategoryID = 999

            };

            

            if (await SeeIfPDatabaseIsEmpty())
            {
                return await _database.InsertAllAsync(Products);
            }
            else if(Products.Count >= count && !Products.Contains(challenge))
            {
                await _database.DeleteAllAsync<Product>();
                Products.Add(challenge);
                return await _database.InsertAllAsync(Products);
            }
            else
            {
                return await _database.UpdateAllAsync(Products);
            }
        }

        public Task<List<Product>> GetProductsForCategory(Categories cate)
        {


            return _database.Table<Product>().Where(p => p.CategoryID == cate.Id).ToListAsync();
        }

        public async Task<bool> SeeIfPDatabaseIsEmpty()
        {
            var result = await _database.Table<Product>().Where(x => x.CategoryID <= 1).CountAsync();
            return result == 0;
        }
    }
}
