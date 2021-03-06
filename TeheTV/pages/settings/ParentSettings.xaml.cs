﻿using System;
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
            app.ScreenGoBack();
        }

        private void pinBtnPressed(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(new PinChange(app), true);
        }

        private void createProfileBtnPressed(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(SCREEN.CreateNewAccount, true);
        }

        private void deleteProfileBtnPressed(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(new DeleteAccount(app));
        }

        private void timeBtnPressed(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(new TimeAdjustment(app));
        }

        private void recommendBtnPressed(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(new Recommendations(app));
        }

        private void logBtnPressed(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(new History(app, 1), true);
        }

    }
}
