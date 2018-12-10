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
using static TeheTV.ProfileDeleteButton;

namespace TeheTV.Pages
{
    public partial class DeleteAccount : Page
    {
        MainWindow app;
        private Profile pToDelete;
        private ProfileDeleteButton bToDelete;

        public DeleteAccount(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            BindProfiles();
            hideCautionModal();
        }

        private void BindProfiles()
        {
            foreach (Profile currP in SettingsManager.GetProfiles())
            {
                ProfileDeleteButton button = new ProfileDeleteButton(app, currP);
                button.ButtonDownEvent += new DeleteButtonEventHandler(processButtonClick);
                profileArea.Children.Add(button);
            }
        }

        private void backBtnPressed(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            if (SettingsManager.getCurrentProfile() == null && SettingsManager.getProfilesCount() <= 0)
                app.ScreenChangeTo(SCREEN.CreateNewAccount);
            else if (SettingsManager.getProfilesCount() <= 0)
                app.ScreenChangeTo(SCREEN.CreateNewAccount);
            else if (SettingsManager.getCurrentProfile() == null)
                app.ScreenChangeTo(SCREEN.ProfileSelector);
            else
                app.ScreenGoBack();
        }

        private void processButtonClick(ProfileDeleteButton b, Profile p)
        {
            pToDelete = p;
            bToDelete = b;
            cautionProfileName.Text = p.Name;
            Animations.Modal.ModalFadeIn(cautionGrid, false);
        }

        private void cancelBtnClicked(object sender, MouseButtonEventArgs e)
        {
            pToDelete = null;
            bToDelete = null;
            Animations.Modal.ModalFadeOut();
            cautionProfileName.Text = "This Profile";
            Sounds.Play(Properties.Resources.soundPassCorrect);
        }

        private void confirmBtnClicked(object sender, MouseButtonEventArgs e)
        {
            SettingsManager.deleteProfile(pToDelete);
            profileArea.Children.Remove(bToDelete);
            pToDelete = null;
            bToDelete = null;
            Animations.Modal.ModalFadeOut();
            cautionProfileName.Text = "This Profile";
            Sounds.Play(Properties.Resources.soundToggle);
            if (profileArea.Children.Count <= 0)
                app.ScreenChangeTo(SCREEN.CreateNewAccount);
        }

        private void hideCautionModal()
        {
            cautionGrid.Visibility = Visibility.Hidden;
            cautionGrid.Opacity = 0;
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
