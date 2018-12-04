using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace TeheTV
{
    class Sounds
    {
        /// <summary></summary>
        /// Plays the sound and waits till the sound is complete before executing next command.
        /// <param name="stream">Properties.Resources.sound</param>
        public static void PlayNWait(Stream stream)
        {
            playSound(stream, true);
        }
        /// <summary></summary>
        /// Plays the sound asyncronously
        /// 
        /// <param name="stream">Properties.Resources.sound</param>
        public static void Play(Stream stream)
        {
            playSound(stream, false);
        }

        private static void playSound(Stream stream, bool wait)
        {
            SoundPlayer player = new SoundPlayer(stream);
            player.Load();
            if (wait)
                player.PlaySync();
            else
                player.Play();
        }
    }
}
