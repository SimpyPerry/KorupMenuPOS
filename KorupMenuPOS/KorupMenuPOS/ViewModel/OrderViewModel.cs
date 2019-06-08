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
        public double totalPrice;
        public double TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        public bool IsBusy { get; set; } = false;
        
        private string _comment { get; set; }
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                App.ItemManager.AddCommentToOrder(_comment);
                
            }
        }
        
        public OrderViewModel()
        {

            OrderItems = App.ItemManager.GetOrderItems();
            TotalPrice = App.ItemManager.GetTotalePrice();

            MessagingCenter.Subscribe<PopupAuthenticateViewModel>(this, "price", (sender) => {
                UpdatePrice();
            });


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
            UpdatePrice();
           
            OnPropertyChanged(nameof(OrderItems));
        }

        private void UpdatePrice()
        {
            TotalPrice = App.ItemManager.GetTotalePrice();
            MessagingCenter.Send(this, "update");
        }

        private void AddOneItemOrder(OrderItem item)
        {
            App.ItemManager.AddOneItemToOrder(item.ProductId);
            TotalPrice = App.ItemManager.GetTotalePrice();

            
        }


        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
