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
using static TeheTV.MainWindow;

namespace TeheTV
{
    public partial class GameButton : UserControl
    {
        public event TeheTVEvent NotEnoughTimeEvent;
        private MainWindow app;
        private Page game;
        private bool isRecommended;

        public GameButton(MainWindow instance, Page gamePage, string gameTitle, string thumbPath) : this(instance, gamePage, gameTitle, thumbPath, false) { }
        public GameButton(MainWindow instance, Page gamePage, string gameTitle, string thumbPath, bool recommended)
        {
            app = instance;
            game = gamePage;
            isRecommended = recommended;

            InitializeComponent();

            Thumbnail.Source = getImgSource(thumbPath);
            Label.Content = gameTitle;
        }

        private void throwNotEnoughTimeEvent()
        {
            if (NotEnoughTimeEvent != null) NotEnoughTimeEvent();
        }

        private void buttonClick(object sender, MouseButtonEventArgs e)
        {
            Profile p = SettingsManager.getCurrentProfile();
            if (p != null && (isRecommended || p.spendStars(SettingsManager.getStarCostAmount()) == true))
            {
                Sounds.Play(Properties.Resources.soundButtonPress);
                app.ScreenChangeTo(game, true);
            }
            else
            {
                Sounds.Play(Properties.Resources.soundAlert);
                throwNotEnoughTimeEvent();
            }
        }

        private static BitmapImage getImgSource(string path)
        {
            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
            return new BitmapImage(uri);
        }
    }
}
