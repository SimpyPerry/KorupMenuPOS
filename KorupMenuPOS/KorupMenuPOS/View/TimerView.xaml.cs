using KorupMenuPOS.ViewModel;
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
	public partial class TimerView : ContentPage
	{
		public TimerView ()
		{
			InitializeComponent ();
           
		}

        private void GoToLeaderBoard_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LeaderBoardPage());
        }
    }
}