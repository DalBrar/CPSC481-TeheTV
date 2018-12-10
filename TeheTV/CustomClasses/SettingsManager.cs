using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TeheTV
{
    class SettingsManager
    {
        public static string appFolder = Environment.CurrentDirectory + "/appdata/";
        private static string pinFilename = appFolder + "pin.ini";
        private static string starsRewardFile = appFolder + "global.reward.stars";
        private static string starsCostFile = appFolder + "global.cost.stars";
        public static string profilesFolder = appFolder + "profiles/";
        private static List<Profile> profiles = new List<Profile>();
        private static Profile currentProfile = null;

        public static void initialize()
        {
            Directory.CreateDirectory(profilesFolder);
            loadProfiles();
        }

        public static void AddWatched(Content c) { if (currentProfile != null) currentProfile.AddWatched(c); }
        public static void AddRecommended(Content c) { if (currentProfile != null) currentProfile.AddRecommendation(c); }

        public static int getStarRewardAmount()
        {
            try
            {
                string s = File.ReadAllText(starsRewardFile);
                int n = int.Parse(s);
                return n;
            }
            catch
            {
                return 2;
            }
        }
        public static int getStarCostAmount()
        {
            try
            {
                string s = File.ReadAllText(starsCostFile);
                int n = int.Parse(s);
                return n;
            }
            catch
            {
                return 3;
            }
        }

        public static void setStarRewardAmount(int n)  { File.WriteAllText(starsRewardFile, ""+n); }
        public static void setStarCostAmount(int n ) { File.WriteAllText(starsCostFile, "" + n); }

        /// <summary>
        /// Checks to see if pin file exists.
        /// </summary>
        /// <returns></returns>
        public static bool doesPINexist()
        {
            return File.Exists(pinFilename);
        }

        /// <summary>
        /// Will return True if pin successfully saved, false otherwise.
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public static bool setPin(int pin)
        {
            string encryptedPin = encryptString(pin);
            File.WriteAllText(pinFilename, encryptedPin);
            return true;
        }

        /// <summary>
        /// returns True if given pin matches saved pin, false otherwise.
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public static bool doesPINmatch(int pin)
        {
            if (!doesPINexist())
                return false;

            string inputPIN = encryptString(pin);
            string savedPIN = getPin();
            if (string.IsNullOrEmpty(savedPIN) || string.IsNullOrWhiteSpace(savedPIN))
                return false;
            return inputPIN.Equals(savedPIN);
        }

        /// <summary>
        /// Check if profile manager has any profiles loaded.
        /// </summary>
        /// <returns></returns>
        public static bool doProfilesExist()
        {
            return (profiles.Count > 0) ? true : false;
        }

        /// <summary>
        /// Get list of all loaded profiles.
        /// </summary>
        /// <returns>List of Profiles</returns>
        public static List<Profile> GetProfiles()
        {
            return profiles;
        }

        /// <summary>
        /// Get Profile by given name.
        /// Given name must match exactly (case sensitive).
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Profile if match, null otherwise.</returns>
        public static Profile GetProfileByName(string name)
        {
            foreach (Profile p in profiles)
            {
                if (p.Name.Equals(name))
                    return p;
            }
            return null;
        }

        /// <summary>
        /// get list of all Profile Names
        /// </summary>
        /// <returns>List of strings</returns>
        public static List<string> GetProfileNames()
        {
            List<string> names = new List<string>();
            foreach (Profile p in profiles)
            {
                names.Add(p.Name);
            }
            return names;
        }

        public static int getProfilesCount()
        {
            return profiles.Count;
        }

        /// <summary>
        /// Creates a new profile and adds it to the profile manager.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="year"></param>
        public static void createNewProfile(string name, int month, int day, int year, int time)
        {
            Profile p = new Profile(name, month, day, year, time);
            p.saveProfile();
            profiles.Add(p);
        }

        /// <summary>
        /// Permonantly deletes a profile.
        /// </summary>
        /// <param name="p"></param>
        public static void deleteProfile(Profile p)
        {
            if (currentProfile == p)
            {
                currentProfile = null;
                MainWindow.TvScreen = null;
            }
            p.DeleteProfile();
            profiles.Remove(p);
        }

        /// <summary>
        /// Set the Current Profile to be used by all navigation pages
        /// </summary>
        /// <param name="p"></param>
        public static void setCurrentProfile(Profile p)
        {
            currentProfile = p;
        }

        /// <summary>
        /// Get the current profile being used.
        /// </summary>
        /// <returns></returns>
        public static Profile getCurrentProfile()
        {
            return currentProfile;
        }

        private static string encryptString(int pin)
        {
            string str = "" + pin;
            string saltyStr = "teheTV" + str + "salt";

            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(saltyStr))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        private static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string getPin()
        {
            return File.ReadAllText(pinFilename);
        }

        private static void loadProfiles()
        {
            profiles = new List<Profile>();
            string info = "/info.profile";
            string watchFile = "/watchHistory.profile";
            string recmdFile = "/recommended.profile";

            List<string> profileFolders;
            try
            {
                profileFolders = Directory.GetDirectories(profilesFolder).ToList();
            }
            catch (UnauthorizedAccessException)
            {
                profileFolders = new List<string>();
            }
            
            foreach (string prfFolder in profileFolders)
            {
                string name = "";
                int month, day, year, time;
                month = day = year = time = 0;

                //MessageBox.Show(prfl);
                List<string> files = Directory.GetFiles(prfFolder + "/").ToList();
                if (files.Contains(prfFolder + info))
                {
                    string[] lines = File.ReadAllLines(prfFolder + info);
                    foreach (string line in lines)
                    {
                        if (line.Contains("name= "))
                            name = line.Replace("name= ", "");
                        try
                        {
                            if (line.Contains("mm= "))
                                month = int.Parse(line.Replace("mm= ", ""));
                            if (line.Contains("dd= "))
                                day = int.Parse(line.Replace("dd= ", ""));
                            if (line.Contains("yy= "))
                                year = int.Parse(line.Replace("yy= ", ""));
                            if (line.Contains("stars= "))
                                time = int.Parse(line.Replace("stars= ", ""));
                        }
                        catch (FormatException)
                        {
                            continue;
                        }
                    }
                }

                List<string> watchedList = null;
                string w = prfFolder + watchFile;
                if (files.Contains(w))
                {
                    watchedList = new List<string>();

                    string[] lines = File.ReadAllLines(w);
                    foreach (string line in lines)
                    {
                        watchedList.Add(line);
                    }
                }

                List<Content> recmdList = null;
                string r = prfFolder + recmdFile;
                if (files.Contains(r))
                {
                    recmdList = new List<Content>();

                    string[] lines = File.ReadAllLines(r);
                    foreach (string line in lines)
                    {
                        Content c = ContentManager.getContentFromFile(line);
                        if (c != null && !watchedList.Contains(c.toStringFilename()))
                            recmdList.Add(c);
                    }
                }

                if (!string.IsNullOrEmpty(name) && month != 0 && day != 0 && year != 0)
                {
                    string dirName = new DirectoryInfo(prfFolder).Name;
                    Profile p = new Profile(name, month, day, year, time, dirName);

                    if (watchedList != null)
                        p.WatchHistory = watchedList;
                    if (recmdList != null)
                        p.Recommendations = recmdList;

                    profiles.Add(p);
                }
            }
        }
    }
}
