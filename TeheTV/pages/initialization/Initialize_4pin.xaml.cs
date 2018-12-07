using System;
using System.Collections.Generic;
using System.Drawing;
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
using Color = System.Drawing.Color;

namespace TeheTV.pages
{
    public partial class Initialize4pin : Page
    {
        MainWindow app;
        private int _digitPos = 1;
        private int _pinStep = 1;
        private int _pinA = -1;
        private int _pinB = -2;
        private string _hiddenPIN = "";

        public Initialize4pin(MainWindow instance)
        {
            app = instance;
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

            updateAnswer(value);
        }

        private void btnErrorClicked(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            ModalFadeOut();
            resetPage();
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
            SettingsManager.setPin("" + _pinB);
            app.ScreenChangeTo(this, true);
        }

        // ******************
        //  Helper Functions
        // ******************
        private void updateAnswer(int n)
        {
            if (n < 0)
                deleteDigit();
            else
                addDigit(n);
        }

        private void addDigit(int n)
        {
            if (_digitPos < 5)
            {
                digitPIN.Content = ((string)digitPIN.Content) + "*";
                _hiddenPIN = _hiddenPIN + n;
                _digitPos++;
            }

            if (_digitPos == 5)
            {
                if (_pinStep == 1)
                {
                    _pinA = int.Parse(_hiddenPIN);
                    _pinStep = 2;
                    _digitPos = 1;
                    digitPIN.Content = "";
                    _hiddenPIN = "";
                    Sounds.Play(Properties.Resources.soundPassCorrect);
                    textINSTRUCTION.Text = "Now re-enter the same PIN:";
                    textINSTRUCTION.Foreground = (System.Windows.Media.Brush)new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 0, 200, 0));
                }
                else if (_pinStep == 2)
                {
                    _pinB = int.Parse(_hiddenPIN);

                    if (_pinA == _pinB)
                    {
                        ModalFadeIn(correctGrid, true);
                    }
                    else
                    {
                        ModalFadeIn(errorGrid, false);
                    }
                }
            }
        }

        private void deleteDigit()
        {
            if (_digitPos > 1)
            {
                string currStars = (string)digitPIN.Content;
                digitPIN.Content = currStars.Substring(0, currStars.Length - 1);
                _hiddenPIN = _hiddenPIN.Substring(0, _hiddenPIN.Length - 1);
                _digitPos--;
            }
            else
            {
                Sounds.Play(Properties.Resources.soundDelete);
            }
        }

        private void resetPage()
        {
            _digitPos = 1;
            digitPIN.Content = "";
            _hiddenPIN = "";
            _pinStep = 1;
            _pinA = -1;
            _pinB = -2;
            textINSTRUCTION.Text = "Enter a 4 digit PIN:";
            textINSTRUCTION.Foreground = (System.Windows.Media.Brush)new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 255));
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
