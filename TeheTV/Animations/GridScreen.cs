using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TeheTV.Animations
{
    class GridScreen
    {
        // ************************
        //  gridScreen Slide Stuff
        // ************************
        private static Int32 _slideMiliSecs = 250;
        private static double _topSpacing = 50;
        private static Grid _slidingGrid;
        private static double _slidingAmount;
        // public Thickness (double left, double top, double right, double bottom);

        public static void SlideUp(Grid obj, double verticalOffset)
        {
            _slidingGrid = obj;
            _slidingAmount = -(verticalOffset - _topSpacing);

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.Duration = TimeSpan.FromMilliseconds(_slideMiliSecs);
            animation.From = new Thickness(0, 0, 0, 0);
            animation.To = new Thickness(0, _slidingAmount, 0, 0);
            playSound(true);
            _slidingGrid.BeginAnimation(Grid.MarginProperty, animation);
        }
        public static void SlideDown()
        {
            if (_slidingGrid != null)
            {

                ThicknessAnimation animation = new ThicknessAnimation();
                animation.Duration = TimeSpan.FromMilliseconds(_slideMiliSecs);
                animation.From = new Thickness(0, _slidingAmount, 0, 0);
                animation.To = new Thickness(0, 0, 0, 0);
                playSound(false);
                _slidingGrid.BeginAnimation(Grid.MarginProperty, animation);
            }
        }

        private static void playSound(bool _slideUp)
        {
            if (_slideUp)
                Sounds.Play(Properties.Resources.soundSlideUp);
            else
                Sounds.Play(Properties.Resources.soundSlideDown);
        }
    }
}
