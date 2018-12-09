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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static TeheTV.Keyboard;

namespace TeheTV.Pages
{
    public partial class Options : Page
    {
        MainWindow app;

        public Options(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            passwordGrid.Visibility = Visibility.Hidden;

            keyboard.ReturnKeyText = "Enter";
            keyboard.EmptySpaceReturn = true;
            keyboard.ReturnEvent += new CustomKeyboardEventHandler(checkPIN);
            keyboard.MaxInputLength = 4;
            keyboard.OutputTextBlock = passwordField;
            keyboard.IsPassword = true;
            keyboard.ClearValuesOnSlideUp = true;
            keyboard.keyboardStyle = Keyboard.KeyboardStyle.NUMERICAL;
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
            keyboard.SlideUp();
            passwordGrid.Visibility = Visibility.Visible;
        }

        private void checkPIN(string output)
        {
            passwordGrid.Visibility = Visibility.Hidden;

            if (SettingsManager.doesPINmatch(output))
            {
                Sounds.Play(Properties.Resources.soundPassCorrect);
                app.ScreenChangeTo(SCREEN.ParentSettings, true);
            } else
            {
                Sounds.Play(Properties.Resources.soundPassWrong);

            }

        }

        /*
        public void AnimatePINBox()
        {
            passwordRec.Stroke = MainWindow.setBrushColor(255, 255, 0, 0);

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.Duration = TimeSpan.FromMilliseconds(_slideMiliSecs);
            animation.From = new Thickness(0, 0, 0, 0);
            animation.To = new Thickness(0, _slidingAmount, 0, 0);
            playSound(true);
            _slidingGrid.BeginAnimation(Grid.MarginProperty, animation);
        }*/
    }
}
