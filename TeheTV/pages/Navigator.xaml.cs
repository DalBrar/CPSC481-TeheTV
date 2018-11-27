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
        /// This can be used statically from any page as long as you pass it the "navi" instance variable.
        /// Use this to change the Navigation from the home row buttons
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="navi"></param>
        public static void changeNavigationFrame(MainWindow instance, NAVI navi)
        {
            if (navi == NAVI.Recommendations)
                instance.ScreenFrame.NavigationService.Navigate(template);
            if (navi == NAVI.YouTube)
                instance.ScreenFrame.NavigationService.Navigate(template);
            if (navi == NAVI.Netflix)
                instance.ScreenFrame.NavigationService.Navigate(template);
            if (navi == NAVI.Spotify)
                instance.ScreenFrame.NavigationService.Navigate(template);
            if (navi == NAVI.Games)
                instance.ScreenFrame.NavigationService.Navigate(template);
        }
    }
}
