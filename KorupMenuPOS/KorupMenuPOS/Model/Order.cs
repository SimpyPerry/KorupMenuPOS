using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace KorupMenuPOS.Model
{
    public class Order : INotifyPropertyChanged
    {
        //Der skal også være en propterychanged herinde da vi skulle kunne registere når amount ændre sig
        public event PropertyChangedEventHandler PropertyChanged;

        private int amount;

        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }
        public string ProdcutName { get; set; }

        public string Comment { get; set; }
        public List<OrderItem> OrderItems { get; set; }




        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
