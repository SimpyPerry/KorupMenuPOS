using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using KorupMenuPOS.Model;

namespace KorupMenuPOS.Data
{
    public class OrderItemService : IOrderItemsService
    {
        private ObservableCollection<OrderItem> items;
        public OrderItemService()
        {
            items = new ObservableCollection<OrderItem>();
        }

        public void AddToList(ObservableCollection<OrderItem> list)
        {
            //Clear fucker hele lortet
            //items.Clear();

            items = list;
        }

        public ObservableCollection<OrderItem> GetOrderItemsList()
        {
            return items;
        }

        public void RemovedFromOrder(int id)
        {
            var item = items.Where(x => x.ProductId == id).FirstOrDefault();

            if(item.Amount > 1)
            {
                item.Amount--;
            }
            else
            {
                items.Remove(item);
            }
        }
    }
}
