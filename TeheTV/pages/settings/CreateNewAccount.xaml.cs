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
using static TeheTV.Keyboard;

namespace TeheTV.Pages
{
    public partial class CreateNewAccount : Page
    {
        MainWindow app;
        private bool _isUp = false;

        public CreateNewAccount(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            btnDone.Visibility = Visibility.Hidden;
            backBtn.Visibility = Visibility.Hidden;

            if (SettingsManager.getCurrentProfile() != null)
                backBtn.Visibility = Visibility.Visible;

            keyboard.ReturnKeyText = "Set";
            keyboard.EmptySpaceReturn = false;
            keyboard.ReturnEvent += new CustomKeyboardEventHandler(slideGridScreenDown);
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
                slideGridScreenUpBy(350);
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
                slideGridScreenUpBy(350);
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
                slideGridScreenUpBy(350);
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
                slideGridScreenUpBy(350);
                keyboard.SlideUp();
            }
        }

        private void fieldTimeClicked(object sender, MouseButtonEventArgs e)
        {
            TextBlock field = (TextBlock)sender;

            selectField(field);
            keyboard.MaxInputLength = 4;
            keyboard.OutputTextBlock = field;
            keyboard.keyboardStyle = Keyboard.KeyboardStyle.NUMERICAL;

            if (!_isUp)
            {
                _isUp = true;
                slideGridScreenUpBy(350);
                keyboard.SlideUp();
            }
        }

        private void doneClicked(object sender, MouseButtonEventArgs e)
        {
            string name = fieldNAME.Text;
            while (name.EndsWith(" "))
                name = name.Substring(0, name.Length - 1);
            int month = getMonthNum(fieldMONTH.Text);
            int day = int.Parse(fieldDAY.Text);
            int year = int.Parse(fieldYEAR.Text);
            int time = int.Parse(fieldTIME.Text);

            SettingsManager.createNewProfile(name, month, day, year, time);
            app.ScreenChangeTo(SCREEN.ProfileSelector, true);
        }

        // ***** Helper Functions *****

        private void selectField(TextBlock field)
        {
            string name = field.Name;
            Rectangle box;
            if (name.Equals("fieldMONTH")) box = fieldMONTHbg;
            else if (name.Equals("fieldDAY")) box = fieldDAYbg;
            else if (name.Equals("fieldYEAR")) box = fieldYEARbg;
            else if (name.Equals("fieldTIME")) box = fieldTIMEbg;
            else box = fieldNAMEbg;

            deselectFields();
            box.Fill = MainWindow.setBrushColor(100, 255, 255, 255);
            Sounds.Play(Properties.Resources.soundButtonClick);
        }

        private void deselectFields()
        {
            byte col = 150;
            fieldNAMEbg.Fill  = MainWindow.setBrushColor(63, col, col, col);
            fieldMONTHbg.Fill = MainWindow.setBrushColor(63, col, col, col);
            fieldDAYbg.Fill   = MainWindow.setBrushColor(63, col, col, col);
            fieldYEARbg.Fill  = MainWindow.setBrushColor(63, col, col, col);
            fieldTIMEbg.Fill  = MainWindow.setBrushColor(63, col, col, col);
        }

        private void slideGridScreenUpBy(double amount)
        {
            Animations.GridScreen.SlideUp(gridScreen, amount);
        }

        private void slideGridScreenDown(string output) {slideGridScreenDown();}
        private void slideGridScreenDown()
        {
            _isUp = false;
            deselectFields();
            Animations.GridScreen.SlideDown();
            checkToRevealDoneButton();
        }

        private void checkToRevealDoneButton()
        {
            string name = fieldNAME.Text;
            string month = fieldMONTH.Text;
            string dayStr = fieldDAY.Text;
            string yearStr = fieldYEAR.Text;
            string timeStr = fieldTIME.Text;

            int day;
            try { day = int.Parse(dayStr); }
            catch { day = 0; }

            int year;
            try { year = int.Parse(yearStr); }
            catch { year = -1; }

            int time;
            try { time = int.Parse(timeStr); }
            catch { time = 0; }

            bool invalidInfo = false;
            if (isEmpty(name))
            {
                invalidInfo = true;
                fieldNAMEbg.Fill = MainWindow.setBrushColor(100, 255, 0, 0);
            }
            if (isEmpty(month))
            {
                invalidInfo = true;
                fieldMONTHbg.Fill = MainWindow.setBrushColor(100, 255, 0, 0);
            }
            if (isEmpty(dayStr) || notValidDay(month, day, year))
            {
                invalidInfo = true;
                fieldDAYbg.Fill = MainWindow.setBrushColor(100, 255, 0, 0);
            }
            if (isEmpty(yearStr) || notValidYear(year))
            {
                invalidInfo = true;
                fieldYEARbg.Fill = MainWindow.setBrushColor(100, 255, 0, 0);
            }
            if (time < 5)
            {
                invalidInfo = true;
                fieldTIMEbg.Fill = MainWindow.setBrushColor(100, 255, 0, 0);
            }

            if (invalidInfo)
            {
                Sounds.Play(Properties.Resources.soundPop);
                btnDone.Visibility = Visibility.Hidden;
            }
            else
            {
                Sounds.Play(Properties.Resources.soundPassCorrect);
                btnDone.Visibility = Visibility.Visible;
            }
        }
        private bool isEmpty(string str)
        {
            return String.IsNullOrEmpty(str) || String.IsNullOrWhiteSpace(str);
        }
        private bool notValidYear(int y)
        {
            int curY = DateTime.Now.Year;
            if (y > 0 && y <= curY)
                return false;
            else
                return true;
        }
        private bool notValidDay(string m, int d, int y)
        {
            if (d == 0) return true;
            if (y == -1) y = DateTime.Now.Year;
            int maxDays = DateTime.DaysInMonth(y, getMonthNum(m));
            if (d >= 1 && d <= maxDays)
                return false;
            else
                return true;
        }
        private int getMonthNum(string m)
        {
            if (m.Equals("Dec")) return 12;
            if (m.Equals("Nov")) return 11;
            if (m.Equals("Oct")) return 10;
            if (m.Equals("Sep")) return 9;
            if (m.Equals("Aug")) return 8;
            if (m.Equals("Jul")) return 7;
            if (m.Equals("Jun")) return 6;
            if (m.Equals("May")) return 5;
            if (m.Equals("Apr")) return 4;
            if (m.Equals("Mar")) return 3;
            if (m.Equals("Feb")) return 2;
            return 1;
        }

        private void backBtnPressed(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            app.ScreenGoBack();
        }
    }
}
