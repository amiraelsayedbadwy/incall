using incalltask.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Helper
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
        private const string FirstLoginKey = "login";
        private const string SettingsKey = "settings_key";
        private const string UserNameKey = "userName";
        private const string DisplayNameKey = "displayName";
        private const string AuthNameKey = "authName";
        private const string PasswordKey = "password";
        private const string UserDomainKey = "userDomain";
        private const string SipServerKey = "sipserver";
        private const string SipServerPortKey = "sipserverport";
        private const string StunServerKey = "stunServer";
        private const string StunServerPortKey = "stunServerPort";
        private const string SipServerTypeKey = "SipServerType";
        private const string SRTPKey = "SRTPPolicy";
        private const string DefaultTransportKey = "defaulttransport";
        private const string UserStatuseKey = "userstatus";
        private const string ExtensionKey= "Extension";
        private static readonly string SettingsDefault = string.Empty;
        private static readonly int SipServerPOrtDefault = 5060;
        private static readonly int STUNServerPortDefault = 3478;
        private static readonly bool FirstLoginDefault = true;
        #endregion


        public static string Extension
        {
            get => AppSettings.GetValueOrDefault(ExtensionKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(ExtensionKey, value);
        }
        public static string UserStatus
        {
            get => AppSettings.GetValueOrDefault(UserStatuseKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(UserStatuseKey, value);
        }
        public static int SipServerPort
        {
            get => AppSettings.GetValueOrDefault(SipServerPortKey, SipServerPOrtDefault);
            set => AppSettings.AddOrUpdateValue(SipServerPortKey, value);
        }
        public static string UserName
        {
            get => AppSettings.GetValueOrDefault(UserNameKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(UserNameKey, value);
        }

        public static string DisplayName
        {
            get => AppSettings.GetValueOrDefault(DisplayNameKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(DisplayNameKey, value);
        }

        public static string AuthName
        {
            get => AppSettings.GetValueOrDefault(AuthNameKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(AuthNameKey, value);
        }

        public static string Password
        {
            get => AppSettings.GetValueOrDefault(PasswordKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(PasswordKey, value);
        }

        public static string UserDomain
        {
            get => AppSettings.GetValueOrDefault(UserDomainKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(UserDomainKey, value);
        }
        public static string SipServer
        {
            get => AppSettings.GetValueOrDefault(SipServerKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SipServerKey, value);
        }
        public static int STUNServerPort
        {
            get => AppSettings.GetValueOrDefault(StunServerPortKey, STUNServerPortDefault);
            set => AppSettings.AddOrUpdateValue(StunServerPortKey, value);
        }
        public static string STUNServer
        {
            get => AppSettings.GetValueOrDefault(StunServerKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(StunServerKey, value);
        }
        public static string SipServerType
        {
            get => AppSettings.GetValueOrDefault(SipServerTypeKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SipServerTypeKey, value);
        }
       
        public static string SRTPPolicy
        {
            get => AppSettings.GetValueOrDefault(SRTPKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SRTPKey, value);
        }
        public static string DefaultTransport
        {
            get => AppSettings.GetValueOrDefault(DefaultTransportKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(DefaultTransportKey, value);
        }
        public static bool FirstLogin
        {
            get => AppSettings.GetValueOrDefault(FirstLoginKey,FirstLoginDefault );
            set => AppSettings.AddOrUpdateValue(FirstLoginKey, value);
        }

    }
}
