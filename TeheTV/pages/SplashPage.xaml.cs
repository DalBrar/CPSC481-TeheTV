using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TeheTV.Pages
{
    public partial class SplashPage : Page
    {
        MainWindow app;

        public SplashPage(MainWindow instance)
        {
            app = instance;
            InitializeComponent();

            this.Loaded += animateSplash;
        }

        private void animateSplash(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("BGfadeIn") as Storyboard;
            sb.Completed += FadeOut;
            sb.Begin();
        }

        private void FadeOut(object sender, EventArgs e)
        {
            Sounds.Play(Properties.Resources.TeheTVwav);
            Storyboard sb = this.FindResource("BGfadeOut") as Storyboard;
            sb.Completed += ExitSplash;
            sb.Begin();
        }

        private void ExitSplash(object sender, EventArgs e)
        {
            if (SettingsManager.doesPINexist())
            {
                if (SettingsManager.doProfilesExist())
                    app.ScreenChangeTo(SCREEN.ProfileSelector, true);
                else
                    app.ScreenChangeTo(SCREEN.CreateNewAccount, true);
            }
            else
                app.ScreenChangeTo(SCREEN.Initialize, true);
        }
    }
}
