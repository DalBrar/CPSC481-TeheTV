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

namespace TeheTV.pages
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

        // ******************************
        //  Click and Drag Scrollability
        // ******************************

        private Point scrollMousePoint = new Point();
        private double hOff = 1;

        private void mouseDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollViewer);
            hOff = scrollViewer.HorizontalOffset;
            scrollViewer.CaptureMouse();

        }

        private void mouseUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.ReleaseMouseCapture();
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            if (scrollViewer.IsMouseCaptured)
            {
                scrollViewer.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(scrollViewer).X));
            }
        }

        private void mouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + e.Delta);
        }

        private void mouseMove(object sender, DragEventArgs e)
        {
        
        }
    }
}
