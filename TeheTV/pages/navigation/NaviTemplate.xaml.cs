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

namespace TeheTV.pages.navigation
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
                if (content == ContentType.RECOMMENDED) list = SettingsManager.getCurrentProfile().Recommendations;
                else list = SettingsManager.getCurrentProfile().getContentType(content);

                foreach (Content c in list)
                {
                    ContentButton button = new ContentButton(app, c);
                    contentArea.Children.Add(button);
                }
            }
        }

        private void BindGames()
        {

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
    }
}
