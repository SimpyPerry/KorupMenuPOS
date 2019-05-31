using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using KorupMenuPOS.Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace KorupMenuPOS.Data
{
    public class MenuDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public MenuDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Categories>().Wait();
            
            
        }


        public async Task<int> RefreshDatabaseDataAsync()
        {

            List<Categories> cats = new List<Categories>();
            cats = await App.Restmanager.GetMenuData();

            

            if (await SeeIfDatabaseIsEmpty())
            {
                return await _database.InsertAllAsync(cats);
            }
            else
            {
                return await _database.UpdateAllAsync(cats);
            }

           

                //foreach (Categories cate in cats)
                //{
                //    if (cate.Id != 0)
                //    {
                //        return await _database.UpdateAsync(cate);
                //    }
                //    else
                //    {
                //        return await _database.InsertAsync(cate);
                //    }
                //}
            

            

        }

        public Task<List<Categories>> GetMenuData()
        {
            return _database.Table<Categories>().OrderBy(x => x.Id).ToListAsync();
        }

        public Task<Categories> GetOneCategori(int id)
        {
            return _database.Table<Categories>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> SeeIfDatabaseIsEmpty()
        {
            var result = await _database.Table<Categories>().Where(x => x.Id == 1).CountAsync();
            return result == 0;
        }
    }
}
