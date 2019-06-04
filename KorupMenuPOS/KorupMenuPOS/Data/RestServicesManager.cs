using KorupMenuPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KorupMenuPOS.Data
{
    //Denne fungere som mellemleddet for interfacet og Viewet
    //Dette gør jeg da man ikke parse interfacet som parameter i viewet
    //På denne måde kan man gøre brug af RestService som et repo
    public class RestServicesManager
    {
        IRestService restService;
        public RestServicesManager(IRestService service)
        {
            restService = service;
        }

        public async Task<List<Categories>> GetMenuData()
        {
            return await restService.RefreshMenuDataAsync();
        }

        public async Task<Categories> GetACategori(int id)
        {
            return await restService.GetTheCategori(id);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await restService.GetProductsAsync();
        }

        public async Task<string> SendeOrderToPOS(ObservableCollection<OrderItem> orders, string comment)
        {
            return await restService.SendListOfOrderToPOS(orders, comment);
        } 

        public async Task<HttpResponseMessage> ServerLogin(int pin)
        {
            return await restService.SendLoginPinAsync(pin);
        }
    }
}
