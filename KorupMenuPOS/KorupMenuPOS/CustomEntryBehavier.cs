
using KorupMenuPOS.Model;
using KorupMenuPOS.View;
using KorupMenuPOS.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace KorupMenuPOS
{
    public class CustomEntryBehavier : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            //Setup
            entry.TextChanged += OnEntryTextChanged;
            //entry.Completed += OnEntryCompleted;
            base.OnAttachedTo(entry);
        }

       

        protected override void OnDetachingFrom(Entry entry)
        {
            //Clean up
            entry.TextChanged -= OnEntryTextChanged;
            //entry.Completed -= OnEntryCompleted;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            //Behavior 
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                var entry = ((Entry)sender);

                //Sikre at alle chars er numre
                bool isValid = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x));

                int Length = args.NewTextValue.Length;

                
                if(entry.Text.Length > 4 || !isValid)
                {
                    string entryText = entry.Text;

                    entryText = entryText.Remove(entryText.Length - 1);

                    entry.Text = entryText;
                }
               

                //((Entry)sender).Text = isValid ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }

        //private void OnEntryCompleted(object sender, EventArgs e)
        //{
        //    var entry = ((Entry)sender);

        //    int pin = int.Parse(entry.Text);

        //    if (entry.Text.Length < 4)
        //    {
        //        entry.Text = entry.Text;
        //    }
        //    //else
        //    //{
        //    //    var result = await App.Restmanager.ServerLogin(pin);

                
        //    //    if(result.IsSuccessStatusCode)
        //    //    {
        //    //        entry.Text = "12";
        //    //    }
        //    //    else
        //    //    {

        //    //        entry.Text = "02";
        //    //        //await App.Current.MainPage.Navigation.PushAsync(new LoggedInPage());
        //    //    }
        //    //}
        //}


    }
}
