using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KorupMenuPOS.ViewModel
{
    class PopupAuthenticateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int LoginCode { get; set; }

        public PopupAuthenticateViewModel()
        {
            AddNumberToLoginCommand = new Command<int>(UpdateLoginValue);
        }

        public ICommand AddNumberToLoginCommand;

        private void UpdateLoginValue(int number)
        {
            LoginCode = number;

            OnPropertyChanged(nameof(LoginCode));
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
