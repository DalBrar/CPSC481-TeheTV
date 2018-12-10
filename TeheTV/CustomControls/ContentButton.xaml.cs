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
using static TeheTV.MainWindow;

namespace TeheTV
{
    /// <summary>
    /// Interaction logic for ContentButton.xaml
    /// </summary>
    public partial class ContentButton : UserControl
    {
        public event TeheTVEvent NotEnoughTimeEvent;
        private MainWindow app;
        private Content content;
        private TVScreen newTV;
        private bool isRecommended;

        public ContentButton(MainWindow instance, Content c) : this(instance, c, false) { }
        public ContentButton(MainWindow instance, Content c, bool recommended)
        {
            app = instance;
            content = c;
            isRecommended = recommended;

            InitializeComponent();

            Thumbnail.Source = c.Thumbnail;
            Watermark.Source = c.Watermark;
            Label.Content = c.Title;
        }

        private void throwNotEnoughTimeEvent()
        {
            if (NotEnoughTimeEvent != null) NotEnoughTimeEvent();
        }

        private void buttonClick(object sender, MouseButtonEventArgs e)
        {
            Profile P = SettingsManager.getCurrentProfile();
            if (P != null && (isRecommended || P.hasTime()))
            {
                Sounds.Play(Properties.Resources.soundButtonPress);
                TVScreen curTV = MainWindow.TvScreen;
                if (curTV == null)
                {
                    newTV = new TVScreen(content, isRecommended);
                    MainWindow.TvScreen = newTV;
                }
                else
                {
                    curTV.Update(content, isRecommended);
                }
            }
            else
            {
                Sounds.Play(Properties.Resources.soundAlert);
                throwNotEnoughTimeEvent();
            }
        }
    }
}
