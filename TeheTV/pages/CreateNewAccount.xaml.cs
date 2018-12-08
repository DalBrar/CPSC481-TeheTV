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

namespace TeheTV.pages
{
    public partial class CreateNewAccount : Page
    {
        MainWindow app;
        private bool _isUp = false;

        public CreateNewAccount(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            checkToRevealDoneButton();

            keyboard.ReturnKeyText = "Set";
            keyboard.EmptySpaceReturn = false;
            keyboard.ReturnEvent += new EventHandler(slideGridScreenDown);
        }

        // ***** Button Click Functions ****
        private void fieldNameClicked(object sender, MouseButtonEventArgs e)
        {
            TextBlock field = (TextBlock)sender;

            selectField(field);
            keyboard.MaxInputLength = 22;
            keyboard.OutputTextBlock = field;
            keyboard.keyboardStyle = Keyboard.KeyboardStyle.ALPHABET;

            if (!_isUp)
            {
                _isUp = true;
                slideGridScreenUpBy(400);
                keyboard.SlideUp();
            }
        }

        private void fieldMonthClicked(object sender, MouseButtonEventArgs e)
        {
            TextBlock field = (TextBlock)sender;

            selectField(field);
            keyboard.MaxInputLength = 3;
            keyboard.OutputTextBlock = field;
            keyboard.keyboardStyle = Keyboard.KeyboardStyle.MONTHS;

            if (!_isUp)
            {
                _isUp = true;
                slideGridScreenUpBy(400);
                keyboard.SlideUp();
            }
        }

        private void fieldDayClicked(object sender, MouseButtonEventArgs e)
        {
            TextBlock field = (TextBlock)sender;

            selectField(field);
            keyboard.MaxInputLength = 2;
            keyboard.OutputTextBlock = field;
            keyboard.keyboardStyle = Keyboard.KeyboardStyle.NUMERICAL;

            if (!_isUp)
            {
                _isUp = true;
                slideGridScreenUpBy(400);
                keyboard.SlideUp();
            }
        }

        private void fieldYearClicked(object sender, MouseButtonEventArgs e)
        {
            TextBlock field = (TextBlock)sender;

            selectField(field);
            keyboard.MaxInputLength = 4;
            keyboard.OutputTextBlock = field;
            keyboard.keyboardStyle = Keyboard.KeyboardStyle.NUMERICAL;

            if (!_isUp)
            {
                _isUp = true;
                slideGridScreenUpBy(400);
                keyboard.SlideUp();
            }
        }

        private void doneClicked(object sender, MouseButtonEventArgs e)
        {

        }

        // ***** Helper Functions *****
        private void checkToRevealDoneButton()
        {
            string name = fieldNAME.Text;
            string month = fieldMONTH.Text;
            string day = fieldDAY.Text;
            string year = fieldYEAR.Text;
            if (isEmpty(name) || isEmpty(month) || isEmpty(day) || isEmpty(year))
                btnDone.Visibility = Visibility.Hidden;
            else
                btnDone.Visibility = Visibility.Visible;
        }
        private bool isEmpty(string str)
        {
            return String.IsNullOrEmpty(str) || String.IsNullOrWhiteSpace(str);
        }

        private void selectField(TextBlock box)
        {
            deselectFields();
            box.Background = MainWindow.setBrushColor(100, 255, 255, 255);
            Sounds.Play(Properties.Resources.soundButtonClick);
        }

        private void deselectFields()
        {
            byte col = 150;
            fieldNAME.Background = MainWindow.setBrushColor(63, col, col, col);
            fieldMONTH.Background = MainWindow.setBrushColor(63, col, col, col);
            fieldDAY.Background = MainWindow.setBrushColor(63, col, col, col);
            fieldYEAR.Background = MainWindow.setBrushColor(63, col, col, col);
        }

        private void slideGridScreenUpBy(double amount)
        {
            Animations.GridScreen.SlideUp(gridScreen, amount);
        }

        private void slideGridScreenDown(object sender, EventArgs e)
        {
            slideGridScreenDown();
        }
        private void slideGridScreenDown()
        {
            _isUp = false;
            deselectFields();
            Animations.GridScreen.SlideDown();
        }
    }
}
