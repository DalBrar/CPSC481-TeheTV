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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TeheTV
{
    public partial class GameButton : UserControl
    {
        private MainWindow app;
        private Page game;

        public GameButton(MainWindow instance, Page gamePage, string gameTitle, string thumbPath)
        {
            app = instance;
            game = gamePage;
            InitializeComponent();

            Thumbnail.Source = getImgSource(thumbPath);
            Label.Content = gameTitle;
        }

        private void buttonClick(object sender, MouseButtonEventArgs e)
        {
            if (SettingsManager.getCurrentProfile().hasTime())
            {
                Sounds.Play(Properties.Resources.soundButtonPress);
                app.ScreenChangeTo(game, true);
            }
            else
            {
                Sounds.Play(Properties.Resources.soundAlert);
            }
        }

        private static BitmapImage getImgSource(string path)
        {
            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
            return new BitmapImage(uri);
        }
    }
}
