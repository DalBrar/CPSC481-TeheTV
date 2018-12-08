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
            keyboard.OutputTextBlock = fieldNAME;
            keyboard.MaxInputLength = 15;
        }

        // ***** Button Click Functions ****
        private void fieldNameClicked(object sender, MouseButtonEventArgs e)
        {
            if (!_isUp)
            {
                slideGridScreenUpBy(400);
            }
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

        private void slideGridScreenUpBy(double amount)
        {
            _isUp = true;
            Animations.GridScreen.SlideUp(gridScreen, amount);
        }

        private void slideGridScreenDown()
        {
            _isUp = false;
            Animations.GridScreen.SlideDown();
        }
    }
}
