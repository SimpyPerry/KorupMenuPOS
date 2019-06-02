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
            GetTotalPrice();

            OnPropertyChanged(nameof(OrderItems));

            //Hvis knappen er inde i et list view, skal du vide reference til commanden ellers virker det ikke
            //dette skal gøres i Xaml delen
            RemoveCommand = new Command<OrderItem>(RemovedOneOrderItem);
        }

        public ICommand RemoveCommand { get; private set; }

        private void GetTotalPrice()
        {
            if (!OrderItems.Any())
            {
                TotalPrice = 0;


            }
            else
            {
                double price = 0;

                foreach(OrderItem item in OrderItems)
                {
                    double sub = item.Amount * item.Price;

                    price = price + sub;
                }

                TotalPrice = price;
            }

            OnPropertyChanged(nameof(TotalPrice));
        }
        private void RemovedOneOrderItem(OrderItem item)
        {
            App.ItemManager.RemovedOneItemFromOrder(item.ProductId);

            OrderItems = App.ItemManager.GetOrderItems();

            OnPropertyChanged(nameof(OrderItems));
        }


        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
