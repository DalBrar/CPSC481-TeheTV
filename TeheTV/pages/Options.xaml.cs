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
    public partial class Options : Page, INotifyPropertyChanged
    {
        MainWindow app;
        private string _currentProfile;
        public event PropertyChangedEventHandler PropertyChanged;
        // return button should navigate back to home
        private Page home;
        private Page currentProfile;
        private Page parentSettings;

        public Options(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            currentProfile = new ProfileSelector(app);
            parentSettings = new ParentSettings(app);
        }

        public string CurrentProfile
        {
            get
            {
                return _currentProfile;
            }
            set
            {
                _currentProfile = value;
                // If anywhere uses CurrentProfile, then update their information
                OnPropertyChanged("CurrentProfile");
            }
        }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void CurrentProfileButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(currentProfile, true);
        }

        private void ChangeSettings_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            app.ScreenChangeTo(parentSettings, true);
        }

        private void Home_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
