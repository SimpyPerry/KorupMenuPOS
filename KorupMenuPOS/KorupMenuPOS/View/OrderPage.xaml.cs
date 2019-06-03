
using KorupMenuPOS.Model;
using KorupMenuPOS.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KorupMenuPOS.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        
        public OrderPage()
        {
            
            InitializeComponent();

        }

        private void Send_order_btn_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupAuthenticate());
        }
    }
}