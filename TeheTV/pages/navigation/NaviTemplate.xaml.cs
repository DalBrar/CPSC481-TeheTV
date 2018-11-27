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
        private Navigator navi;
        public NaviTemplate(Navigator instance)
        {
            navi = instance;
            InitializeComponent();

            // havent figured out what the size of the navigation frame and home buttons grid should be yet
        }
    }
}
