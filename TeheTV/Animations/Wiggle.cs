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
    class Wiggle
    {
        // ************************
        //  gridScreen Slide Stuff
        // ************************
        private static Int32 _slideMiliSecs = 250;
        private static Grid _gridObject;
        private static Thickness _originalPosition;
        // public Thickness (double left, double top, double right, double bottom);

        public static void SlideUp(Grid obj)
        {
            _gridObject = obj;
            Thickness th = obj.Margin; ;
            _originalPosition = new Thickness(th.Left, th.Top, th.Right, th.Bottom);

            ThicknessAnimation animation = new ThicknessAnimation();
            animation.Duration = TimeSpan.FromMilliseconds(_slideMiliSecs);
            animation.From = new Thickness(0, 0, 0, 0);
            animation.To = new Thickness(0, 0, 0, 0);
            playSound(true);
            _gridObject.BeginAnimation(Grid.MarginProperty, animation);
        }
        public static void SlideDown()
        {
            if (_gridObject != null)
            {

                ThicknessAnimation animation = new ThicknessAnimation();
                animation.Duration = TimeSpan.FromMilliseconds(_slideMiliSecs);
                animation.From = new Thickness(0, 0, 0, 0);
                animation.To = new Thickness(0, 0, 0, 0);
                playSound(false);
                _gridObject.BeginAnimation(Grid.MarginProperty, animation);
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
