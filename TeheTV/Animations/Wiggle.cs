using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TeheTV.Animations
{
    class Wiggle
    {
        public delegate void CustomAnimationEvent();
        public static event CustomAnimationEvent Completed;
        // ************************
        //  gridScreen Slide Stuff
        // ************************
        private static TimeSpan DURATION = TimeSpan.FromMilliseconds(25);
        private static double OFFSET = 10;
        private static int WIGGLEAMOUNT = 5;
        private static Grid _gridObject;
        private static double _oL;
        private static int count;
        // public Thickness (double left, double top, double right, double bottom);

        public static void Run(Grid obj)
        {
            _gridObject = obj;
            _oL = obj.Margin.Left;
            count = 0;
            Start();
        }
        private static void Start() { moveLeft(null, EventArgs.Empty); }

        private static void moveLeft(object s, EventArgs e)
        {
            var left = _oL;
            TranslateTransform trans = new TranslateTransform();
            _gridObject.RenderTransform = trans;
            DoubleAnimation anim = new DoubleAnimation(left, (left - OFFSET), DURATION);
            anim.Completed += moveRight;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }
        private static void moveRight(object s, EventArgs e)
        {
            var left = _oL;
            TranslateTransform trans = new TranslateTransform();
            _gridObject.RenderTransform = trans;
            DoubleAnimation anim = new DoubleAnimation(left, (left + OFFSET), DURATION);
            anim.Completed += finishOrLoop;
            trans.BeginAnimation(TranslateTransform.XProperty, anim);
        }

        private static void finishOrLoop(object s, EventArgs e)
        {
            if (count < WIGGLEAMOUNT)
            {
                count++;
                Start();
            }
            else
            {
                if (Completed != null) Completed();
                Completed = null;
            }
        }
    }
}
