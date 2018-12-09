using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeheTV
{
    public enum ContentType
    {
        NETFLIX,
        SPOTIFY,
        YOUTUBE
    }

    public class Content
    {
        private ContentType type;
        private int ageMin, ageMax;
        private string title, contentPath, imagePath;

        public Content(ContentType t, int minAge, int maxAge, string Title, string contentPath, string thumnailPath)
        {
            type = t;
            ageMin = minAge;
            ageMax = maxAge;
            title = Title;
            this.contentPath = contentPath;
            imagePath = thumnailPath;
        }

        public bool EqualsType(ContentType t)
        {
            return type == t;
        }
    }
}
