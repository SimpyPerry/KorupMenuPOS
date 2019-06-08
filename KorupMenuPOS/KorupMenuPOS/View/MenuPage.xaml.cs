using KorupMenuPOS.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KorupMenuPOS.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
		public MenuPage ()
		{

			InitializeComponent ();

		}

        private void PlaceOrder_PopUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OrderPage());
            //PopupNavigation.Instance.PushAsync(new PopupAuthenticate());
        }

        private void ServerLogin_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new ServerLoginPopUpPage());
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private void GoToChallenge_Label_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LeaderBoardPage());
        }
    }
}