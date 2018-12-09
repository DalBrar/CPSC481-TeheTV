using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace TeheTV.Pages
{
    public partial class Options : Page
    {
        MainWindow app;

        public Options(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
        }

        private void backBtnPressed(object sender, MouseButtonEventArgs e)
        {
            app.ScreenGoBack();
        }

        private void profilesBtnPressed(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(SCREEN.ProfileSelector, true);
        }

        private void parentSettingsBtnPressed(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(SCREEN.ParentSettings, true);
        }
    }
}
