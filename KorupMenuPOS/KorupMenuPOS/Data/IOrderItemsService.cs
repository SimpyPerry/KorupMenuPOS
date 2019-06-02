using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using KorupMenuPOS.Model;

namespace KorupMenuPOS.Data
{
    public interface IOrderItemsService
    {
        ObservableCollection<OrderItem> GetOrderItemsList();
        void AddToList(ObservableCollection<OrderItem> list);
        void RemovedFromOrder(int id);
    }
}
