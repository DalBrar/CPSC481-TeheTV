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

namespace TeheTV.Pages.navigation
{
    // this template should be used to create Recommended, Youtube, Netflix, Spotify, Games pages.
    public partial class NaviTemplate : Page
    {
        private MainWindow app;
        private Navigator navi;
        private ContentType content;
        private bool customSearch = false;
        private string searchText = "";

        public NaviTemplate(MainWindow instance, Navigator navigator, string query)
        {
            app = instance;
            navi = navigator;
            content = ContentType.SEARCH;
            customSearch = true;
            searchText = query.ToLower();

            InitializeComponent();
            textHEADER.Text = getTitleText();
            BindContent();
        }
        public NaviTemplate(MainWindow instance, Navigator navigator, ContentType type)
        {
            app = instance;
            navi = navigator;
            content = type;

            InitializeComponent();
            textHEADER.Text = getTitleText();
            BindContent();
        }

        private void BindContent()
        {
            if (SettingsManager.getCurrentProfile() == null) return;
            if (content == ContentType.GAMES)
            {
                BindGames();
                return;
            }

            if (customSearch)
            {
                List<Content> master = SettingsManager.getCurrentProfile().getContentForProfile();
                
                foreach (Content c in master)
                {
                    string t = c.Title;
                    if (t.ToLower().Contains(searchText))
                    {
                        ContentButton button = new ContentButton(app, c);
                        contentArea.Children.Add(button);
                    }
                }
            }
            else
            {
                List<Content> list;
                bool recommended;
                if (content == ContentType.RECOMMENDED)
                {
                    list = SettingsManager.getCurrentProfile().Recommendations;
                    recommended = true;
                }
                else
                {
                    list = SettingsManager.getCurrentProfile().getContentType(content);
                    recommended = false;
                }

                foreach (Content c in list)
                {
                    ContentButton button = new ContentButton(app, c, recommended);
                    contentArea.Children.Add(button);
                }
            }
        }

        private void BindGames()
        {
            GameButton mathGame = new GameButton(app, new MathGame(app), "Math Game", "/resources/thumb_mathgame.png");
            GameButton ticTacToe = new GameButton(app, new TicTacToe(app), "Tic Tac Toe", "/resources/thumb_tictactoe.png");
            contentArea.Children.Add(mathGame);
            contentArea.Children.Add(ticTacToe);
        }

        private string getTitleText()
        {
            if (content == ContentType.RECOMMENDED)
                return "Recommendations";
            if (content == ContentType.YOUTUBE)
                return "YouTube";
            if (content == ContentType.NETFLIX)
                return "Netflix";
            if (content == ContentType.SPOTIFY)
                return "Spotify";
            if (content == ContentType.GAMES)
                return "Games";
            if (content == ContentType.SEARCH)
                return "Search Results";
            return "";
        }

        private void gearBtnPressed(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            app.ScreenChangeTo(SCREEN.Options, true);
        }

        // Scrolling methods
        Point scrollMousePoint = new Point();
        double hOff = 1;

        private void ScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollViewer);
            hOff = scrollViewer.HorizontalOffset;
            scrollViewer.CaptureMouse();
        }

        private void ScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollViewer.IsMouseCaptured)
            {
                scrollViewer.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(scrollViewer).X));
            }
        }

        private void ScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.ReleaseMouseCapture();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + e.Delta);
        }
    }
}
