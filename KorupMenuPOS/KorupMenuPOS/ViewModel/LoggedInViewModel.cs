using KorupMenuPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace KorupMenuPOS.ViewModel
{
    public class LoggedInViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Order> OrderList { get; set; }
        public double PrizeSum { get; set; }
        

        public LoggedInViewModel(MenuPageViewModel model)
        {
            OrderList = model.OrderList;
            PrizeSum = model.TotalePrice;
        }



        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
