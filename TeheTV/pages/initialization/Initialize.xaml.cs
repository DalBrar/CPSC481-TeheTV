using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
    public partial class Initialize : Page
    {
        MainWindow app;
        private Page init2;
        public Initialize(MainWindow instance)
        {
            app = instance;
            InitializeComponent();
            init2 = new Initialize2(app);
        }

        private void btnStartOnMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            app.ScreenChangeTo(init2, true);
        }
    }
}
