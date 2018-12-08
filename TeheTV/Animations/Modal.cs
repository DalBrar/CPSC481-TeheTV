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
    class Modal
    {
        // ******************
        //  Modal Fade Stuff
        // ******************
        private static Grid _fadingObj;
        private static bool _success = false;

        public static void ModalFadeIn(Grid obj, bool success)
        {
            _fadingObj = obj;
            _success = success;

            _fadingObj.Opacity = 0;
            _fadingObj.Visibility = Visibility.Visible;

            DoubleAnimation animation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(100)));
            animation.Completed += playSound;
            _fadingObj.BeginAnimation(Grid.OpacityProperty, animation);
        }
        public static void ModalFadeOut()
        {
            if (_fadingObj != null)
            {
                DoubleAnimation animation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(100)));
                animation.Completed += ModalHidden;
                _fadingObj.BeginAnimation(Grid.OpacityProperty, animation);
            }
        }

        private static void ModalHidden(object senderNotUsed, EventArgs eNotUsed)
        {
            _fadingObj.Opacity = 0;
            _fadingObj.Visibility = Visibility.Hidden;
        }

        private static void playSound(object senderNotUsed, EventArgs eNotUsed)
        {
            if (_success)
                Sounds.Play(Properties.Resources.soundPassCorrect);
            else
                Sounds.Play(Properties.Resources.soundUhOh);
        }
    }
}
