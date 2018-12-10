using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TeheTV
{
    /// <summary>
    /// Interaction logic for TVScreen.xaml
    /// </summary>
    public partial class TVScreen : Window
    {
        public delegate void TVEventHandler(TVScreen tv);
        
        public event TVEventHandler PlayingEvent;
        public event TVEventHandler StoppedEvent;
        public event TVEventHandler OutOfTimeEvent;
        Content C;
        private bool canIStartPlaying = false;

        DispatcherTimer timer;

        public TVScreen(Content c)
        {
            C = c;
            InitializeComponent();
            this.Show();
            this.Focus();
            initializePlayer();
            wait1Second();
        }

        public void Update(Content c)
        {
            PlayingEvent = null;
            StoppedEvent = null;
            OutOfTimeEvent = null;
            timer.Stop();
            mePlayer.Stop();
            C = c;
            this.Focus();
            initializePlayer();
            wait1Second();
        }

        public void Play()
        {
            if (canIStartPlaying)
            {
                timer.Start();
                mePlayer.Play();
            }
        }

        public void Pause()
        {
            canIStartPlaying = false;
            timer.Stop();
            mePlayer.Pause();
        }

        public void Stop()
        {
            canIStartPlaying = false;
            MainWindow.TvScreen = null;
            timer.Stop();
            mePlayer.Stop();
            this.Close();
        }
        
        protected virtual void ExecutePlayingEvent() { if (PlayingEvent != null) PlayingEvent(this); }
        protected virtual void ExecuteStoppedEvent() { if (StoppedEvent != null) StoppedEvent(this); }
        protected virtual void ExecuteOutOfTimeEvent() { if (OutOfTimeEvent != null) OutOfTimeEvent(this); }

        private void initializePlayer()
        {
            canIStartPlaying = false;
            if (timer != null) timer.Stop();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTick;
        }

        private void timerTick(object sender, EventArgs e)
        {
            Profile P = SettingsManager.getCurrentProfile();
            if (P == null)
                Stop();
            else
            {
                if (P.hasTime())
                {
                    P.ReduceTime();
                    ExecutePlayingEvent();
                }
                else
                {
                    canIStartPlaying = false;
                    ExecuteOutOfTimeEvent();
                    Stop();
                }
            }
        }

        private void stopButtonPressed(object sender, MouseButtonEventArgs e)
        {
            canIStartPlaying = false;
            Sounds.Play(Properties.Resources.soundButtonPress);
            ExecuteStoppedEvent();
            Stop();
        }

        private void wait1Second()
        {
            DoubleAnimation a = new DoubleAnimation();
            a.Duration = TimeSpan.FromSeconds(1);
            a.From = 1;
            a.To = 1;
            a.Completed += startSplash;
            TVScreenGrid.BeginAnimation(Grid.OpacityProperty, a);
        }

        private void startSplash(object s, EventArgs e) { this.Focus(); splashAndPlay(); }
        private void splashAndPlay()
        {
            canIStartPlaying = true;
            splashLogo.Source = C.Watermark;
            mePlayer.Opacity = 0.0;
            DoubleAnimation a = new DoubleAnimation();
            a.Duration = TimeSpan.FromSeconds(5);
            a.From = 1;
            a.To = 0;
            a.Completed += startPlaying;
            splashGrid.BeginAnimation(Grid.OpacityProperty, a);
        }
        private void startPlaying(object s, EventArgs e)
        {
            splashGrid.Opacity = 0;
            mePlayer.Source = new Uri(C.ContentPath, UriKind.RelativeOrAbsolute);
            mePlayer.Opacity = 1.0;
            Play();
        }

        private void dragMove(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
