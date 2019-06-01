using KorupMenuPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KorupMenuPOS.ViewModel
{
    public class LoggedInViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Order> OrderList { get; set; }
        public double PrizeSum { get; set; }
        

        public LoggedInViewModel()
        {
            ReceiveOrderCommand = new Command(ReciveOrderList);
            ReciveOrderList();
        }

        public ICommand ReceiveOrderCommand { get; private set; }

        private void  ReciveOrderList()
        {
            MessagingCenter.Subscribe<MenuPageViewModel, ObservableCollection<Order>>(this, "Order", (s,a) =>
            {
                OrderList = a;

            });

            OnPropertyChanged(nameof(OrderList));
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
