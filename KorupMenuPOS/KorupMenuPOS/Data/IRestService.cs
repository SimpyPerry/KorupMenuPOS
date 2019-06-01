using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KorupMenuPOS.Model;

namespace KorupMenuPOS.Data
{
    public interface IRestService
    {
        Task<List<Categories>> RefreshMenuDataAsync();
        Task<Categories> GetTheCategori(int id);
        Task<List<Product>> GetProductsAsync();
        Task<string> SendListOfOrderToPOS(List<OrderItem> orders);
        Task<HttpResponseMessage> SendLoginPinAsync(int pin);
    }
}
