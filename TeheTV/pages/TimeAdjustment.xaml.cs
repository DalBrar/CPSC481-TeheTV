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
    /// <summary>
    /// Interaction logic for TimeAdjustment.xaml
    /// </summary>
    public partial class TimeAdjustment : Page
    {
        MainWindow app;
        private Page parentSettings;

        public TimeAdjustment(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            parentSettings = new ParentSettings(app);
        }

        // implement a pop up window later to confirm and also check the validity of the time they entered. 
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(parentSettings, true);
        }
    }
}
