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

namespace TeheTV.pages
{
    public partial class Navigator : Page
    {
        private MainWindow app;

        // add all navigation pages here
        private static Page template;

        public Navigator(MainWindow instance)
        {
            app = instance;
            InitializeComponent();

            // instantiate all pages here
            template = new pages.navigation.NaviTemplate(this);

            recTime.Stroke = MainWindow.setBrushColor(255, 0, 255, 255);
        }

        /// <summary>
        /// Use this to change the Navigation from the home row buttons
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="navi"></param>
        public void changeNavigationFrame(NAVI navi)
        {
            if (navi == NAVI.Recommendations)
                naviFrame.NavigationService.Navigate(template);
            if (navi == NAVI.YouTube)
                naviFrame.NavigationService.Navigate(template);
            if (navi == NAVI.Netflix)
                naviFrame.NavigationService.Navigate(template);
            if (navi == NAVI.Spotify)
                naviFrame.NavigationService.Navigate(template);
            if (navi == NAVI.Games)
                naviFrame.NavigationService.Navigate(template);
        }

        public enum NAVI
        {
            Recommendations,
            YouTube,
            Netflix,
            Spotify,
            Games
        }

        private void clickTime(object sender, MouseButtonEventArgs e)
        {
            selectMenuItem(sender);
        }

        private void clickYoutube(object sender, MouseButtonEventArgs e)
        {
            selectMenuItem(sender);
        }

        private void clickNetflix(object sender, MouseButtonEventArgs e)
        {
            selectMenuItem(sender);
        }

        private void clickSpotify(object sender, MouseButtonEventArgs e)
        {
            selectMenuItem(sender);
        }

        private void clickGames(object sender, MouseButtonEventArgs e)
        {
            selectMenuItem(sender);
        }

        private void clickSearch(object sender, MouseButtonEventArgs e)
        {

        }

        // ******************
        //  Helper Functions
        // ******************
        private void selectMenuItem(object source)
        {
            string name = ((Image) source).Name;
            Rectangle rec;
            if (name.Equals("imgYoutube")) rec = recYoutube;
            else if (name.Equals("imgNetflix")) rec = recNetflix;
            else if (name.Equals("imgSpotify")) rec = recSpotify;
            else if (name.Equals("imgGames")) rec = recGames;
            else rec = recTime;

            deslectAllMenuItems();
            rec.Stroke = MainWindow.setBrushColor(255, 0, 255, 255);
            playButtonClick();
        }
        private void deslectAllMenuItems()
        {
            recTime.Stroke    = MainWindow.setBrushColor(255, 0, 0, 0);
            recYoutube.Stroke = MainWindow.setBrushColor(255, 0, 0, 0);
            recNetflix.Stroke = MainWindow.setBrushColor(255, 0, 0, 0);
            recSpotify.Stroke = MainWindow.setBrushColor(255, 0, 0, 0);
            recGames.Stroke   = MainWindow.setBrushColor(255, 0, 0, 0);
        }

        private void playButtonClick() { Sounds.Play(Properties.Resources.soundButtonClick); }
    }
}
