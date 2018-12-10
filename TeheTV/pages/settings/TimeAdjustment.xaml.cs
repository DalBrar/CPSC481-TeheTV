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
    public partial class TimeAdjustment : Page
    {
        MainWindow app;
        int field = 0;

        public TimeAdjustment(MainWindow instance)
        {
            app = instance;
            InitializeComponent();

            totalStarsField.Text = "" + SettingsManager.getCurrentProfile().Stars;
            rewardStarsField.Text = "" + SettingsManager.getStarRewardAmount();
            costStarsField.Text = "" + SettingsManager.getStarCostAmount();

            keyboard.ReturnKeyText = "Set";
            keyboard.IsPassword = false;
            keyboard.EmptySpaceReturn = true;
            keyboard.ClearValuesOnSlideUp = false;
            keyboard.keyboardStyle = Keyboard.KeyboardStyle.NUMERICAL;
            keyboard.ReturnEvent += new CustomKeyboardEventHandler(saveChanges);
        }

        private void backBtnPressed(object sender, MouseButtonEventArgs e)
        {
            Sounds.Play(Properties.Resources.soundButtonClick);
            app.ScreenGoBack();
        }

        private void totalStarsClicked(object sender, MouseButtonEventArgs e)
        {
            keyboard.OutputTextBlock = totalStarsField;
            keyboard.MaxInputLength = 11;
            field = 1;
            selectField();
            slideGridUp(200);
            keyboard.SlideUp();
        }

        private void rewardStarsClicked(object sender, MouseButtonEventArgs e)
        {
            keyboard.OutputTextBlock = rewardStarsField;
            keyboard.MaxInputLength = 3;
            field = 2;
            selectField();
            slideGridUp(350);
            keyboard.SlideUp();
        }

        private void costStarsClicked(object sender, MouseButtonEventArgs e)
        {
            keyboard.OutputTextBlock = costStarsField;
            keyboard.MaxInputLength = 3;
            field = 3;
            selectField();
            slideGridUp(700);
            keyboard.SlideUp();
        }

        private void selectField()
        {
            Rectangle box;
            if (field == 1) box = totalStarsRec;
            if (field == 2) box = rewardStarsRec;
            else box = costStarsRec;

            deselectFields();
            box.Fill = MainWindow.setBrushColor(100, 255, 255, 255);
            Sounds.Play(Properties.Resources.soundButtonClick);
        }

        private void deselectFields()
        {
            byte col = 150;
            totalStarsRec.Fill = MainWindow.setBrushColor(63, col, col, col);
            rewardStarsRec.Fill = MainWindow.setBrushColor(63, col, col, col);
            costStarsRec.Fill = MainWindow.setBrushColor(63, col, col, col);
        }

        private void slideGridUp(double amount)
        {
            Animations.GridScreen.SlideUp(TimeAdjustGrid, amount);
        }

        private void saveChanges(string output)
        {
            Animations.GridScreen.SlideDown();
            if (field == 1)
            {
                int n = int.Parse(totalStarsField.Text);
                SettingsManager.getCurrentProfile().setStars(n);
            }
            if (field == 2)
            {
                int n = int.Parse(rewardStarsField.Text);
                SettingsManager.setStarRewardAmount(n);
            }
            if (field == 3)
            {
                int n = int.Parse(costStarsField.Text);
                SettingsManager.setStarCostAmount(n);
            }
            field = 0;
        }
    }
}
