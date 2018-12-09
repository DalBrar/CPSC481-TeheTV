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
using TeheTV.pages;

namespace TeheTV
{
    /// <summary>
    /// Interaction logic for ProfileButton.xaml
    /// </summary>
    public partial class ProfileButton : UserControl
    {
        private MainWindow app;
        private Profile profile;

        public ProfileButton(MainWindow instance, Profile p)
        {
            app = instance;
            profile = p;

            InitializeComponent();
            Label.Content = p.Name;
        }

        private void buttonClick(object sender, MouseButtonEventArgs e)
        {
            animateButton();
            SettingsManager.setCurrentProfile(profile);
            app.ScreenChangeTo(SCREEN.Navigator, true);
        }

        // ************************
        //  Button Click Animation
        // ************************
        private static Int32 _slideMiliSecs = 250;

        public void animateButton()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (ThreadStart)delegate () { changeGradient0(); });
            changeGradient1();
        }

        private void changeGradient0()
        {
            ColorAnimation animation = new ColorAnimation();
            animation.Duration = TimeSpan.FromMilliseconds(_slideMiliSecs);
            animation.From = Color.FromArgb(255, 112, 198, 86);
            animation.To = Color.FromArgb(255, 46, 199, 0);
            animation.AutoReverse = true;
            playSound();
            GradientStop0.BeginAnimation(GradientStop.ColorProperty, animation);
        }

        private void changeGradient1()
        {
            ColorAnimation animation = new ColorAnimation();
            animation.Duration = TimeSpan.FromMilliseconds(_slideMiliSecs);
            animation.From = Color.FromArgb(255, 83, 147, 63);
            animation.To = Color.FromArgb(255, 35, 147, 0);
            animation.AutoReverse = true;
            playSound();
            GradientStop1.BeginAnimation(GradientStop.ColorProperty, animation);
        }

        private static void playSound()
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
        }
    }
}
