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
	public partial class LoggedPage : ContentPage
	{
		public LoggedPage ()
		{
			InitializeComponent ();
		}

        private async void Refresh_btn_Clicked(object sender, EventArgs e)
        {
            await App.MDatabase.RefreshDatabaseDataAsync();
            await App.PDatabase.CreateOrUpdateProductDB();
        }

        private async void GoToTimer_btn_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new TimerView());
        }

        private async void GoToLeaderBoard_btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LeaderBoardPage());
        }

        private void Exit_App_btn_Clicked(object sender, EventArgs e)
        {
            //For IOS
            //Thread.CurrentThread.Abort();

            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}