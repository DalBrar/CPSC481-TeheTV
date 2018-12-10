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
    /// <summary>
    /// Interaction logic for Recommendations.xaml
    /// </summary>
    public partial class Recommendations : Page
    {
        MainWindow app;

        public Recommendations(MainWindow instance)
        {
            app = instance;
            InitializeComponent();

            keyboard.ReturnKeyText = "Search";
            keyboard.MaxInputLength = 30;
            keyboard.IsPassword = false;
            keyboard.EmptySpaceReturn = true;
            keyboard.ClearValuesOnSlideUp = false;
            keyboard.OutputTextBlock = searchBarField;
            keyboard.keyboardStyle = Keyboard.KeyboardStyle.ALL;
            keyboard.ReturnEvent += new CustomKeyboardEventHandler(updateSearchResults);
            keyboard.TypeEvent += new CustomKeyboardEventHandler(updateSearchResults);
        }

        private void backBtnPressed(object sender, MouseButtonEventArgs e) { app.ScreenGoBack(); }

        private void updateSearchResults(string output)
        {

        }
    }
}
