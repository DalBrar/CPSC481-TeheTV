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

            initializeStackPanels();
        }

        private void backBtnPressed(object sender, MouseButtonEventArgs e) { app.ScreenGoBack(); }
        
        private void searchbarClicked(object sender, MouseButtonEventArgs e)
        {
            keyboard.SlideUp();
        }

        private void updateSearchResults(string output)
        {
            resultContent.Children.Clear();
        }

        private void initializeStackPanels()
        {
            List<Content> rec = SettingsManager.getCurrentProfile().Recommendations.ToList();
            List<Content> ser = ContentManager.getMasterList();

            foreach (Content c in ser)
            {
                if (!rec.Contains(c))
                    new RecommendedContentControl(c, resultContent, reccdContent, false);
            }

            foreach (Content c in rec)
                new RecommendedContentControl(c, resultContent, reccdContent, true);
        }

        // Scrolling methods
        Point scrollMousePoint = new Point();
        double hOff = 1;

        private void ScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            scrollMousePoint = e.GetPosition(scrollViewer);
            hOff = scrollViewer.HorizontalOffset;
            scrollViewer.CaptureMouse();
        }

        private void ScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.IsMouseCaptured)
            {
                scrollViewer.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(scrollViewer).X));
            }
        }

        private void ScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            scrollViewer.ReleaseMouseCapture();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + e.Delta);
        }
    }
}
