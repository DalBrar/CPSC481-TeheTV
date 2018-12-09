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
    /// Interaction logic for ContentButton.xaml
    /// </summary>
    public partial class ContentButton : UserControl
    {
        private MainWindow app;
        private Content content;

        public ContentButton(MainWindow instance, Content c)
        {
            app = instance;
            content = c;
            InitializeComponent();

            Thumbnail.Source = c.Thumbnail;
            Watermark.Source = c.Watermark;
            Label.Content = c.Title;
        }

        private void buttonClick(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonPress);
            TVScreen curTV = MainWindow.TvScreen;
            if (curTV == null)
            {
                TVScreen newTV = new TVScreen(SettingsManager.getCurrentProfile(), content);
                MainWindow.TvScreen = newTV;
            } else
            {
                curTV.Update(SettingsManager.getCurrentProfile(), content);
            }
        }
    }
}
