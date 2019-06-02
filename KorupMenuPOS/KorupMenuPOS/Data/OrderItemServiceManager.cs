using KorupMenuPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KorupMenuPOS.Data
{
    
    public class OrderItemServiceManager
    {
        IOrderItemsService itemService;
        public OrderItemServiceManager(IOrderItemsService service)
        {
            itemService = service;
        }


        public ObservableCollection<OrderItem> GetOrderItems()
        {
            return itemService.GetOrderItemsList();
        }

        public void AddListToService(ObservableCollection<OrderItem> list)
        {
            itemService.AddToList(list);
        }

        public void RemovedOneItemFromOrder(int id)
        {
            itemService.RemovedFromOrder(id);
        }
    }
}
