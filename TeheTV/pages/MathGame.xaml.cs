using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TeheTV.Animations;

namespace TeheTV.Pages
{
    /// <summary>
    /// Interaction logic for MathGame.xaml
    /// </summary>
    public partial class MathGame : Page
    {
        MainWindow app;
        private int num1 = 0;
        private int num2 = 0;
        private int numAnswer = 0;
        private int numAnswerChoice = 0;

        public MathGame(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            resetPage();
            hideModals();
        }

        private void resetPage()
        {
            //_digitPos = 1;
            //digitANSWER.Content = "";
            Random rnd = new Random();
            num1 = rnd.Next(5, 11);
            num2 = rnd.Next(5, 11);
            numAnswer = num1 * num2;
            textQuestion.Text = num1 + " x " + num2;

            numAnswerChoice = rnd.Next(1, 4);

            if(numAnswerChoice == 1)
            {
                choice1.Content = numAnswer;
                choice2.Content = getDifferentRandomNumber(rnd, numAnswer);
                choice3.Content = getDifferentRandomNumber(rnd, numAnswer);
            }
            else if(numAnswerChoice == 2)
            {
                choice1.Content = getDifferentRandomNumber(rnd, numAnswer);
                choice2.Content = numAnswer;
                choice3.Content = getDifferentRandomNumber(rnd, numAnswer);
            }
            else
            {
                choice1.Content = getDifferentRandomNumber(rnd, numAnswer);
                choice2.Content = getDifferentRandomNumber(rnd, numAnswer);
                choice3.Content = numAnswer;
            }
        }

        private int getDifferentRandomNumber( Random rnd, int answer )
        {
            int randomNum = rnd.Next(25, 121);
            while(randomNum == answer)
            {
                randomNum = rnd.Next(25, 121);
            }
            return randomNum;
        }

        private void BackBtn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(SCREEN.Initialize, true);
        }

        private void Choice3_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(numAnswerChoice == 3)
            {
                Sounds.Play(Properties.Resources.soundReward);
                Modal.ModalFadeIn(correctGrid, true);
                Task.Delay(1000).ContinueWith(t => cleanAfterSuccess());
            }
            else
            {
                Modal.ModalFadeIn(errorGrid, false);
            }
        }

        private void cleanAfterSuccess()
        {
            this.Dispatcher.Invoke(() =>
                {
                    hideModals();
                    resetPage();
                }
            );
        }

        private void Choice2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (numAnswerChoice == 2)
            {
                Sounds.Play(Properties.Resources.soundReward);
                Modal.ModalFadeIn(correctGrid, true);
                Task.Delay(1000).ContinueWith(t => cleanAfterSuccess());
            }
            else
            { 
                Modal.ModalFadeIn(errorGrid, false);
            }
        
        }

        private void Choice1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (numAnswerChoice == 1)
            {
                Sounds.Play(Properties.Resources.soundReward);
                Modal.ModalFadeIn(correctGrid, true);
                Task.Delay(1000).ContinueWith(t => cleanAfterSuccess());
            }
            else
            {
                Modal.ModalFadeIn(errorGrid, false);
            }
        }

        private void btnErrorClicked(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            Animations.Modal.ModalFadeOut();
        }

        private void hideModals()
        {
            errorGrid.Visibility = Visibility.Hidden;
            errorGrid.Opacity = 0;
            correctGrid.Visibility = Visibility.Hidden;
            correctGrid.Opacity = 0;
        }
    }
}
