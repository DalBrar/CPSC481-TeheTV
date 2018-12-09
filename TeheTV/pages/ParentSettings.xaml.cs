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
    public partial class ParentSettings : Page
    {
        MainWindow app;
        private Page option;
        private Page profile;
        private Page log;
        private Page rec;
        private Page time;

        public ParentSettings(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            option = new Options(app);
            profile = new ProfileAdjustment(app);
            log = new History(app);
            //rec
            time = new TimeAdjustment(app);


        }

        private void ReturnButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(option, true);
        }

        private void ProfileButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(profile, true);
        }

        private void LogButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(log, true);
        }

        private void RecButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void TimeResButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(time, true);
        }
    }
}
