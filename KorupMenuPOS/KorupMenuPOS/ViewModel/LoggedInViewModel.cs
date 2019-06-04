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

        public bool IsBusy { get; set; }

        public LoggedInViewModel()
        {
            RefreshCommand = new Command(RefreshMenu);
        }

        public ICommand RefreshCommand { get; private set; }

        private async void RefreshMenu()
        {
            IsBusy = true;
            OnPropertyChanged(nameof(IsBusy));
            await App.MDatabase.RefreshDatabaseDataAsync();
            await App.PDatabase.CreateOrUpdateProductDB();
            IsBusy = false;
            OnPropertyChanged(nameof(IsBusy));
           
        }
        
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
