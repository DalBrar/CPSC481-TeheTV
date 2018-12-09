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
        private static List<Content> masterList;
        private static List<Content> list4to6;
        private static List<Content> list6to8;
        private static List<Content> list8to10;
        private static List<string> imagesList;
        private static int errors = 0;

        public static void initialize()
        {
            masterList = new List<Content>();
            list4to6 = new List<Content>();
            list6to8 = new List<Content>();
            list8to10 = new List<Content>();
            imagesList = new List<string>();
            
            if (Directory.Exists(ContentFolder))
            {
                loadContent();
            }
            else
                displayNoContentMsg();
        }

        public static List<string> getImagesList() { return imagesList; }
        public static List<Content> getMasterList() { return masterList; }

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
            try
            {
                youtubeFiles = Directory.GetFiles(ContentFolder, "youtube*.mp4").Select(Path.GetFileName).ToList();
                netflixFiles = Directory.GetFiles(ContentFolder, "netflix*.mp4").Select(Path.GetFileName).ToList();
                spotifyFiles = Directory.GetFiles(ContentFolder, "spotify*.mp3").Select(Path.GetFileName).ToList();
                imagesList = Directory.GetFiles(ContentFolder, "*.jpg").Select(Path.GetFileName).ToList();
            }
            catch
            {
                imagesList = new List<string>();
                displayNoContentMsg();
                return;
            }

            iterateThroughList(ContentType.NETFLIX, netflixFiles);
            iterateThroughList(ContentType.SPOTIFY, spotifyFiles);
            iterateThroughList(ContentType.YOUTUBE, youtubeFiles);
        }

        private static void iterateThroughList(ContentType t, List<string> list)
        {

            foreach (string file in list)
            {
                Content c = loadFile(file, t);
                if (c == null)
                {
                    errors++;
                    continue;
                }
                addToLists(c);
            }
        }

        public static Content getContentFromFile(string file)
        {
            if (file.StartsWith("netflix", StringComparison.OrdinalIgnoreCase))
                return loadFile(file, ContentType.NETFLIX);
            if (file.StartsWith("spotify", StringComparison.OrdinalIgnoreCase))
                return loadFile(file, ContentType.SPOTIFY);
            if (file.StartsWith("youtube", StringComparison.OrdinalIgnoreCase))
                return loadFile(file, ContentType.YOUTUBE);
            return null;
        }
        private static Content loadFile(string filenameExt, ContentType t)
        {
            TextInfo STRING = new CultureInfo("en-US", false).TextInfo;
            string ext = ".mp4";
            string defaultImagePath = defaultVideoImage;

            if (t == ContentType.SPOTIFY)
            {
                ext = ".mp3";
                defaultImagePath = defaultMusicImage;
            }

            string filePath = ContentFolder + filenameExt;
            string filename = Regex.Replace(filenameExt, ext, "", RegexOptions.IgnoreCase);
            string thumbnail = Regex.Replace(filenameExt, ext, ".jpg", RegexOptions.IgnoreCase);

            string thumbPath;
            int thumbIndex = imagesList.FindIndex(x => x.Equals(thumbnail, StringComparison.OrdinalIgnoreCase));
            if (thumbIndex != -1)
                thumbPath = ContentFolder + imagesList[thumbIndex];
            else
                thumbPath = defaultImagePath;

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
                    return null;
                }

                string title = STRING.ToTitleCase(parts[3]);
                if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
                    return null;

                return new Content(t, min, max, title, filePath, thumbPath, filenameExt);
            }
            else
                return null;
        }

        private static void addToLists(Content c)
        {
            int min = c.MinAge;
            int max = c.MaxAge;

            masterList.Add(c);
            if (min <= 6)
                list4to6.Add(c);
            if ((min >= 6 && min <= 8) || (max >= 6 && max <= 8))
                list6to8.Add(c);
            if (max >= 8)
                list8to10.Add(c);
        }
    }
}
