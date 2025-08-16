using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Resources;
using System.Threading.Tasks;

namespace client_scheduler.Localization
{
    public static class LocalizationHelper
    {
        private static CultureInfo currentCulture = CultureInfo.CurrentCulture;

        public static void SetCulture(string cultureCode)
        {
            currentCulture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentCulture;
        }

        public static string GetString(string key)
        {
            return Resources.ResourceManager.GetString(key, currentCulture);
        }

        public static string GetString(string key, params object[] args)
        {
            string format = Resources.ResourceManager.GetString(key, currentCulture);
            return string.Format(format, args);
        }

        public static void InitializeFromSystemCulture()
        {
            currentCulture = CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = currentCulture;
            Thread.CurrentThread.CurrentUICulture = currentCulture;

            string region = GetUserRegion();
            //region = "ES"; // <- test to force region change based on location
            SetCulture(region);
        }

        public static string GetCurrentLanguage()
        {
            return currentCulture.TwoLetterISOLanguageName;
        }

        public static string GetUserRegion()
        {
            return currentCulture.Name.Contains("-") ? currentCulture.Name.Split('-')[1] : "US";
        }
    }
}
