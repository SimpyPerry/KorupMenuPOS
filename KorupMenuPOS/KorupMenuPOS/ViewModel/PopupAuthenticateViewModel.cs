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

        public string PlaceHolder { get; set; } = "Indtast Pin";

        private string _Pin;
        public string Pin
        {
            get { return _Pin; }
            set
            {
                _Pin = value;
                OnPropertyChanged(nameof(Pin));
            }
        }
        public bool IsBusy { get; set; }

        public PopupAuthenticateViewModel()
        {
            EnterPinCommand = new Command(LoginAndSend);
        }

        public ICommand EnterPinCommand { get; set; }

        private async void LoginAndSend()
        {
            
            
                if(Pin.Length == 4)
                {
                    int code = int.Parse(Pin);
                    IsBusy = true;
                    OnPropertyChanged(nameof(IsBusy));

                    var response = await App.Restmanager.ServerLogin(code);

                    

                    if (response.IsSuccessStatusCode)
                    {
                        var order = App.ItemManager.GetOrderItems();
                        string comment = App.ItemManager.GetComment();

                        if(order != null)
                        {
                            PlaceHolder = await App.Restmanager.SendeOrderToPOS(order, comment);
                            if(PlaceHolder == "Ordren er sendt")
                            {
                                //Lave kode der nulstiller lister af produkter og orderitem, samt commet
                                //sørg for koden også er tilgængelig i instillinger for tjener
                                
                                App.ItemManager.ResetOrder();
                                App.ItemManager.GetTotalePrice();
                                
                                //husk at tilføje max længde til kommentar
                                //ændre på udseende af entry comment
                                
                            }

                           
                        }
                        else
                        {
                            PlaceHolder = "Ingen vare";
                        }

                    }
                    else
                    {
                        Pin = "";
                        PlaceHolder = "Forkert Kode";
                        App.ItemManager.ResetOrder();
                        App.ItemManager.GetTotalePrice();
                        OnPropertyChanged(nameof(PlaceHolder));
                   
                    }
                }


                IsBusy = false;

                OnPropertyChanged(nameof(IsBusy));


        }

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
