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

        private async void UpdateLoginValue(int number)
        {
            var response = await App.Restmanager.ServerLogin(number);

            if (response.IsSuccessStatusCode)
            {

            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
