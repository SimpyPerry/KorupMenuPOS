using KorupMenuPOS.Data;
using KorupMenuPOS.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace KorupMenuPOS.ViewModel
{
    public class LeaderBoardViewModel : INotifyPropertyChanged
    {

        
        public List<HighScore> HighScores { get; set; }

        public LeaderBoardViewModel()
        {
            ToList();
        }

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
                    ChallengeDate = cp.ChallengeDate

                });
               
            }

            HighScores = scores;

            OnPropertyChanged(nameof(HighScores));
            
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
