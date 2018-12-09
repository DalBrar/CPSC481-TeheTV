using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeheTV
{
    public class Profile
    {
        private string name;
        private int month;
        private int day;
        private int year;
        private int age;
        private string profilePath;
        private static string profileInfoFile;
        private int folder;

        public Profile(string Name, int Month, int Day, int Year)
        {
            name = Name;
            month = Month;
            day = Day;
            year = Year;
            age = calculateAge();

            folder = getNextFolder();
            profilePath = SettingsManager.profilesFolder + folder + "/";
            profileInfoFile = profilePath + "info.profile";
        }

        public string Name
        {
            get { return name; }
        }

        public string getName()
        {
            return name;
        }

        public int getAge()
        {
            return age;
        }

        public void saveProfile()
        {
            Directory.CreateDirectory(profilePath);
            List<string> info = new List<string>();
            info.Add("name= " + name);
            info.Add("mm= " + month);
            info.Add("dd= " + day);
            info.Add("yy= " + year);
            File.WriteAllLines(profileInfoFile, info);
        }

        public List<Content> getNetflixContent() { return getSpecificContent(ContentType.NETFLIX); }
        public List<Content> getSpotifyContent() { return getSpecificContent(ContentType.SPOTIFY); }
        public List<Content> getYouTubeContent() { return getSpecificContent(ContentType.YOUTUBE); }

        private List<Content> getSpecificContent(ContentType t)
        {
            List<Content> list = ContentManager.getListForAge(age);
            List<Content> output = new List<Content>();
            foreach (Content c in list)
            {
                if (c.EqualsType(t))
                    output.Add(c);
            }
            return output;
        }

        private int calculateAge()
        {
            var today = DateTime.Today;
            var dob = new DateTime(year, month, day);
            // calculate age based on year
            var age = today.Year - dob.Year;
            // calculate age based on exact day
            if (dob > today.AddYears(-age))
                age--;

            return age;
        }

        private int getNextFolder()
        {
            int n = 1;
            while (Directory.Exists(SettingsManager.profilesFolder + n + "/"))
                n++;
            return n;
        }
    }
}
