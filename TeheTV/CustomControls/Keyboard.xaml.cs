using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace TeheTV
{
    /// <summary>
    /// Remember to ALWAYS set OutputTextBlock property of keyboard to a TextBlock before use.
    /// Optional Properties:
    ///      MaxInputLength = int           - Control how long the input is (default 15 characters)
    ///      keyboardStlye = KeyboardStyle  - Control which kinds of keys are available (All, numbers only, alpha only)
    ///      ReturnKeyText = string         - Change the text of the Return key
    ///      EmptySpaceReturn = bool        - Whether clicking the empty space functions as clicking return (default true).
    /// Optional Event:
    ///      ReturnEven += new KeyboardEventHandler(customMethod);  - Executes the custom method when the keyboard's return key is pressed.
    /// </summary>
    public partial class Keyboard : UserControl
    {
        public delegate void CustomKeyboardEventHandler(string input);
        public event CustomKeyboardEventHandler ReturnEvent;
        public event CustomKeyboardEventHandler TypeEvent;
        private TextBlock outputTextBlock;
        private int _MAXSTRING = 14;
        private bool _isCAPSLOCK = false;
        private bool _hideOutput = false;
        private bool _clearValues = false;
        private string realInput = "";

        public Keyboard()
        {
            InitializeComponent();

            hideKeyset(keysetNumerical);
            hideKeyset(keysetMonths);

            this.Visibility = Visibility.Hidden;
        }

        protected virtual void ExecuteReturnEvent()
        {
            if (ReturnEvent != null) ReturnEvent(realInput);
        }

        protected virtual void ExecuteTypeEvent()
        {
            if (TypeEvent != null) TypeEvent(realInput);
        }

        // ***** Properites *****
        public TextBlock OutputTextBlock
        {
            set { outputTextBlock = value; }
        }

        public int MaxInputLength
        {
            get { return _MAXSTRING; }
            set { _MAXSTRING = value; }
        }

        public bool IsPassword
        {
            set { _hideOutput = value; }
        }

        public bool ClearValuesOnSlideUp
        {
            set { _clearValues = value; }
        }

        public KeyboardStyle keyboardStyle
        {
            set
            {
                if (value == KeyboardStyle.NUMERICAL)
                {
                    showButton(keyDel);
                    hideKeyset(keysetAlphabet);
                    hideKeyset(keysetMonths);
                    showKeyset(keysetNumerical);
                    hideButton(keyNum);
                }
                else if (value == KeyboardStyle.ALPHABET)
                {
                    showButton(keyDel);
                    hideKeyset(keysetNumerical);
                    hideKeyset(keysetMonths);
                    showKeyset(keysetAlphabet);
                    hideButton(keyNum);
                }
                else if (value == KeyboardStyle.MONTHS)
                {
                    hideKeyset(keysetNumerical);
                    hideKeyset(keysetAlphabet);
                    showKeyset(keysetMonths);
                    hideButton(keyDel);
                    hideButton(keyNum);
                }
                else
                {
                    showButton(keyDel);
                    showButton(keyNum);
                    hideKeyset(keysetNumerical);
                    hideKeyset(keysetMonths);
                    showKeyset(keysetAlphabet);
                }
            }
        }

        public string ReturnKeyText
        {
            set { keyReturn.Content = value; }
        }

        public bool EmptySpaceReturn
        {
            set
            {
                if (value)
                    invisibleReturnBarrier.Visibility = Visibility.Visible;
                else
                    invisibleReturnBarrier.Visibility = Visibility.Hidden;
            }
        }

        public enum KeyboardStyle
        {
            ALL,
            NUMERICAL,
            ALPHABET,
            MONTHS
        }

        // ***** Key Press Functions *****

        private void monthPressed(object sender, MouseButtonEventArgs e)
        {
            string mnth = (string)((Button)sender).Content;
            outputTextBlock.Text = mnth;
            playKeypress();
        }

        private void outputKey(string ch)
        {
            int len = outputTextBlock.Text.Length;
            if (len < _MAXSTRING)
            {
                if (!_hideOutput)
                {
                    outputTextBlock.Text = outputTextBlock.Text + ch;
                    realInput = outputTextBlock.Text;
                    playKeypress();
                    ExecuteTypeEvent();
                } else
                {
                    outputTextBlock.Text = outputTextBlock.Text + "*";
                    realInput = realInput + ch;
                    playKeypress();
                    ExecuteTypeEvent();
                }
            }
            else
                playError();
        }

        private void keyPressed(object sender, MouseButtonEventArgs e)
        {
            string ch = (string)((Button)sender).Content;
            outputKey(ch);
        }

        private void spacePressed(object sender, MouseButtonEventArgs e)
        {
            int len = outputTextBlock.Text.Length;
            if (len > 0)
            {
                string ch = outputTextBlock.Text.Substring(len - 1, 1);
                if (ch.Equals(" "))
                    playError();
                else
                    outputKey(" ");
                return;
            }
            playError();
        }

        private void delPressed(object sender, MouseButtonEventArgs e)
        {
            string text = outputTextBlock.Text;
            int len = text.Length;
            if (len > 0)
            {
                if (!_hideOutput)
                {
                    outputTextBlock.Text = text.Substring(0, len - 1);
                    realInput = outputTextBlock.Text;
                    playKeypress();
                    ExecuteTypeEvent();
                } else
                {
                    outputTextBlock.Text = text.Substring(0, len - 1);
                    realInput = realInput.Substring(0, len - 1);
                    playKeypress();
                    ExecuteTypeEvent();
                }
            }
            else
                playError();
        }

        private void capsPressed(object sender, MouseButtonEventArgs e)
        {
            playToggle();
            if (_isCAPSLOCK)
            {
                _isCAPSLOCK = false;
                keyQ.Content = "q";
                keyW.Content = "w";
                keyE.Content = "e";
                keyR.Content = "r";
                keyT.Content = "t";
                keyY.Content = "y";
                keyU.Content = "u";
                keyI.Content = "i";
                keyO.Content = "o";
                keyP.Content = "p";
                keyA.Content = "a";
                keyS.Content = "s";
                keyD.Content = "d";
                keyF.Content = "f";
                keyG.Content = "g";
                keyH.Content = "h";
                keyJ.Content = "j";
                keyK.Content = "k";
                keyL.Content = "l";
                keyZ.Content = "z";
                keyX.Content = "x";
                keyC.Content = "c";
                keyV.Content = "v";
                keyB.Content = "b";
                keyN.Content = "n";
                keyM.Content = "m";
            }
            else
            {
                _isCAPSLOCK = true;
                keyQ.Content = "Q";
                keyW.Content = "W";
                keyE.Content = "E";
                keyR.Content = "R";
                keyT.Content = "T";
                keyY.Content = "Y";
                keyU.Content = "U";
                keyI.Content = "I";
                keyO.Content = "O";
                keyP.Content = "P";
                keyA.Content = "A";
                keyS.Content = "S";
                keyD.Content = "D";
                keyF.Content = "F";
                keyG.Content = "G";
                keyH.Content = "H";
                keyJ.Content = "J";
                keyK.Content = "K";
                keyL.Content = "L";
                keyZ.Content = "Z";
                keyX.Content = "X";
                keyC.Content = "C";
                keyV.Content = "V";
                keyB.Content = "B";
                keyN.Content = "N";
                keyM.Content = "M";
            }
        }

        private void numPressed(object sender, MouseButtonEventArgs e)
        {
            playToggle();
            if (keysetNumerical.Visibility == Visibility.Visible)
            {
                hideKeyset(keysetNumerical);
                showKeyset(keysetAlphabet);
            }
            else
            {
                hideKeyset(keysetAlphabet);
                showKeyset(keysetNumerical);
            }
        }

        private void returnPressed(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (ThreadStart)delegate () { ExecuteReturnEvent(); });
            SlideDown();
        }

        private void hideKeyset(Grid grid)
        {
            grid.Visibility = Visibility.Hidden;
        }
        private void showKeyset(Grid grid)
        {
            grid.Visibility = Visibility.Visible;
        }

        private void hideButton(Button btn)
        {
            btn.Visibility = Visibility.Hidden;
        }
        private void showButton(Button btn)
        {
            btn.Visibility = Visibility.Visible;
        }

        private void playKeypress()
        {
            Sounds.Play(Properties.Resources.soundKeypress);
        }

        private void playError()
        {
            Sounds.Play(Properties.Resources.soundDelete);
        }

        private void playToggle()
        {
            Sounds.Play(Properties.Resources.soundButtonPress);
        }

        // ************************
        //  Slide Animations
        // ************************
        private static Int32 _slideMiliSecs = 250;
        // public Thickness (double left, double top, double right, double bottom);

        public void SlideUp()
        {
            if (_clearValues)
            {
                outputTextBlock.Text = "";
                realInput = "";
            } else
            {
                if (string.IsNullOrEmpty(realInput) || string.IsNullOrWhiteSpace(realInput))
                    realInput = outputTextBlock.Text;
            }
            this.Visibility = Visibility.Visible;
            ThicknessAnimation animation = new ThicknessAnimation();
            animation.Duration = TimeSpan.FromMilliseconds(_slideMiliSecs);
            animation.From = new Thickness(0, 700, 0, 0);
            animation.To = new Thickness(0, 0, 0, 0);
            playSound(true);
            this.BeginAnimation(MarginProperty, animation);
        }
        public void SlideDown()
        {
            ThicknessAnimation animation = new ThicknessAnimation();
            animation.Duration = TimeSpan.FromMilliseconds(_slideMiliSecs);
            animation.From = new Thickness(0, 0, 0, 0);
            animation.To = new Thickness(0, 700, 0, 0);
            playSound(false);
            animation.Completed += Hidekeyboard;
            this.BeginAnimation(MarginProperty, animation);
        }

        private void Hidekeyboard(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private static void playSound(bool _slideUp)
        {
            if (_slideUp)
                Sounds.Play(Properties.Resources.soundSlideUp);
            else
                Sounds.Play(Properties.Resources.soundSlideDown);
        }
    }
}
