using KorupMenuPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KorupMenuPOS.ViewModel
{
    public class LoggedinLeaderboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<HighScore> HighScores { get; set; }
        public LoggedinLeaderboardViewModel()
        {
            ToList();

            DeleteTimeCommand = new Command<HighScore>(DeleteTime);
        }

        public ICommand DeleteTimeCommand { get; private set; }
        async void ToList()
        {
            List<ChallengePerson> Challengers = new List<ChallengePerson>();

            Challengers = await App.Database.GetLeaderBoardTimes();

            HighScores = new ObservableCollection<HighScore>();

            foreach (ChallengePerson cp in Challengers)
            {
                HighScores.Add(new HighScore
                {
                    ChallengerName = cp.PersonName,
                    HighScoreTime = cp.ChallengeTime.ToLongTimeString(),
                    ChallengeDate = cp.ChallengeDate.ToShortDateString(),
                    ChallengenPersonId = cp.PersonID

                });

            }

            
            OnPropertyChanged(nameof(HighScores));

        }

        private void DeleteTime(HighScore score)
        {
            App.Database.DeleteThisScore(score.ChallengenPersonId);

            HighScores.Remove(score);

            OnPropertyChanged(nameof(HighScores));
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
