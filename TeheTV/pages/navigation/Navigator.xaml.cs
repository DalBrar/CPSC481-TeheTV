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
using TeheTV.Pages.navigation;

namespace TeheTV.Pages
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
            template = new NaviTemplate(app, this, ContentType.NETFLIX);

            recTime.Stroke = MainWindow.setBrushColor(255, 0, 255, 255);

            changeNavigationFrame(NAVI.Recommendations);
            gridSearchBar.Visibility = Visibility.Hidden;

            keyboard.ReturnKeyText = "Search";
            keyboard.EmptySpaceReturn = true;
            keyboard.ReturnEvent += new EventHandler(hideSearchBar);
            keyboard.TypeEvent += new EventHandler(doSearch);
            keyboard.MaxInputLength = 30;
            keyboard.OutputTextBlock = fieldSearchBar;
            keyboard.keyboardStyle = Keyboard.KeyboardStyle.ALL;
        }

        /// <summary>
        /// Use this to change the Navigation from the home row buttons
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="navi"></param>
        private void changeNavigationFrame(NAVI navi)
        {
            if (navi == NAVI.Recommendations)
                naviFrame.NavigationService.Navigate(new NaviTemplate(app, this, ContentType.RECOMMENDED));
            if (navi == NAVI.Netflix)
                naviFrame.NavigationService.Navigate(new NaviTemplate(app, this, ContentType.NETFLIX));
            if (navi == NAVI.Spotify)
                naviFrame.NavigationService.Navigate(new NaviTemplate(app, this, ContentType.SPOTIFY));
            if (navi == NAVI.YouTube)
                naviFrame.NavigationService.Navigate(new NaviTemplate(app, this, ContentType.YOUTUBE));
            if (navi == NAVI.Games)
                naviFrame.NavigationService.Navigate(new NaviTemplate(app, this, ContentType.GAMES));
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
            changeNavigationFrame(NAVI.Recommendations);
        }

        private void clickYoutube(object sender, MouseButtonEventArgs e)
        {
            selectMenuItem(sender);
            changeNavigationFrame(NAVI.YouTube);
        }

        private void clickNetflix(object sender, MouseButtonEventArgs e)
        {
            selectMenuItem(sender);
            changeNavigationFrame(NAVI.Netflix);
        }

        private void clickSpotify(object sender, MouseButtonEventArgs e)
        {
            selectMenuItem(sender);
            changeNavigationFrame(NAVI.Spotify);
        }

        private void clickGames(object sender, MouseButtonEventArgs e)
        {
            selectMenuItem(sender);
            changeNavigationFrame(NAVI.Games);
        }

        private void clickSearch(object sender, MouseButtonEventArgs e)
        {
            playButtonClick();
            showSearchBar();
            keyboard.SlideUp();
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
            recSearch.Stroke = MainWindow.setBrushColor(255, 0, 0, 0);
        }

        private void playButtonClick() { Sounds.Play(Properties.Resources.soundButtonClick); }


        private void showSearchBar()
        {
            gridSearchBar.Visibility = Visibility.Visible;
        }
        private void hideSearchBar(object sender, EventArgs e) { hideSearchBar(); }
        private void hideSearchBar()
        {
            gridSearchBar.Visibility = Visibility.Hidden;
        }

        private void doSearch(object sender, EventArgs e) { doSearch(); }
        private void doSearch()
        {
            deslectAllMenuItems();
            recSearch.Stroke = MainWindow.setBrushColor(255, 0, 255, 255);
            string searchText = fieldSearchBar.Text;
            naviFrame.NavigationService.Navigate(new NaviTemplate(app, this, searchText));
        }
    }
}
