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
    public partial class RecommendedContentControl : UserControl
    {
        private Content content;
        private StackPanel stackSearch;
        private StackPanel stackRecmnd;
        private bool added = false;

        public RecommendedContentControl(Content c, StackPanel searchPanel, StackPanel recommendedPanel, bool Recmnded)
        {
            content = c;
            stackSearch = searchPanel;
            stackRecmnd = recommendedPanel;
            added = Recmnded;

            InitializeComponent();

            Thumbnail.Source = c.Thumbnail;
            Watermark.Source = c.Watermark;
            Title.Content = c.Title;

            if (added)
                Recommend();
            else
                Unrecommend();
        }

        public void Recommend()
        {
            added = true;
            stackSearch.Children.Remove(this);
            stackRecmnd.Children.Add(this);
            BaseBlue.Visibility = Visibility.Visible;
            BaseBlack.Visibility = Visibility.Hidden;
            minus.Visibility = Visibility.Visible;
            plus.Visibility = Visibility.Hidden;
            SettingsManager.getCurrentProfile().AddRecommendation(content);
        }

        public void Unrecommend()
        {
            added = false;
            stackRecmnd.Children.Remove(this);
            stackSearch.Children.Add(this);
            BaseBlack.Visibility = Visibility.Visible;
            BaseBlue.Visibility = Visibility.Hidden;
            plus.Visibility = Visibility.Visible;
            minus.Visibility = Visibility.Hidden;
            SettingsManager.getCurrentProfile().RemoveRecommendation(content);
        }

        private void triggerClicked(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonPress);
            if (added)
                Unrecommend();
            else
                Recommend();
        }
    }
}
