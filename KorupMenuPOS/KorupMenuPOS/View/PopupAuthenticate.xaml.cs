using KorupMenuPOS.ViewModel;
using Rg.Plugins.Popup.Pages;
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
	public partial class PopupAuthenticate : PopupPage
	{
		public PopupAuthenticate ()
		{
			InitializeComponent ();

            BindingContext = new PopupAuthenticateViewModel();
		}


        private async void Goback_btn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }

        

        
    }
}
