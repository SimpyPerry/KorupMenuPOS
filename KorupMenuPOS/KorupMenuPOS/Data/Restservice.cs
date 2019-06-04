using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KorupMenuPOS.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.CSharp;
using System.Collections.ObjectModel;

namespace KorupMenuPOS.Data
{
    public class Restservice : IRestService
    {
        private readonly HttpClient _httpClient;
        private Uri ProductsEndPoint { get; set; }
        private Uri CategoriesEndPoint { get; set; }
        private Uri OrdersEndPoint { get; set; }
        private Uri LoginEndPoint { get; set; }
        public List<Categories> MenuItems { get; private set; }
        public Categories Category { get; private set; }
        public List<Product> Products { get; private set; }

        public Restservice()
        {
            
            _httpClient = new HttpClient();
            CategoriesEndPoint = new Uri("https://koruppos.azurewebsites.net/api/categories");
            ProductsEndPoint = new Uri("https://koruppos.azurewebsites.net/api/products");
            OrdersEndPoint = new Uri("https://koruppos.azurewebsites.net/api/orders");
            LoginEndPoint = new Uri("https://koruppos.azurewebsites.net/api/account");
        }

        public async Task<List<Categories>> RefreshMenuDataAsync()
        {
            MenuItems = new List<Categories>();

            var response = await _httpClient.GetAsync(CategoriesEndPoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                MenuItems = JsonConvert.DeserializeObject<List<Categories>>(content);
            }
            
            

            return MenuItems;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            MenuItems = new List<Categories>();

            var response = await _httpClient.GetAsync(CategoriesEndPoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                MenuItems = JsonConvert.DeserializeObject<List<Categories>>(content);
            }

            Products = new List<Product>();

            foreach(Categories cate in MenuItems)
            {
                List<Product> ps = new List<Product>();

                ps = cate.Products;

                foreach (Product pro in ps)
                {
                    pro.CategoryID = cate.Id;
                    Products.Add(pro);
                }
            }

            return Products;
            
        }

        public async Task<Categories> GetTheCategori(int id)
        {
            Category = new Categories();


            MenuItems = new List<Categories>();

            var response = await _httpClient.GetAsync(CategoriesEndPoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                MenuItems = JsonConvert.DeserializeObject<List<Categories>>(content);
            }

            Category = MenuItems.Find(c => c.Id == id);

            return Category;


        }

        public async Task<string> SendListOfOrderToPOS(ObservableCollection<OrderItem> orders, string  comment)
        {
            //Order theOrder = new Order();

            //theOrder.Comment = "{Tester lige en gang}";
            //List<OrderItem> items = new List<OrderItem>();

            //foreach (Order o in orders)
            //{
            //    OrderItem oi = new OrderItem();

            //    oi.Amount = o.Amount;
            //    oi.ProductId = o.ProductId;

            //    items.Add(oi);

            //}

            //theOrder.OrderItems = items;



            JArray products = new JArray();


            foreach (OrderItem o in orders)
            {
                products.Add(JObject.Parse(
                    @"{""ProductId"":""" + o.ProductId + "\", " + @"""Amount"":""" + o.Amount + @"""}"));


            }

            JObject basket = JObject.Parse(@"{ 'Comment': '"+ comment +"' }" +
                "'OrderItems':[" + products.ToString() + "]");




            //string basket = @"{
            //            'Comment':'Test af format',
            //            'OrderItems':[
            //                {
            //                    'ProductId':'1',
            //                    'Amount':'2'
            //                },
            //                {
            //                    'ProductId':'3',
            //                    'Amount':'1'
            //                }
            //              ]
            //            }";

            //JObject jo = JObject.Parse(basket);


            //foreach (Order o in orders)
            //{
            //    basket = basket +
            //        @"{'ProductId':"+o.ProductId+ ",'Amount':" +o.Amount+"}";


            //}

            //basket = basket + "]}";



            //JArray prod = new JArray();

            ////foreach(Order o in orders)
            ////{
            ////    prod.Add(new JProperty())
            ////};

            //foreach (Order o in orders)
            //{
            //    prod.Add(new JObject(
            //        new JProperty("ProductId", o.ProductId),
            //        new JProperty("Amount", o.Amount)
            //        ));

            //}


            //    JObject basket = new JObject
            //{
            //    new JProperty ("Comment", "Værdi der bliver lavet senere"),
            //    new JProperty ("OrderItems", prod)
            //};


            var json = JsonConvert.SerializeObject(basket);
            //StringContent laver det om til HttpContent for ellers kan det ikke sendes
            //Kan ikke sende et array uden index 
            var content = new StringContent(json, Encoding.UTF8, "aplication/json");


            var response = await _httpClient.PostAsync(OrdersEndPoint, content);

            if (response.IsSuccessStatusCode)
            {
                return "Ordren er sendt";
                
            }else
            {
                return response.ToString();
            }
        }

        public async Task<HttpResponseMessage> SendLoginPinAsync(int pin)
        {
            var json = JsonConvert.SerializeObject(pin);

            var content = new StringContent(json, Encoding.UTF8, "aplication/json");

            var response = await _httpClient.PostAsync(LoginEndPoint, content);

            return response;
        }
    }
}
