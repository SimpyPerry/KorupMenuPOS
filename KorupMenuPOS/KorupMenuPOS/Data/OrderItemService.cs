using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using KorupMenuPOS.Model;

namespace KorupMenuPOS.Data
{
    public class OrderItemService : IOrderItemsService, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<OrderItem> items;
        private ObservableCollection<Product> products;
        

        private string message;
        private double _total;
        public double total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged(nameof(total));
            }
        }


        public OrderItemService()
        {
            items = new ObservableCollection<OrderItem>();
            products = new ObservableCollection<Product>();
            total = new double();
            message = "";
           
            
        }


        public void AddToList(ObservableCollection<OrderItem> list)
        {
            //Clear fucker hele lortet
            //items.Clear();

            items = list;
        }

        public void AddToOrder(Product addedProduct)
        {
            products.Add(addedProduct);

            List<Product> addedProductList = new List<Product>();

            addedProductList = products.Where(x => x.Id == addedProduct.Id).ToList();

            int amount = 0;

            foreach(Product product in addedProductList)
            {
                amount++;
            }

            var item = items.Where(x => x.ProductId == addedProduct.Id).FirstOrDefault();

            if(item == null)
            {
                OrderItem o = new OrderItem()
                {
                    ProductId = addedProduct.Id,
                    Price = addedProduct.Price,
                    ProdcutName = addedProduct.Name,
                    Amount = 1

                };

                items.Add(o);
            }
            else
            {
                items.Where(x => x.ProductId == addedProduct.Id).FirstOrDefault().Amount = amount;
            }
        }

        public void AddToOrderFromOrder(int id)
        {
            var item = items.Where(x => x.ProductId == id).FirstOrDefault();
            var product = products.Where(x => x.Id == id).FirstOrDefault();

            products.Add(product);

            item.Amount++;
        }


        public double CalcultaePrice()
        {
            if (!items.Any())
            {
                
                return 0;
            }
            else
            {
                double price = 0;

                foreach (OrderItem item in items)
                {
                    double sub = item.Amount * item.Price;

                    price = price + sub;
                }

                total = price;
                return total;
                
            }
        }

        public ObservableCollection<OrderItem> GetOrderItemsList()
        {
            return items;
        }

        public void RemovedFromOrder(int id)
        {
            var item = items.Where(x => x.ProductId == id).FirstOrDefault();
            var product = products.Where(x => x.Id == id).FirstOrDefault();

            products.Remove(product);

            if (item.Amount > 1)
            {
                item.Amount--;
            }
            else
            {
                item.Amount = 0;
                items.Remove(item);
            }
        }

        public void RemoveThisFromOrder(Product removedProduct)
        {
            if(products.Any(x=> x.Id == removedProduct.Id))
            {
                var index = products.IndexOf(products.Where(x => x.Name == removedProduct.Name).FirstOrDefault());

                products.RemoveAt(index);

                List<Product> removedProductList = new List<Product>();
                removedProductList = products.Where(x => x.Id == removedProduct.Id).ToList();

                var item = items.Where(x => x.ProductId == removedProduct.Id).FirstOrDefault();

                if (!removedProductList.Any())
                {
                    item.Amount = 0;
                    items.Remove(item);
                }
                else
                {
                    item.Amount = removedProductList.Count();

                }
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CommentToOrder(string comment)
        {
            if(comment != null)
            {
                message = comment;
            }
            else
            {
                message = "";
            }
            
        }

        public string GetTheComment()
        {
            return message;
        }

        public void EmptyOrder()
        {
            products.Clear();
            items.Clear();
            message = "";
            
        }

        public void ChallengeToOrder()
        {
            Product challenge = new Product()
            {

                Name = "Udfordringen!!!",
                Id = 999,
                Description = "Er du mand eller kvinde nok til at spise denne enorme bøf, og kan du gøre det hurtigere end dine venner?",
                Price = 129.95,
                CategoryId = 999

            };

            AddToOrder(challenge);
        }
    }
}
