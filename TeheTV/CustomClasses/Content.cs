using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TeheTV
{
    public enum ContentType
    {
        RECOMMENDED,
        NETFLIX,
        SPOTIFY,
        YOUTUBE,
        GAMES,
        SEARCH
    }

    public class Content
    {
        private ContentType type;
        private int ageMin, ageMax;
        private string title, contentPath, imagePath, filename;

        public Content(ContentType t, int minAge, int maxAge, string Title, string contentPath, string thumnailPath, string fileName)
        {
            type = t;
            ageMin = minAge;
            ageMax = maxAge;
            title = Title;
            this.contentPath = contentPath;
            imagePath = thumnailPath;
            filename = fileName;
        }

        public bool EqualsType(ContentType t)
        {
            return type == t;
        }

        public string Title
        {
            get { return title; }
        }

        public string ContentPath
        {
            get { return contentPath; }
        }

        public int MinAge
        {
            get { return ageMin; }
        }

        public int MaxAge
        {
            get { return ageMax; }
        }

        public BitmapImage Thumbnail
        {
            get
            {
                Uri uri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                BitmapImage image = new BitmapImage(uri);
                return image;
            }
        }

        public BitmapImage Watermark
        {
            get
            {
                string path;
                if (type == ContentType.NETFLIX)
                    path = "/resources/navicon_netflix.png";
                else if (type == ContentType.SPOTIFY)
                    path = "/resources/navicon_spotify.png";
                else
                    path = "/resources/navicon_youtube.png";
                
                Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);
                BitmapImage image = new BitmapImage(uri);
                return image;
            }
        }

        public string toStringFilename()
        {
            return filename;
        }
    }
}
