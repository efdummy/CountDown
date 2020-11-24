using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;dépassement
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

/// <summary>
/// Count down
/// 
/// Count down is usefull to control the speech time in a conference where slides and image are displayed.
/// Just start CountDonw.exe and it will display discreetly on the screen the remaining seconds for your speech.
/// You can let CountDown.exe in the Background. When the time is over, it will go back in the foreground (one time) and display a red background.
/// It also display the number of seconds of the exeeding time.
/// 
/// Display a small Window with a running count down
/// - Count down starting value is given by the file name (for example 240.exe for 240 seconds which is 4 minutes)
/// - Change the exe file name (rename the file) if you need a different starting value
/// - By default (if the shape of exe filename is not <integer>.exe), the count down will be of 10 seconds
/// - When the count down is over, the backgroung of the Windows becomes red
/// </summary>

namespace CountDown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string BackgroundColorInitial = "DodgerBlue";
        const string BackgroundColorItsOver = "Firebrick";
        const string InitialWidth = "200";
        const string InitialHeight = "100";

        public MainWindow()
        {
            InitializeComponent();

            // Find the number of seconds for starting the count down
            MainWindowDataContext ctx = new MainWindowDataContext();
            string exePath=System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            // File name must be a string representing an integer (number of seconds for the count down)
            string intFileName=System.IO.Path.GetFileNameWithoutExtension(exePath);
            int countDownStartValue;
            bool success=int.TryParse(intFileName, out countDownStartValue);

            // Set Count down value
            if (success)
            {
                ctx.InitialCountDown = intFileName;
                ctx.CountDown = intFileName;
            }
            else
            {
                countDownStartValue = 10; // Default starting value (10 seconds)
                ctx.InitialCountDown= countDownStartValue.ToString();
                ctx.CountDown = countDownStartValue.ToString();
            }

            // Set Windows title
            ctx.WinTitle = string.Format("{0} count down", ctx.InitialCountDown);

            // Set initial background color and size
            ctx.WinBackground = BackgroundColorInitial;
            ctx.WinWidth = InitialWidth;
            ctx.WinHeight = InitialHeight;

            // Start the timer and set it in the Window's context
            ctx.DispatcherTimer = new DispatcherTimer();
            ctx.DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            ctx.DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            ctx.DispatcherTimer.Start();
            ctx.IsFirstTick = true;

            // Memorize the Window's context
            this.DataContext = ctx;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Bring Window to the foreground and activate it only the first time or when time is over
            if (((MainWindowDataContext)this.DataContext).IsFirstTick)
            {
                ((MainWindowDataContext)this.DataContext).IsFirstTick = false;
                Activate();
                SystemSounds.Beep.Play();
            }

            // Get count down current value
            string scount =((MainWindowDataContext)this.DataContext).CountDown;
            // Convert to int and decrease
            int count=int.Parse(scount);
            count--;
            //Convert new count value to string and set the context
            scount=count.ToString();
            ((MainWindowDataContext)this.DataContext).CountDown = scount;

            // Is it the end?
            if (count == 0) ProcessItsOver(); 

        }

        private void ProcessItsOver()
        {
            // Set background to red
            ((MainWindowDataContext)this.DataContext).WinBackground = BackgroundColorItsOver;

            // Restore the window and its size if necessary
            int currentWidth = int.Parse(((MainWindowDataContext)this.DataContext).WinWidth);
            int currentHeight = int.Parse(((MainWindowDataContext)this.DataContext).WinHeight);
            if (currentWidth<int.Parse(InitialWidth))
                ((MainWindowDataContext)this.DataContext).WinWidth = InitialWidth;
            if (currentHeight<int.Parse(InitialHeight))
                ((MainWindowDataContext)this.DataContext).WinHeight = InitialHeight;
            if (this.WindowState == WindowState.Minimized) this.WindowState = WindowState.Normal;

            // Bring Window to the foreground and Beep
            ((MainWindowDataContext)this.DataContext).IsFirstTick = true;

        }
    }
}
