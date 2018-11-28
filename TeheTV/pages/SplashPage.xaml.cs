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

namespace TeheTV.pages
{
    public partial class SplashPage : Page
    {
        MainWindow app;
        SoundPlayer player;

        public SplashPage(MainWindow instance)
        {
            app = instance;
            InitializeComponent();

            Stream strm = Properties.Resources.TeheTVwav;
            player = new SoundPlayer(strm);
            player.Load();

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
            player.Play();
            Storyboard sb = this.FindResource("BGfadeOut") as Storyboard;
            sb.Completed += ExitSplash;
            sb.Begin();
        }

        private void ExitSplash(object sender, EventArgs e)
        {
            if (File.Exists("profiles.ini"))
                app.changeScreen(SCREEN.ProfileSelector);
            else
                app.changeScreen(SCREEN.Initialize);
        }
    }
}
