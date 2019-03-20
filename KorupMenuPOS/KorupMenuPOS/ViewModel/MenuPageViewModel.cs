using KorupMenuPOS.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace KorupMenuPOS.ViewModel
{
    public class MenuPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public MenuPageViewModel()
        {
            
            GoToChallengePageCommand = new Command(async () => await GoToChanllengePage());
                
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GoToChallengePageCommand { get; }

        async Task GoToChanllengePage()
        {
            await Navigation.PushAsync(new TimerView());
        }
    }
}
