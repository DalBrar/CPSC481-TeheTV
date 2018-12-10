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
using TeheTV.Animations;

namespace TeheTV.Pages
{
    /// <summary>
    /// Interaction logic for TicTacToe.xaml
    /// </summary>
    public partial class TicTacToe : Page
    {
        MainWindow app;

        // 1 == x player turn and 0 == O player turn
        private int turn = 1;

        public TicTacToe(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            resetGame();
            hideModals();
        }

        private void hideModals()
        { 
            errorGrid.Visibility = Visibility.Hidden;
            errorGrid.Opacity = 0;
            correctGrid.Visibility = Visibility.Hidden;
            correctGrid.Opacity = 0;
        }


        private void resetGame()
        {
            choice1.Content = "";
            choice2.Content = "";
            choice3.Content = "";
            choice4.Content = "";
            choice5.Content = "";
            choice6.Content = "";
            choice7.Content = "";
            choice8.Content = "";
            choice9.Content = "";
        }

        private void declareWinner(String name)
        {
            if(name.Equals("X"))
            {
                Modal.ModalFadeIn(correctGrid, true);
            }
            else if(name.Equals("O"))
            {
                textErrorMsg.Text = "Computer won, better luck next time!";
                Modal.ModalFadeIn(errorGrid, true);
            }
            else if(name.Equals("D"))
            {
                textErrorMsg.Text = "Draw game, better luck next time!";
                Modal.ModalFadeIn(errorGrid, true);
            }
            Task.Delay(3000).ContinueWith(t => cleanAfterWin());
        }

        private void cleanAfterWin()
        {
            this.Dispatcher.Invoke(() =>
            {
                resetGame();
                hideModals();
            });
        }

        private void checkWinner(String symbol)
        {
            //Checking 1st row
            if(choice1.Content.Equals(symbol) && choice2.Content.Equals(symbol) && choice3.Content.Equals(symbol))
            {
                //Winner
                declareWinner(symbol);
                return;
            }

            //Checking 2nd row
            if (choice4.Content.Equals(symbol) && choice5.Content.Equals(symbol) && choice6.Content.Equals(symbol))
            {
                //Winner
                declareWinner(symbol);
                return;
            }

            //Checking 3rd row
            if (choice7.Content.Equals(symbol) && choice8.Content.Equals(symbol) && choice9.Content.Equals(symbol))
            {
                //Winner
                declareWinner(symbol);
                return;
            }

            //Checking 1st column
            if (choice1.Content.Equals(symbol) && choice4.Content.Equals(symbol) && choice7.Content.Equals(symbol))
            {
                //Winner
                declareWinner(symbol);
                return;
            }

            //Checking 2nd column
            if (choice2.Content.Equals(symbol) && choice5.Content.Equals(symbol) && choice8.Content.Equals(symbol))
            {
                //Winner
                declareWinner(symbol);
                return;
            }

            //Checking 3rd column
            if (choice3.Content.Equals(symbol) && choice6.Content.Equals(symbol) && choice9.Content.Equals(symbol))
            {
                //Winner
                declareWinner(symbol);
                return;
            }

            //Checking 1st diagonal
            if (choice1.Content.Equals(symbol) && choice5.Content.Equals(symbol) && choice9.Content.Equals(symbol))
            {
                //Winner
                declareWinner(symbol);
                return;
            }

            //Checking 2nd diagonal
            if (choice3.Content.Equals(symbol) && choice5.Content.Equals(symbol) && choice7.Content.Equals(symbol))
            {
                //Winner
                declareWinner(symbol);
                return;
            }

            checkIfGameEnded();
        }

        private void checkIfGameEnded()
        {
            if( !choice1.Content.Equals("") &&
                !choice2.Content.Equals("") &&
                !choice3.Content.Equals("") &&
                !choice4.Content.Equals("") &&
                !choice5.Content.Equals("") &&
                !choice6.Content.Equals("") &&
                !choice7.Content.Equals("") &&
                !choice8.Content.Equals("") &&
                !choice9.Content.Equals(""))
            {
                declareWinner("D");
            }
        }

        private void Choice1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(choice1.Content.ToString().Equals(""))
            {
                if(turn == 1)
                {
                    choice1.Content = "X";
                    turn = 0;
                }
            }
            checkWinner("X");
            Task.Delay(1000).ContinueWith(t => wePlay());
        }

