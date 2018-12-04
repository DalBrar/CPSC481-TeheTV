using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TeheTV
{
    public partial class MainWindow : Window
    {
        // add all pages here
        private static Page createNewAccount;
        private static Page initialize;
        private static Page navigator;
        private static Page nowPlaying;
        private static Page options;
        private static Page parentSettings;
        private static Page profileSelector;
        private static Page splashScreen;

        public MainWindow()
        {
            InitializeComponent();
            SettingsManager.initialize();

            // instantiate all pages here
            createNewAccount = new pages.CreateNewAccount(this);
            initialize       = new pages.Initialize(this);
            navigator        = new pages.Navigator(this);
            nowPlaying       = new pages.NowPlaying(this);
            options          = new pages.Options(this);
            parentSettings   = new pages.ParentSettings(this);
            profileSelector  = new pages.ProfileSelector(this);
            splashScreen     = new pages.SplashPage(this);

            // this is the first screen that loads, do not change
            ScreenFrame.NavigationService.Navigate(splashScreen);
        }
        
        private void dragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        
        private void closeApp(object sender, MouseButtonEventArgs e)
        {
            Sounds.PlayNWait(Properties.Resources.soundButtonPress);
            System.Windows.Application.Current.Shutdown();
        }

        private void minimizeApp(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonPress);
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// This can be used from any page using the app variable: app.changeScreen(SCREEN.<Screen_you_desire>, bool fadeEffect);
        /// Use this to change the screen frame
        /// </summary>
        /// <param name="screen">SCREEN</param>
        /// <param name="fadeEffect">true/false</param>
        public void ScreenChangeTo(SCREEN screen, bool fadeEffect)
        {
            if (screen == SCREEN.CreateNewAccount)
                ScreenChangeTo(createNewAccount, fadeEffect);
            else if (screen == SCREEN.Initialize)
                ScreenChangeTo(initialize, fadeEffect);
            else if (screen == SCREEN.Navigator)
                ScreenChangeTo(navigator, fadeEffect);
            else if (screen == SCREEN.NowPlaying)
                ScreenChangeTo(nowPlaying, fadeEffect);
            else if (screen == SCREEN.Options)
                ScreenChangeTo(options, fadeEffect);
            else if (screen == SCREEN.ParentSettings)
                ScreenChangeTo(parentSettings, fadeEffect);
            else if (screen == SCREEN.ProfileSelector)
                ScreenChangeTo(profileSelector, fadeEffect);
        }
       
        /// <summary>
        /// This can be used from any page using the app variable:
        ///      app.changeScreen(SCREEN.<Screen_you_desire>, bool fadeEffect);
        /// </summary>
        /// <param name="nextPage">Page</param>
        /// <param name="fadeEffect">true/false</param>
        public void ScreenChangeTo(Page nextPage, bool fadeEffect)
        {
            changeScreen(nextPage, fadeEffect, false);
        }

        public void ScreenGoBack()
        {
            changeScreen(null, true, true);
        } 

        private void changeScreen(Page nextPage, bool fadeEffect, bool goBack)
        {
            _goBack = goBack;

            if (fadeEffect)
            {
                _nextPage = nextPage;
                frame_Navigating();
            }
            else
            {
                ScreenFrame.NavigationService.Navigate(nextPage);
            }
        }

        // *****************************************
        //  ScreenFrame Navigation Transition stuff
        // *****************************************
        private Page _nextPage;
        private DependencyProperty _effect = FrameworkElement.OpacityProperty;
        private double _depToChange = 0;
        private double _depOffset = 0;
        private Duration _durFadeOut = new Duration(TimeSpan.FromMilliseconds(100));
        private Duration _durFadeIn = new Duration(TimeSpan.FromMilliseconds(750));
        private bool _goBack = false;

        private void frame_Navigating()
        {
            if (Content != null)
            {
                _depToChange = ScreenFrame.Opacity;

                DoubleAnimation animation0 = new DoubleAnimation();
                animation0.From = _depToChange;
                animation0.To = _depOffset;
                animation0.Duration = _durFadeOut;
                animation0.Completed += SlideCompleted;
                ScreenFrame.BeginAnimation(_effect, animation0);
            }
        }

        private void SlideCompleted(object senderNotUsed, EventArgs eNotUsed)
        {
            if (_goBack)
                ScreenFrame.GoBack();
            else
                ScreenFrame.NavigationService.Navigate(_nextPage);

            Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate ()
                {
                    DoubleAnimation animation0 = new DoubleAnimation();
                    animation0.From = _depOffset;
                    animation0.To = _depToChange;
                    animation0.Duration = _durFadeIn;
                    ScreenFrame.BeginAnimation(_effect, animation0);
                });
        }
    }
}
