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

        public void AddOneToOrder(Product addedProduct)
        {
            itemService.AddToOrder(addedProduct);
        }

        public void RemoveOneItemWithProduct(Product removedProduct)
        {
            itemService.RemoveThisFromOrder(removedProduct);
        }

        public double GetTotalePrice()
        {
            return itemService.CalcultaePrice();
        }

        public void AddOneItemToOrder(int id)
        {
            itemService.AddToOrderFromOrder(id);
        }

        public void AddCommentToOrder(string comment)
        {
            itemService.CommentToOrder(comment);
        }

        public string GetComment()
        {
            return itemService.GetTheComment();
        }

        public void ResetOrder()
        {
            itemService.EmptyOrder();
        }

        public void AddChallengeToThisOrder()
        {
            itemService.ChallengeToOrder();
        }

    }
}
