using KorupMenuPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KorupMenuPOS.ViewModel
{
    public class OrderViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ObservableCollection<OrderItem> OrderItems { get; set; }
        public double TotalPrice { get; set; }
        public OrderViewModel()
        {

            OrderItems = App.ItemManager.GetOrderItems();
            TotalPrice = App.ItemManager.GetTotalePrice();

            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(OrderItems));

            //Hvis knappen er inde i et list view, skal du vide reference til commanden ellers virker det ikke
            //dette skal gøres i Xaml delen
            RemoveCommand = new Command<OrderItem>(RemovedOneOrderItem);
            AddToOrderCommand = new Command<OrderItem>(AddOneItemOrder);
        }

        public ICommand RemoveCommand { get; private set; }
        public ICommand AddToOrderCommand { get; private set; }

        
        private void RemovedOneOrderItem(OrderItem item)
        {
            App.ItemManager.RemovedOneItemFromOrder(item.ProductId);

            OrderItems = App.ItemManager.GetOrderItems();
            TotalPrice = App.ItemManager.GetTotalePrice();

            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(OrderItems));
        }

        private void AddOneItemOrder(OrderItem item)
        {
            App.ItemManager.AddOneItemToOrder(item.ProductId);
            TotalPrice = App.ItemManager.GetTotalePrice();

            OnPropertyChanged(nameof(TotalPrice));
        }


        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
