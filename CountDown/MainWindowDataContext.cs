using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CountDown
{
    public class MainWindowDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Windows title
        private string winTitle;
        public string WinTitle
        {
            get { return winTitle; }
            set { if (value != winTitle) winTitle = value; this.OnPropertyChanged("WinTitle"); }
        }

        // Initial count down value
        private string initialCountDown;
        public string InitialCountDown
        {
            get { return initialCountDown; }
            set { if (value != initialCountDown) initialCountDown = value; this.OnPropertyChanged("IniitalCountDown"); }
        }

        // Number of remaining seconds
        private string countDown;
        public string CountDown
        {
            get { return countDown;}
            set { if (value !=countDown) countDown=value; this.OnPropertyChanged("CountDown"); }
        }

        // Initial Window's size
        private string winHeight;
        public string WinHeight
        {
            get { return winHeight; }
            set { winHeight = value; this.OnPropertyChanged("WinHeight"); }
        }
        private string winWidth;
        public string WinWidth
        {
            get { return winWidth; }
            set { winWidth = value; this.OnPropertyChanged("WinWidth"); }
        }

        // Windows background color
        private string winBackground;
        public string WinBackground
        {
            get { return winBackground; }
            set { if (value != winBackground) winBackground = value; this.OnPropertyChanged("WinBackground"); }
        }

        // The timer
        private DispatcherTimer dispatcherTimer;
        public DispatcherTimer DispatcherTimer
        {
            get { return dispatcherTimer; }
            set {dispatcherTimer = value; this.OnPropertyChanged("DispatcherTimer"); }
        }

        // True if it is the first timer's tick
        private bool isFirstTick;
        public bool IsFirstTick
        {
            get { return isFirstTick; }
            set { isFirstTick = value; this.OnPropertyChanged("IsFirstTick"); }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
