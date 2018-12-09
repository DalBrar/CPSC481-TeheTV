using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace TeheTV
{
    public class ContentManager
    {
        public enum AgeGroup
        {
            g1_4_to_6,
            g2_6_to_8,
            g3_8_to_10
        }

        private static string ContentFolder = SettingsManager.appFolder + "Content/";
        private const string defaultVideoImage = "/resources/defaultVideoImage.png";
        private const string defaultMusicImage = "/resources/defaultMusicImage.png";
        private static List<Content> list4to6;
        private static List<Content> list6to8;
        private static List<Content> list8to10;
        private static int errors = 0;

        public static void initialize()
        {
            list4to6 = new List<Content>();
            list6to8 = new List<Content>();
            list8to10 = new List<Content>();
            
            if (Directory.Exists(ContentFolder))
            {
                loadContent();
            }
            else
                displayNoContentMsg();
        }

        public static List<Content> getListForAge(int age)
        {
            if (age <= 6)
                return list4to6;
            if (age >= 6 && age <= 8)
                return list6to8;
            if (age >= 8)
                return list8to10;
            return new List<Content>();
        }

        private static void displayNoContentMsg()
        {
            MessageBox.Show("Error: There is no content for the app to use!\n" +
                            "----------------------------------------------\n" +
                            "Please place content in:\n" +
                            "     currentDirectory\\appdata\\Content\\\n\n" +
                            "With filenames in the form of:\n" +
                            "     <contentType>_<minAge>_<maxAge>_<name>.<ext>\n\n" +
                            "where:\n" +
                            "     contentType  = youtube, netflix, spotify\n" +
                            "     minAge & maxAge  = integers of the corresponding age\n" +
                            "     name  = The name of your content\n" +
                            "     ext  = mp4 for videos, mp3 for music, or jpg for thumbnails.\n\n" +
                            "**Note: thumbnail filenames must match exactly to their representive content.\n\n" +
                            "Restart the app for changes to take effect.");
        }

        private static void loadContent()
        {
            List<string> youtubeFiles;
            List<string> netflixFiles;
            List<string> spotifyFiles;
            List<string> imageFiles;
            try
            {
                youtubeFiles = Directory.GetFiles(ContentFolder, "youtube*.mp4").Select(Path.GetFileName).ToList();
                netflixFiles = Directory.GetFiles(ContentFolder, "netflix*.mp4").Select(Path.GetFileName).ToList();
                spotifyFiles = Directory.GetFiles(ContentFolder, "spotify*.mp3").Select(Path.GetFileName).ToList();
                imageFiles = Directory.GetFiles(ContentFolder, "*.jpg").Select(Path.GetFileName).ToList();
            }
            catch
            {
                displayNoContentMsg();
                return;
            }

            iterateThroughList(ContentType.NETFLIX, netflixFiles, imageFiles);
            iterateThroughList(ContentType.SPOTIFY, spotifyFiles, imageFiles);
            iterateThroughList(ContentType.YOUTUBE, youtubeFiles, imageFiles);
        }

        private static void iterateThroughList(ContentType t, List<string> list, List<string> images)
        {
            TextInfo STRING = new CultureInfo("en-US", false).TextInfo;
            string ext = ".mp4";
            string defaultImagePath = defaultVideoImage;

            if (t == ContentType.SPOTIFY)
            {
                ext = ".mp3";
                defaultImagePath = defaultMusicImage;
            }

            foreach (string file in list)
            {
                string temp = file;

                string filePath = ContentFolder + temp;
                string filename = Regex.Replace(temp, ext, "", RegexOptions.IgnoreCase);
                string thumbnail = Regex.Replace(temp, ext, ".jpg", RegexOptions.IgnoreCase);

                string thumbPath = ContentFolder;
                int thumbIndex = images.FindIndex(x => x.Equals(thumbnail, StringComparison.OrdinalIgnoreCase));
                if (thumbIndex != -1)
                    thumbPath += images[thumbIndex];
                else
                    thumbPath += defaultImagePath;

                string[] parts = filename.Split('_');
                if (parts.Length == 4)
                {
                    int min, max;
                    try
                    {
                        min = int.Parse(parts[1]);
                        max = int.Parse(parts[2]);
                    }
                    catch
                    {
                        errors++;
                        continue;
                    }

                    string title = STRING.ToTitleCase(parts[3]);
                    if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
                    {
                        errors++;
                        continue;
                    }

                    Content c = new Content(t, min, max, title, filePath, thumbPath);
                    addToLists(c, min, max);
                }
                else
                    errors++;
            }
        }

        private static void addToLists(Content c, int min, int max)
        {
            if (min <= 6)
                list4to6.Add(c);
            if ((min >= 6 && min <= 8) || (max >= 6 && max <= 8))
                list6to8.Add(c);
            if (max >= 8)
                list8to10.Add(c);
        }
    }
}
