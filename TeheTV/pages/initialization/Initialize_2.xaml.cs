using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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

namespace TeheTV.pages
{
    public partial class Initialize2 : Page
    {
        MainWindow app;
        Page nextInit;
        private int _digitPos = 1;
        private int _dig1 = 0;
        private int _dig2 = 0;
        private int _dig3 = 0;
        private int _dig4 = 0;

        public Initialize2(MainWindow instance)
        {
            app = instance;
            nextInit = new Initialize3(instance);
            InitializeComponent();
            resetPage();
            hideModals();
        }

        // ************************
        //  Button Press Functions
        // ************************
        private void btnKeypadPressKey(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundKeypress);
            Button btn = (Button) e.Source;
            string content = (string) btn.Content;

            int value = 0;
            try
            {
                value = int.Parse(content);
            }
            catch (Exception)
            {
                value = -1;
            }

            updateYear(value);
        }

        private void btnErrorClicked(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            ModalFadeOut();
            resetPage();
            app.ScreenGoBack();
        }

        private void btnCorrNoClicked(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            ModalFadeOut();
            resetPage();
        }

        private void btnCorrYesClicked(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            ModalFadeOut();
            resetPage();
            app.ScreenChangeTo(nextInit, true);
        }

        // ******************
        //  Helper Functions
        // ******************
        private void updateYear(int n)
        {
            if (n < 0)
                deleteDigit();
            else
                addDigit(n);
        }

        private void addDigit(int n)
        {
            if (_digitPos == 1)
            {
                digitONE.Content = n;
                _dig1 = n * 1000;
                _digitPos++;
            }
            else if (_digitPos == 2)
            {
                digitTWO.Content = n;
                _dig2 = n * 100;
                _digitPos++;
            }
            else if (_digitPos == 3)
            {
                digitTHREE.Content = n;
                _dig3 = n * 10;
                _digitPos++;
            }
            else if (_digitPos == 4)
            {
                digitFOUR.Content = n;
                _dig4 = n;
                _digitPos++;

                int age = getAge();
                if (age < 16 || age > 100)
                {
                    ModalFadeIn(errorGrid, false);
                }
                else
                {
                    textCorrAge.Text = "You were born in the year " + getYear() + ".";
                    ModalFadeIn(correctGrid, true);
                }
            }
            else
            {
                Sounds.Play(Properties.Resources.soundDelete);
            }
        }

        private void deleteDigit()
        {
            if (_digitPos == 1)
            {
                Sounds.Play(Properties.Resources.soundDelete);
            }
            else if (_digitPos == 2)
            {
                digitONE.Content = "";
                _digitPos--;
            }
            else if (_digitPos == 3)
            {
                digitTWO.Content = "";
                _digitPos--;
            }
            else if (_digitPos == 4)
            {
                digitTHREE.Content = "";
                _digitPos--;
            }
            else if (_digitPos == 5)
            {
                digitFOUR.Content = "";
                _digitPos--;
            }
        }

        private int getAge()
        {
            int cur = DateTime.Now.Year;
            int par = getYear();
            return cur - par;
        }

        private int getYear()
        {
            return _dig1 + _dig2 + _dig3 + _dig4;
        }

        private void resetPage()
        {
            _digitPos = 1;
            _dig1 = 0;
            _dig2 = 0;
            _dig3 = 0;
            _dig4 = 0;
            digitONE.Content = "";
            digitTWO.Content = "";
            digitTHREE.Content = "";
            digitFOUR.Content = "";
        }

        private void hideModals()
        {
            errorGrid.Visibility = Visibility.Hidden;
            errorGrid.Opacity = 0;
            correctGrid.Visibility = Visibility.Hidden;
            correctGrid.Opacity = 0;
        }

        // ******************
        //  Modal Fade Stuff
        // ******************
        private Grid _fadingObj;
        private bool _success = false;

        private void ModalFadeIn(Grid obj, bool success)
        {
            _fadingObj = obj;
            _success = success;

            _fadingObj.Opacity = 0;
            _fadingObj.Visibility = Visibility.Visible;

            DoubleAnimation animation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(100)));
            animation.Completed += playSound;
            _fadingObj.BeginAnimation(Grid.OpacityProperty, animation);
        }
        private void ModalFadeOut()
        {
            if (_fadingObj != null)
            {
                DoubleAnimation animation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(100)));
                animation.Completed += ModalHidden;
                _fadingObj.BeginAnimation(Grid.OpacityProperty, animation);
            }
        }

        private void ModalHidden(object senderNotUsed, EventArgs eNotUsed)
        {
            _fadingObj.Opacity = 0;
            _fadingObj.Visibility = Visibility.Hidden;
        }

        private void playSound(object senderNotUsed, EventArgs eNotUsed)
        {
            if (_success)
                Sounds.Play(Properties.Resources.soundPassCorrect);
            else
                Sounds.Play(Properties.Resources.soundUhOh);
        }
    }
}
