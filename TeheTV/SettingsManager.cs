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
        private static string appFolder = Environment.CurrentDirectory + "/appdata/";
        private static string pinFilename = appFolder + "pin.ini";
        public static string profilesFolder = appFolder + "profiles/";
        private static List<Profile> profiles = new List<Profile>();

        public static void initialize()
        {
            Directory.CreateDirectory(profilesFolder);
            loadProfiles();
        }

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
        public static bool setPin(string pin)
        {
            if (string.IsNullOrEmpty(pin) || string.IsNullOrWhiteSpace(pin))
                return false;

            string encryptedPin = encryptString(pin);

            File.WriteAllText(pinFilename, encryptedPin);
            return true;
        }

        /// <summary>
        /// returns True if given pin matches saved pin, false otherwise.
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public static bool doesPINmatch(string pin)
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
                if (p.getName().Equals(name))
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
                names.Add(p.getName());
            }
            return names;
        }

        /// <summary>
        /// Creates a new profile and adds it to the profile manager.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="year"></param>
        public static void createNewProfile(string name, int month, int day, int year)
        {
            Profile p = new Profile(name, month, day, year);
            p.saveProfile();
            profiles.Add(p);
        }

        /// <summary>
        /// Permonantly deletes a profile.
        /// </summary>
        /// <param name="p"></param>
        public static void deleteProfile(Profile p)
        {
            profiles.Remove(p);
        }

        private static string encryptString(string str)
        {
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

            List<string> profileFolders;
            try
            {
                profileFolders = Directory.GetDirectories(profilesFolder).ToList();
            }
            catch (UnauthorizedAccessException)
            {
                profileFolders = new List<string>();
            }

            foreach (string prfl in profileFolders)
            {
                string name = "";
                int month, day, year;
                month = day = year = 0;

                //MessageBox.Show(prfl);
                List<string> files = Directory.GetFiles(prfl + "/").ToList();
                if (files.Contains(prfl + info))
                {
                    string[] lines = File.ReadAllLines(prfl + info);
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
                        }
                        catch (FormatException)
                        {
                            continue;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(name) && month != 0 && day != 0 && year != 0)
                {
                    Profile p = new Profile(name, month, day, year);
                    profiles.Add(p);
                }
            }
        }
    }
}
