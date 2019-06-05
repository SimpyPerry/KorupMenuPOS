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


        public string NameEntry { get; set; }
        public string NamePlaceholder { get; set; } = "Navn";


        System.Timers.Timer timer;
        int hour, mintues, seconds;

        private bool timerIsOff;
        public bool TimerIsOff
        {
            get { return timerIsOff; }
            set
            {
                timerIsOff = value;
                OnPropertyChanged(nameof(TimerIsOff));
            }
        }

        public TimerViewModel()
        {
            

            timer = new System.Timers.Timer();

            timer.Interval = 1000;

            timer.Elapsed += OntimeEvent;

            StartTimerCommand = new Command(timer.Start);
            StopTimerCommand = new Command(StopTimer);
            
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
            NamePlaceholder = "Navn";
            NameEntry = null;
            OnPropertyChanged(nameof(CHTimer));
            OnPropertyChanged(nameof(NameEntry));
            OnPropertyChanged(nameof(NamePlaceholder));
        }
        private void OntimeEvent(object sender, ElapsedEventArgs e)
        {
            TimerIsOff = false;
            

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

        private void StopTimer()
        {
            timer.Stop();
            TimerIsOff = true;
        }



        //denne er den der bliver databindet til, derfor public
        string chtimer = "00:00:00";
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

            if (NameEntry != null || NameEntry != "" && seconds > 0)
            {

                await App.Database.AddTimeToLeaderBoard(challengeTime, NameEntry);
            }
            else
            {
                NameEntry = null;
                NamePlaceholder = "Prøv igen";
            }

           
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
