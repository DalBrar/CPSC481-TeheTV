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
        public delegate void TVEventHandler();

        public event TVEventHandler PlayingEvent;
        public event TVEventHandler StoppedEvent;
        public event TVEventHandler OutOfTimeEvent;
        Profile P;
        Content C;

        DispatcherTimer timer;

        public TVScreen(Profile p, Content c)
        {
            P = p;
            C = c;
            InitializeComponent();
            this.Show();
            getFocus();
            initializePlayer();
            splashAndPlay();
        }

        public void Update(Profile p, Content c)
        {
            PlayingEvent = null;
            StoppedEvent = null;
            OutOfTimeEvent = null;
            timer.Stop();
            mePlayer.Stop();
            P = p;
            C = c;
            getFocus();
            initializePlayer();
            splashAndPlay();
        }

        public void Play()
        {
            timer.Start();
            mePlayer.Play();
        }

        public void Pause()
        {
            timer.Stop();
            mePlayer.Pause();
        }

        public void Stop()
        {
            MainWindow.TvScreen = null;
            timer.Stop();
            mePlayer.Stop();
            this.Close();
        }

        protected virtual void ExecutePlayingEvent() { if (PlayingEvent != null) PlayingEvent(); }
        protected virtual void ExecuteStoppedEvent() { if (StoppedEvent != null) StoppedEvent(); }
        protected virtual void ExecuteOutOfTimeEvent() { if (OutOfTimeEvent != null) OutOfTimeEvent(); }

        private void initializePlayer()
        {
            if (timer != null) timer.Stop();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(60);
            timer.Tick += timerTick;
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (P.hasTime())
            {
                P.ReduceTime();
                ExecutePlayingEvent();
            } else
            {
                ExecuteOutOfTimeEvent();
                Stop();
            }
        }

        private void stopButtonPressed(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonPress);
            ExecuteStoppedEvent();
            Stop();
        }

        private void splashAndPlay()
        {
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

        private void getFocus()
        {
            int i = 0;
            while (!this.IsFocused && i < 5000)
            {
                this.Focus();
                i++;
            }
        }
    }
}
