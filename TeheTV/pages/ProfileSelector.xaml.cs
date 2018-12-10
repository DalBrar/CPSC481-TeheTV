using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class ProfileSelector : Page
    {
        MainWindow app;

        public ProfileSelector(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            BindProfiles();
        }

        private void BindProfiles()
        {
            foreach (Profile currP in SettingsManager.GetProfiles())
            {
                ProfileButton button = new ProfileButton(app, currP);
                profileArea.Children.Add(button);
            }
        }

        private void gearBtnPressed(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            app.ScreenChangeTo(SCREEN.Options, true);
        }

        // Scrolling methods
        Point scrollMousePoint = new Point();
        double vOff = 1;

        private void ScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollViewer);
            vOff = scrollViewer.VerticalOffset;
            scrollViewer.CaptureMouse();
        }

        private void ScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollViewer.IsMouseCaptured)
            {
                scrollViewer.ScrollToVerticalOffset(vOff + (scrollMousePoint.Y - e.GetPosition(scrollViewer).Y));
            }
        }

        private void ScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.ReleaseMouseCapture();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + e.Delta);
        }
    }
}
