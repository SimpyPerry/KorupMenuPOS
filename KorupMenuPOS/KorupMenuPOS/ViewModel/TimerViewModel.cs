using KorupMenuPOS.Data;
using KorupMenuPOS.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace KorupMenuPOS.ViewModel
{
    public class TimerViewModel : INotifyPropertyChanged
    {
        string chtimer = "00:00:00";

        string entry = string.Empty;
        string nameEntry = string.Empty;

        System.Timers.Timer timer;
        int hour, mintues, seconds;

        bool timerIsOff = true;

        bool nameIsTyped = true;

        
        
        public TimerViewModel()
        {
            


            timer = new System.Timers.Timer();

            timer.Interval = 1000;

            timer.Elapsed += OntimeEvent;

            

            StartTimerCommand = new Command(timer.Start);
            StopTimerCommand = new Command(timer.Stop);
            ResetTimerCommand = new Command(ResetTimer);
            SendTimeToLeaderBoardCommand = new Command(AddToLeaderBoard);
            

        }

        public ICommand StartTimerCommand { private set; get; }
        public ICommand StopTimerCommand { private set; get; }
        public ICommand ResetTimerCommand { private set; get; }
        public ICommand SendTimeToLeaderBoardCommand { private set; get; }
       

      
      
        
        void ResetTimer()
        {
            hour = 0;
            mintues = 0;
            seconds = 0;
            chtimer = "00:00:00";
            nameEntry = "";
            OnPropertyChanged(nameof(CHTimer));
            OnPropertyChanged(nameof(NameEntry));
        }
        private void OntimeEvent(object sender, ElapsedEventArgs e)
        {
            seconds++;
            if (seconds == 60)
            {
                seconds = 0;
                mintues++;
            }
            if (mintues == 60)
            {
                mintues = 0;
                hour++;
            }
            chtimer = string.Format($"{hour.ToString().PadLeft(2, '0')}:{mintues.ToString().PadLeft(2, '0')}:{seconds.ToString().PadLeft(2, '0')}");
            OnPropertyChanged(nameof(CHTimer));
        }

        public bool TimerIsOff
        {
            get { return timerIsOff; }
        }

        public bool NameIsTyped
        {
            get { return nameIsTyped; }
        }

        public string NameEntry
        {
            get { return nameEntry; }
            set {
                nameEntry = value;
               
            }       
                
        }

        //denne er den der bliver databindet til, derfor public
        public string CHTimer
        {
            //Denne gør at når der sker ændringer i chtimer værdien
            //Bliver det også vist i viewet
            get { return chtimer; }
            set
            {
                chtimer = value;
                
            }
        }

        async void AddToLeaderBoard()
        {
            string challengeTime = chtimer;
            string challengerName = nameEntry;


            await App.Database.AddTimeToLeaderBoard(challengeTime, challengerName);
           
        }

       


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
