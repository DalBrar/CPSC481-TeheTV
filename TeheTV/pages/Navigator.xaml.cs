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
    }
}
