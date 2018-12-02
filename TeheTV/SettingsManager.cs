using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TeheTV
{
    class SettingsManager
    {
        private static string appFolder = "appdata/";
        private static string pinFilename = appFolder + "pin.ini";
        public static string profilesFolder = appFolder + "profiles/";
        private static List<Profile> profiles = new List<Profile>();

        public static void initialize()
        {
            Directory.CreateDirectory(profilesFolder);
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
        /// Creates a new profile and adds it to the profile manager.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="year"></param>
        public static void createNewProfile(string name, int month, int day, int year)
        {
            profiles.Add(new Profile(name, month, day, year));
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
    }
}
