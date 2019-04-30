﻿using System;
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

        private void GoToChallengePage_button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TimerView());
        }
    }
}