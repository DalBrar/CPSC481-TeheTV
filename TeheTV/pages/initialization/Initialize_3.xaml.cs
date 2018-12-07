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
    public partial class Initialize3 : Page
    {
        MainWindow app;
        Page initPIN;
        private int _digitPos = 1;
        private int num1 = 0;
        private int num2 = 0;
        private int numAnswer = 0;

        public Initialize3(MainWindow instance)
        {
            app = instance;
            initPIN = new Initialize4pin(instance);
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
            app.ScreenChangeTo(initPIN, true);
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
            if (_digitPos < 3)
            {
                digitANSWER.Content = ((string)digitANSWER.Content) + n;
                _digitPos++;
            }

            if (_digitPos == 3)
            {
                int ans = int.Parse((string) digitANSWER.Content);

                if (ans == numAnswer)
                {
                    ModalFadeIn(correctGrid, true);
                }
                else
                {
                    ModalFadeIn(errorGrid, false);
                }
            }
        }

        private void deleteDigit()
        {
            if (_digitPos > 1)
            {
                string currAnswer = (string) digitANSWER.Content;
                digitANSWER.Content = currAnswer.Substring(0, currAnswer.Length - 1);
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
            digitANSWER.Content = "";
            Random rnd = new Random();
            num1 = rnd.Next(5, 11);
            num2 = rnd.Next(5, 11);
            numAnswer = num1 * num2;
            textQuestion.Text = num1 + " x " + num2;
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
