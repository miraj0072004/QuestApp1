
using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using QuestApp1.Models;

namespace QuestApp1.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string Email
        {
            get
            {
                return AppSettings.GetValueOrDefault("Email", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Email", value);
            }
        }

        public static string Password
        {
            get
            {
                return AppSettings.GetValueOrDefault("Password", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("Password", value);
            }
        }

        public static string AccessToken
        {
            get
            {
                return AppSettings.GetValueOrDefault("AccessToken", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("AccessToken", value);
            }
        }

        public static DateTime AccessTokenExpiration
        {
            get => AppSettings.GetValueOrDefault("AccessTokenExpiration", DateTime.UtcNow);
            set => AppSettings.AddOrUpdateValue("AccessTokenExpiration", value);
        }



        public static string LoggedInUserId
        {
            get
            {
                return AppSettings.GetValueOrDefault("UserId", ""); ;
            }
            set
            {
                AppSettings.AddOrUpdateValue("UserId", value);
            }
        }

        
    }
}
