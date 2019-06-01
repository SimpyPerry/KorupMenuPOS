using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace KorupMenuPOS.Model
{
    public class OrderItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int ProductId { get; set; }

        public string ProdcutName { get; set; }

        public double Price { get; set; }

        private int amount;

        

        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }



        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
