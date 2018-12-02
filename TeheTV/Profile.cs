using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeheTV
{
    class Profile
    {
        private string name;
        private int month;
        private int day;
        private int year;
        private int age;
        private string profilePath;
        private static string profileInfoFile;

        public Profile(string Name, int Month, int Day, int Year)
        {
            name = Name;
            month = Month;
            day = Day;
            year = Year;
            age = calculateAge();
            profilePath = SettingsManager.profilesFolder + getSafeName(name) + "/";
            profileInfoFile = profilePath + "info.profile";
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
            info.Add(name);
            info.Add("" + month);
            info.Add("" + day);
            info.Add("" + year);
            File.WriteAllLines(profileInfoFile, info);
        }

        private string getSafeName(string name)
        {
            return name.Replace(" ", "_");
        }
    }
}
