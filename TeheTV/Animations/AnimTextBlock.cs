using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TeheTV.Animations
{
    class AnimTextBlock
    {
        private static TimeSpan DURATION = TimeSpan.FromMilliseconds(2000);

        // haven't gotten it working it :(
        private static void GradualFadout(TextBlock obj, Color brushFrom, Color brushTo)
        {
            SolidColorBrush brush = (SolidColorBrush) obj.Foreground;
            ColorAnimation anim = new ColorAnimation();
            anim.Duration = DURATION;
            anim.From = brushFrom;
            anim.To = brushTo;
            brush.BeginAnimation(SolidColorBrush.ColorProperty, anim);
        }
    }
}
