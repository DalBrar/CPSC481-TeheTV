using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TeheTV
{
    public class Profile
    {
        private string name;
        private int month;
        private int day;
        private int year;
        private int age;
        private int time;
        private string profilePath;
        private string profileInfoFile;
        private string profileWatchedFile;
        private string profileRecommendedFile;
        private List<Content> recommended = new List<Content>();
        private List<string> watched;

        public Profile(string Name, int Month, int Day, int Year, int Time) : this(Name, Month, Day, Year, Time, "" + getNextFolder()) {}
        public Profile(string Name, int Month, int Day, int Year, int Time, string folderName)
        {
            name = Name;
            month = Month;
            day = Day;
            year = Year;
            age = calculateAge();
            time = Time;
            recommended = getRecommendations();
            watched = new List<string>();
            
            profilePath = SettingsManager.profilesFolder + folderName + "/";
            profileInfoFile = profilePath + "info.profile";
            profileWatchedFile = profilePath + "watchHistory.profile";
            profileRecommendedFile = profilePath + "recommended.profile";
        }

        public string Name
        {
            get { return name; }
        }

        public List<Content> Recommendations
        {
            get { return recommended; }
            set { recommended = value; }
        }

        public List<string> WatchHistory
        {
            get { return watched; }
            set { watched = value; }
        }

        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public void AddRecommendation(Content c)
        {
            if (!recommended.Contains(c))
            {
                recommended.Add(c);
                saveProfile();
            }
        }

        public void RemoveRecommendation(Content c)
        {
            recommended.Remove(c);
            saveProfile();
        }

        public void AddWatched(Content c)
        {
            if (!watched.Contains(c.toStringFilename()))
            {
                watched.Add(c.toStringFilename());
                recommended.Remove(c);
                saveProfile();
            }
        }

        public bool hasTime()
        {
            return time > 0;
        }

        public void AddTime(int t)
        {
            time += t;
            saveProfile();
        }

        public void ReduceTime()
        {
            if (time > 0)
                time--;
            saveProfile();
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
            info.Add("time= " + time);
            File.WriteAllLines(profileInfoFile, info);

            List<string> rec = new List<string>();
            foreach (Content c in recommended)
                rec.Add(c.toStringFilename());
            File.WriteAllLines(profileRecommendedFile, rec);

            File.WriteAllLines(profileWatchedFile, watched);
        }
        public List<Content> getContentForProfile() { return ContentManager.getListForAge(age); }
        public List<Content> getContentType(ContentType t)
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

        public void DeleteProfile()
        {
            EmptyFolder(profilePath);
        }

        private void EmptyFolder(string path)
        {
            foreach (string file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }
            foreach (string folder in Directory.GetDirectories(path))
            {
                EmptyFolder(folder);
                Directory.Delete(folder);
            }
            Directory.Delete(path);
        }

        private List<Content> getRecommendations()
        {
            List<Content> master = ContentManager.getMasterList();
            int len = master.Count;

            if (len > 6)
            {
                List<Content> rec = new List<Content>();
                List<int> used = new List<int>();
                Random rnd = new Random();
                int n = 0;
                while (n < 6)
                {
                    int i = rnd.Next(0, len);
                    if (used.Contains(i))
                        continue;
                    else
                    {
                        used.Add(i);
                        rec.Add(master[i]);
                        n++;
                    }
                }
                return rec;
            }
            else
                return master;
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

        private static int getNextFolder()
        {
            int n = 1;
            while (Directory.Exists(SettingsManager.profilesFolder + n + "/"))
                n++;
            return n;
        }
    }
}
