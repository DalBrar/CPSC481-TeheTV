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

namespace TeheTV.Pages
{
    /// <summary>
    /// Interaction logic for Histroy.xaml
    /// </summary>
    public partial class History : Page
    {
        MainWindow app;
        private Page parentSettings;

        public History(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            parentSettings = new ParentSettings(app);
        }

        private void ReturnButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(parentSettings, true);
        }

        private void NameButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TypeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AchievementButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
