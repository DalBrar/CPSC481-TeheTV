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

namespace TeheTV
{
    /// <summary>
    /// Remember to ALWAYS set OutputTextBlock property of keyboard to a TextBlock before use.
    /// Optional Properties:
    ///      MaxInputLength = int           - Control how long the input is (default 15 characters)
    ///      keyboardStlye = KeyboardStyle  - Control which kinds of keys are available (All, numbers only, alpha only)
    /// </summary>
    public partial class Keyboard : UserControl
    {
        private TextBlock output;
        private int _MAXSTRING = 14;
        private bool _isCAPSLOCK = false;

        public Keyboard()
        {
            InitializeComponent();
            hideNumbers();
        }

        // ***** Properites *****
        public TextBlock OutputTextBlock
        {
            set { output = value; }
        }

        public int MaxInputLength
        {
            get { return _MAXSTRING; }
            set { _MAXSTRING = value; }
        }

        public KeyboardStyle keyboardStyle
        {
            set
            {
                if (value == KeyboardStyle.NUMERICAL)
                {
                    hideAlphabet();
                    showNumbers();
                    keyNum.Visibility = Visibility.Hidden;
                }
                else if (value == KeyboardStyle.ALPHABET)
                {
                    hideNumbers();
                    showAlhpabet();
                    keyNum.Visibility = Visibility.Hidden;
                }
                else
                {
                    keyNum.Visibility = Visibility.Visible;
                    hideNumbers();
                    showAlhpabet();
                }
            }
        }

        public enum KeyboardStyle
        {
            ALL,
            NUMERICAL,
            ALPHABET
        }

        // ***** Key Press Functions *****

        private void outputKey(string ch)
        {
            int len = output.Text.Length;
            if (len < _MAXSTRING)
            {
                output.Text = output.Text + ch;
                playKeypress();
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
            outputKey(" ");
        }

        private void delPressed(object sender, MouseButtonEventArgs e)
        {
            string text = output.Text;
            int len = text.Length;
            if (len > 0)
            {
                output.Text = text.Substring(0, len - 1);
                playKeypress();
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
            if (gridNumerical.Visibility == Visibility.Visible)
            {
                hideNumbers();
                showAlhpabet();
            }
            else
            {
                hideAlphabet();
                showNumbers();
            }
        }

        private void returnPressed(object sender, MouseButtonEventArgs e)
        {

        }

        private void hideNumbers()
        {
            gridNumerical.Visibility = Visibility.Hidden;
        }
        private void showNumbers()
        {
            gridNumerical.Visibility = Visibility.Visible;
        }

        private void hideAlphabet()
        {
            gridAlphabet.Visibility = Visibility.Hidden;
        }
        private void showAlhpabet()
        {
            gridAlphabet.Visibility = Visibility.Visible;
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
    }
}