        // we play only when turn == 1
        private void wePlay()
        {
            this.Dispatcher.Invoke(() =>
            {
                if (turn == 0)
                {
                    Random rnd = new Random();
                    int randomNum = rnd.Next(1, 10);
                    Boolean breakLoop = false;
                    while (!breakLoop)
                    {
                        switch (randomNum)
                        {
                            case 1:
                                if (choice1.Content.Equals(""))
                                {
                                    choice1.Content = "O";
                                    breakLoop = true;
                                }
                                break;
                            case 2:
                                if (choice2.Content.Equals(""))
                                {
                                    choice2.Content = "O";
                                    breakLoop = true;
                                }
                                break;
                            case 3:
                                if (choice3.Content.Equals(""))
                                {
                                    choice3.Content = "O";
                                    breakLoop = true;
                                }
                                break;
                            case 4:
                                if (choice4.Content.Equals(""))
                                {
                                    choice4.Content = "O";
                                    breakLoop = true;
                                }
                                break;
                            case 5:
                                if (choice5.Content.Equals(""))
                                {
                                    choice5.Content = "O";
                                    breakLoop = true;
                                }
                                break;
                            case 6:
                                if (choice6.Content.Equals(""))
                                {
                                    choice6.Content = "O";
                                    breakLoop = true;
                                }
                                break;
                            case 7:
                                if (choice7.Content.Equals(""))
                                {
                                    choice7.Content = "O";
                                    breakLoop = true;
                                }
                                break;
                            case 8:
                                if (choice8.Content.Equals(""))
                                {
                                    choice8.Content = "O";
                                    breakLoop = true;
                                }
                                break;
                            case 9:
                                if (choice9.Content.Equals(""))
                                {
                                    choice9.Content = "O";
                                    breakLoop = true;
                                }
                                break;
                        }
                        randomNum = rnd.Next(1, 10);
                    }
                    turn = 1;
                    checkWinner("O");
                }
            });
        }

        private void Choice2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (choice2.Content.ToString().Equals(""))
            {
                if (turn == 1)
                {
                    choice2.Content = "X";
                    turn = 0;
                }
            }
            checkWinner("X");
            Task.Delay(1000).ContinueWith(t => wePlay());
        }

        private void Choice3_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (choice3.Content.ToString().Equals(""))
            {
                if (turn == 1)
                {
                    choice3.Content = "X";
                    turn = 0;
                }
            }
            checkWinner("X");
            Task.Delay(1000).ContinueWith(t => wePlay());
        }

        private void Choice4_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (choice4.Content.ToString().Equals(""))
            {
                if (turn == 1)
                {
                    choice4.Content = "X";
                    turn = 0;
                }
            }
            checkWinner("X");
            Task.Delay(1000).ContinueWith(t => wePlay());
        }

        private void Choice5_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (choice5.Content.ToString().Equals(""))
            {
                if (turn == 1)
                {
                    choice5.Content = "X";
                    turn = 0;
                }
            }
            checkWinner("X");
            Task.Delay(1000).ContinueWith(t => wePlay());
        }

        private void Choice6_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (choice6.Content.ToString().Equals(""))
            {
                if (turn == 1)
                {
                    choice6.Content = "X";
                    turn = 0;
                }
            }
            checkWinner("X");
            Task.Delay(1000).ContinueWith(t => wePlay());
        }

        private void Choice7_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (choice7.Content.ToString().Equals(""))
            {
                if (turn == 1)
                {
                    choice7.Content = "X";
                    turn = 0;
                }
            }
            checkWinner("X");
            Task.Delay(1000).ContinueWith(t => wePlay());
        }

        private void Choice8_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (choice8.Content.ToString().Equals(""))
            {
                if (turn == 1)
                {
                    choice8.Content = "X";
                    turn = 0;
                }
            }
            checkWinner("X");
            Task.Delay(1000).ContinueWith(t => wePlay());
        }

        private void Choice9_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (choice9.Content.ToString().Equals(""))
            {
                if (turn == 1)
                {
                    choice9.Content = "X";
                    turn = 0;
                }
            }
            checkWinner("X");
            Task.Delay(1000).ContinueWith(t => wePlay());
        }

        private void BackBtn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            resetGame();
            app.ScreenGoBack();
        }
    }
}
