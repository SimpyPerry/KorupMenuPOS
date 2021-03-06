﻿using Rg.Plugins.Popup.Pages;
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
	public partial class ServerLoginPopUpPage : PopupPage
	{
		public ServerLoginPopUpPage ()
		{
			InitializeComponent ();
		}

        private async void PincodeEntry_Completed(object sender, EventArgs e)
        {
            string entry = PincodeEntry.Text;


            if (entry.Length == 4)
            {

                var pin = int.Parse(entry);

                IsBusy.IsRunning = true;
                var result = await App.Restmanager.ServerLogin(pin);

                if (result.IsSuccessStatusCode)
                {
                    await Navigation.PushAsync(new LoggedPage());

                    await PopupNavigation.Instance.PopAsync();
                    
                }
                else
                {
                    
                    PincodeEntry.Text = null;
                    //ændres tilbage til forkert kode
                    PincodeEntry.Placeholder = "Godkendt";
                    await Navigation.PushAsync(new LoggedPage());
                    await PopupNavigation.Instance.PopAsync();
                }
            }
            else
            {
                
            }

            IsBusy.IsRunning = false;

        }

        private async void Goback_btn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}