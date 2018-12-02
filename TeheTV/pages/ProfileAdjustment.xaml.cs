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
    /// <summary>
    /// Interaction logic for ProfileAdjustment.xaml
    /// </summary>
    public partial class ProfileAdjustment : Page, INotifyPropertyChanged
    {
        private string _profileName = "Joseph";
        public event PropertyChangedEventHandler PropertyChanged;
        MainWindow app;

        public ProfileAdjustment()
        {
            InitializeComponent();
            ProfileName = "2";
        }

        public string ProfileName
        {
            get
            {
                return _profileName;
            }
            set
            {
                _profileName = value;
                // If anywhere uses CurrentProfile, then update their information
                OnPropertyChanged("ProfileName");
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
    }
}
