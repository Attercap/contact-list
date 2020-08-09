using System;
using System.Text;

namespace ContactList.Business
{
    /// <summary>
    /// General string utility methods
    /// Currently only date display and simple crypto methods, but could be expanded with number formats, general manipulators, etc
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// Returns string of date for ease of presentation across systems
        /// </summary>
        /// <param name="baseDate">date to be displayed, UTC date expected</param>
        /// <param name="includeTime">bool if time should be included in date display</param>
        /// <param name="utcOffset">user's minute offset of UTC date</param>
        /// <returns>string of formatted date to user's time zone</returns>
        public static string DisplayDate(DateTime baseDate, bool includeTime, int utcOffset)
        {
            return DisplayDate(baseDate.AddMinutes(utcOffset), includeTime);
        }

        /// <summary>
        /// Returns string of date for ease of presentation across systems
        /// </summary>
        /// <param name="baseDate">date to be displayed</param>
        /// <param name="includeTime">bool if time should be included in date display</param>
        /// <returns>string of formatted date</returns>
        public static string DisplayDate(DateTime baseDate, bool includeTime)
        {
            string myReturn = string.Empty;

            if (includeTime)
            {
                myReturn = string.Format("{0:MM/dd/yyyy hh:mm tt}", baseDate);
            }
            else
            {
                myReturn = string.Format("{0:MM/dd/yyyy}", baseDate);
            }

            return myReturn;
        }

        /// <summary>
        /// Changes string into hash version of string
        /// Simple cryptography; would normally create separate Cryptography class or object to allow encrypt/decrypt
        /// </summary>
        /// <param name="password">string to be hashed</param>
        /// <returns>SHA256 ASCII string</returns>
        public static string HashPassword(string password)
        {
            byte[] data = Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }
    }
}
