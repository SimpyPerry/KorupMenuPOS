using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace KorupMenuPOS.ViewModel
{
    public class TimerViewModel : INotifyPropertyChanged
    {
        string chtimer = "00:00:00";

        System.Timers.Timer timer;
        int hour, mintues, seconds;

        public TimerViewModel()
        {
            timer = new System.Timers.Timer();

            timer.Interval = 1000;

            timer.Elapsed += OntimeEvent;


            startTimerCommand = new Command(StartTimer);
            stopTimerCommand = new Command(StopTimer);
            ResetTimerCommand = new Command(ResetTimer);

        }

        public ICommand startTimerCommand { get; }
        public ICommand stopTimerCommand { get; }
        public ICommand ResetTimerCommand { get; }

        void StartTimer()
        {
            timer.Start();
        }
        void StopTimer()
        {
            timer.Stop();
        }
        void ResetTimer()
        {
            hour = 0;
            mintues = 0;
            seconds = 0;
            chtimer = "00:00:00";
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
        }

        //denne er den der bliver databindet til, derfor public
        public string CHTimer
        {
            //Denne gør at når der sker ændringer i chtimer værdien
            //Bliver det også vist i viewet
            get => chtimer;
            set
            {
               
                if (chtimer == value)
                    return;
                chtimer = value;
                OnPropertyChanged(nameof(CHTimer));
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string chtimer)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(chtimer));
        }
    }
}
