using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App2.ViewModel
{
    class TabataViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        private int reps;
        public int Reps {
            get { return reps; }
            set
            {
                if (value != reps)
                {
                    reps = value;
                    OnPropertyChanged("Reps");
                }
            } }
        private int exerciseTime;
        public int ExcerciseTime
        {
            get { return exerciseTime; }
            set
            {
                if (value != exerciseTime)
                {
                    exerciseTime = value;
                    OnPropertyChanged("ExcerciseTime");
                }
            }
        }
        private int breakTime;
        public int BreakTime
        {
            get { return breakTime; }
            set
            {
                if (value != breakTime)
                {
                    breakTime = value;
                    OnPropertyChanged("BreakTime");
                }
            }
        }
        private DateTime timer;
        private DateTime startTime;
        private bool _continueTimer;
        private TimeSpan time;
        public TimeSpan Time {
            get { return time; }
            protected set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged("Time");
                }
            } }

        public ICommand Start { get; set; }
        public ICommand Stop { get; set; }

        public TabataViewModel()
        {
            Start = new Command(() => { StartTimer(); });
            Stop = new Command(() => { StopTimer(); });
        }

        private void StopTimer()
        {
            _continueTimer = false;
        }

        private void StartTimer()
        {
            _continueTimer = true;
            startTime = DateTime.Now;
            Device.StartTimer(TimeSpan.FromSeconds(1), () => TimerCallback());
        }

        private bool TimerCallback()
        {
            timer = DateTime.Now;
            Time = timer - startTime;
            if (Time.Seconds >= Reps*breakTime*ExcerciseTime)
            {
                StopTimer();
                System.Diagnostics.Debug.WriteLine("stop");
            }
            else if (Time.Seconds % (breakTime * ExcerciseTime) == 0)
            {
                System.Diagnostics.Debug.WriteLine("break");
            }

            return  _continueTimer;
        }
    }
}
