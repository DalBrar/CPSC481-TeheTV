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
    /// Set button action with: btn.ButtonAction += new
    /// Usage: <TeheTV:SettingsButton x:Name="btnName" Label="Button Label" ImagePath="/resources/btn_image.png"  PreviewMouseLeftButtonDown="buttonMethod"/>
    /// </summary>
    public partial class SettingsButton : UserControl
    {
        public delegate void SettingsButtonEvent();
        public event SettingsButtonEvent ButtonAction;

        public SettingsButton()
        {
            InitializeComponent();
        }

        public string Label
        {
            set { LabelHolder.Text = value; }
        }

        public string ImagePath
        {
            set
            {
                Uri uri = new Uri(value, UriKind.RelativeOrAbsolute);
                BitmapImage image = new BitmapImage(uri);
                ImageHolder.Source = image;
            }
        }

        private void settingsButtonControl_Pressed(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            if (ButtonAction != null)
                ButtonAction();
        }
    }
}
