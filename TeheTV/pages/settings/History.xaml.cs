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

namespace TeheTV.Pages
{
    /// <summary>
    /// Interaction logic for Histroy.xaml
    /// </summary>
    public partial class History : Page
    {
        MainWindow app;
        private int col;
        private BitmapImage netflix = getImgSource("/resources/navicon_netflix.png");
        private BitmapImage youtube = getImgSource("/resources/navicon_youtube.png");
        private BitmapImage spotify = getImgSource("/resources/navicon_spotify.png");

        public History(MainWindow instance, int columnHeader)
        {
            MainWindow.HistorySortCount++;
            col = columnHeader;
            app = instance;
            InitializeComponent();
            loadData();
        }

        private void backBtnPressed(object sender, MouseButtonEventArgs e)
        {
            while (MainWindow.HistorySortCount > 0)
            {
                MainWindow.HistorySortCount--;
                app.ScreenGoBack();
            }
            Sounds.Play(Properties.Resources.soundButtonClick);
        }

        private void namePressed(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonPress);
            app.ScreenChangeTo(new History(app, 1), false);
        }

        private void typePressed(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonPress);
            app.ScreenChangeTo(new History(app, 2), false);
        }

        private void datePressed(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonPress);
            app.ScreenChangeTo(new History(app, 3), false);
        }

        private void rewardPressed(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonPress);
            app.ScreenChangeTo(new History(app, 4), false);
        }

        private void loadData()
        {
            // name
            if (col == 1)
            {
                setRow1("/resources/History/Bill_Nye.jpg", "Bill Nye", netflix, "10/30/18", true, "+2:00");
                setRow2("/resources/History/Teletubbies.jpg", "Teletubbies", youtube, "11/15/18", true, "+5:00");
                setRow3("/resources/History/Polite_Words.jpg", "Use Polite Words", netflix, "09/28/18", false, "");
            }
            // type
            if (col == 2)
            {
                setRow1("/resources/History/Bill_Nye.jpg", "Bill Nye", netflix, "10/30/18", true, "+2:00");
                setRow2("/resources/History/Polite_Words.jpg", "Use Polite Words", netflix, "09/28/18", false, "");
                setRow3("/resources/History/Teletubbies.jpg", "Teletubbies", youtube, "11/15/18", true, "+5:00");
            }
            // date
            if (col == 3)
            {
                setRow1("/resources/History/Polite_Words.jpg", "Use Polite Words", netflix, "09/28/18", false, "");
                setRow2("/resources/History/Bill_Nye.jpg", "Bill Nye", netflix, "10/30/18", true, "+2:00");
                setRow3("/resources/History/Teletubbies.jpg", "Teletubbies", youtube, "11/15/18", true, "+5:00");
            }
            // achievement
            if (col == 4)
            {
                setRow1("/resources/History/Teletubbies.jpg", "Teletubbies", youtube, "11/15/18", true, "+5:00");
                setRow2("/resources/History/Bill_Nye.jpg", "Bill Nye", netflix, "10/30/18", true, "+2:00");
                setRow3("/resources/History/Polite_Words.jpg", "Use Polite Words", netflix, "09/28/18", false, "");
            }
        }

        private void setRow1(string imgPath, string title, BitmapImage logo, string date, bool reward, string time)
        {
            img1.Source = getImgSource(imgPath);
            name1.Content = title;
            logo1.Source = logo;
            date1.Content = date;
            if (reward)
            {
                reward1.Source = getImgSource("/resources/navicon_star.png");
                time1.Content = time;
            }
        }
        private void setRow2(string imgPath, string title, BitmapImage logo, string date, bool reward, string time)
        {
            img2.Source = getImgSource(imgPath);
            name2.Content = title;
            logo2.Source = logo;
            date2.Content = date;
            if (reward)
            {
                reward2.Source = getImgSource("/resources/navicon_star.png");
                time2.Content = time;
            }
        }
        private void setRow3(string imgPath, string title, BitmapImage logo, string date, bool reward, string time)
        {
            img3.Source = getImgSource(imgPath);
            name3.Content = title;
            logo3.Source = logo;
            date3.Content = date;
            if (reward)
            {
                reward3.Source = getImgSource("/resources/navicon_star.png");
                time3.Content = time;
            }
        }
        private static BitmapImage getImgSource(string path)
        {
            Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
            return new BitmapImage(uri);
        }
    }

}
