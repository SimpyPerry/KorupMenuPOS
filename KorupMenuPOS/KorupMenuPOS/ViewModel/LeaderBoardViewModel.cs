using KorupMenuPOS.Data;
using KorupMenuPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KorupMenuPOS.ViewModel
{
    public class LeaderBoardViewModel : INotifyPropertyChanged
    {

        
        public List<HighScore> HighScores { get; set; }

        public LeaderBoardViewModel()
        {
            ToList();

            ChallengeToOrderCommand = new Command(AddChallengeToOrder);
        }

        public ICommand ChallengeToOrderCommand { get; private set; }

        async void ToList()
        {
            List<ChallengePerson> Challengers = new List<ChallengePerson>();

            Challengers = await App.Database.GetLeaderBoardTimes();

            List<HighScore> scores = new List<HighScore>();

            foreach(ChallengePerson cp in Challengers)
            {
                scores.Add(new HighScore
                {
                    ChallengerName = cp.PersonName,
                    HighScoreTime = cp.ChallengeTime.ToLongTimeString(),
                    ChallengeDate = cp.ChallengeDate.ToShortDateString()

                });
               
            }

            HighScores = scores;

            OnPropertyChanged(nameof(HighScores));
            
        }

        private void AddChallengeToOrder()
        {
            App.ItemManager.AddChallengeToThisOrder();
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
