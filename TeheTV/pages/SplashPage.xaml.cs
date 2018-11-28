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
    public partial class SplashPage : Page
    {
        MainWindow app;
        public SplashPage(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            this.Loaded += animateSplash;
        }

        private void animateSplash(object sender, RoutedEventArgs e)
        {
            System.Windows.Media.Animation.Storyboard sb = this.FindResource("BGfadeIn") as System.Windows.Media.Animation.Storyboard;
            System.Windows.Media.Animation.Storyboard.SetTarget(sb, this.blackBGD);
            sb.Completed += FadeIn_Completed;
            sb.Begin();
        }

        private void FadeIn_Completed(object sender, EventArgs e)
        {
            System.Windows.Media.Animation.Storyboard sb = this.FindResource("BGfadeOut") as System.Windows.Media.Animation.Storyboard;
            System.Windows.Media.Animation.Storyboard.SetTarget(sb, this.blackBGD);
            sb.Completed += FadeOut_Complete;
            sb.Begin();
        }

        private void FadeOut_Complete(object sender, EventArgs e)
        {
            
        }

        //  Style="{DynamicResource BGAnimation}"
    }
}
