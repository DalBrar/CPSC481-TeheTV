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
using TeheTV.Animations;
using static TeheTV.Animations.Wiggle;
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
            if (string.IsNullOrEmpty(output) || string.IsNullOrWhiteSpace(output))
            {
                passwordGrid.Visibility = Visibility.Hidden;
                return;
            }

            int pin = int.Parse(output);
            if (SettingsManager.doesPINmatch(pin))
            {
                passwordGrid.Visibility = Visibility.Hidden;
                Sounds.Play(Properties.Resources.soundPassCorrect);
                app.ScreenChangeTo(SCREEN.ParentSettings, true);
            } else
            {
                Sounds.Play(Properties.Resources.soundPassWrong);
                passwordRec.Stroke = MainWindow.setBrushColor(255, 255, 0, 0);
                Wiggle.Completed += new CustomAnimationEvent(wiggleDone);
                Wiggle.Run(passwordGrid);
            }
        }

        private void wiggleDone()
        {
            passwordRec.Stroke = MainWindow.setBrushColor(255, 0, 0, 0);
            passwordGrid.Visibility = Visibility.Hidden;
        }
    }
}
