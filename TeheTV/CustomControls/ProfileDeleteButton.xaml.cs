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
using TeheTV.Pages;

namespace TeheTV
{
    /// <summary>
    /// Interaction logic for ProfileDeleteButton.xaml
    /// </summary>
    public partial class ProfileDeleteButton : UserControl
    {
        public delegate void DeleteButtonEventHandler(ProfileDeleteButton button, Profile profile);

        public event DeleteButtonEventHandler ButtonDownEvent;
        private MainWindow app;
        private Profile profile;

        public ProfileDeleteButton(MainWindow instance, Profile p)
        {
            app = instance;
            profile = p;

            InitializeComponent();
            Label.Content = p.Name;
        }

        private void buttonClick(object sender, MouseButtonEventArgs e)
        {
            animateButton();
            if (ButtonDownEvent != null) ButtonDownEvent(this, profile);
        }

        // ************************
        //  Button Click Animation
        // ************************
        private static Int32 _slideMiliSecs = 250;

        public void animateButton()
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (ThreadStart)delegate () { changeGradient0(); });
            changeGradient1();
        }

        private void changeGradient0()
        {
            ColorAnimation animation = new ColorAnimation();
            animation.Duration = TimeSpan.FromMilliseconds(_slideMiliSecs);
            animation.From = Color.FromArgb(255, 198, 86, 112);
            animation.To = Color.FromArgb(255, 199, 0, 46);
            animation.AutoReverse = true;
            GradientStop0.BeginAnimation(GradientStop.ColorProperty, animation);
        }

        private void changeGradient1()
        {
            ColorAnimation animation = new ColorAnimation();
            animation.Duration = TimeSpan.FromMilliseconds(_slideMiliSecs);
            animation.From = Color.FromArgb(255, 147, 63, 83);
            animation.To = Color.FromArgb(255, 147, 0, 35);
            animation.AutoReverse = true;
            GradientStop1.BeginAnimation(GradientStop.ColorProperty, animation);
        }
    }
}
