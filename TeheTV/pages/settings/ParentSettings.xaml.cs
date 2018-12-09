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

namespace TeheTV.Pages
{
    public partial class ParentSettings : Page
    {
        MainWindow app;
        //private Page option;
        //private Page profile;
        //private Page log;
        //private Page rec;
        //private Page time;

        public ParentSettings(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            //option = new Options(app);
            //profile = new ProfileAdjustment(app);
            //log = new History(app);
            //time = new TimeAdjustment(app);
        }

        private void backBtnPressed(object sender, MouseButtonEventArgs e)
        {
            playSound();
            app.ScreenGoBack();
        }

        private void createProfileBtnPressed(object sender, MouseButtonEventArgs e)
        {

        }

        private void recommendBtnPressed(object sender, MouseButtonEventArgs e)
        {

        }

        private void logBtnPressed(object sender, MouseButtonEventArgs e)
        {

        }

        private void timeBtnPressed(object sender, MouseButtonEventArgs e)
        {

        }

        private void playSound()
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
        }
    }
}
