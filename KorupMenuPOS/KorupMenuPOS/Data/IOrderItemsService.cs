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
        void AddToOrder(Product addedProduct);
        void RemoveThisFromOrder(Product removedProduct);
        double CalcultaePrice();
        void AddToOrderFromOrder(int id);
        void CommentToOrder(string comment);
        string GetTheComment();
        void EmptyOrder();
    }
}
