using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ServerService.Helper
{
    public class AppSettingsHelper
    {
        private static IConfigurationRoot configuration =
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json").Build();

        #region api
        public static string ApiLink { get => configuration.GetSection("ApiLink").Value; }
        public static string ApiUserName { get => configuration.GetSection("ApiUserName").Value; }
        public static string ApiPassword { get => configuration.GetSection("ApiPassword").Value; }
        #endregion

        #region ihaNews
        public static string IhaUserName { get => configuration.GetSection("IhaInfo:UserName").Value; }
        public static string IhaUserCode { get => configuration.GetSection("IhaInfo:UserCode").Value; }
        public static string IhaPassword { get => configuration.GetSection("IhaInfo:Password").Value; }
        public static string IhaStandardLink { get => configuration.GetSection("IhaInfo:StandardLink").Value; }
        public static string IhaInternetLink { get => configuration.GetSection("IhaInfo:InternetLink").Value; }
        public static int IhaTimerSeconds { get => configuration.GetSection("IhaInfo:TimerSeconds").Value.ToSeconds(); }
        public static bool IhaIsActive { get => configuration.GetSection("IhaInfo:Active").Value.ToBoolean(); }
        #endregion

        #region dhaNews
        public static string DhaUserName { get => configuration.GetSection("DhaInfo:UserName").Value; }
        public static string DhaUserCode { get => configuration.GetSection("DhaInfo:UserCode").Value; }
        public static string DhaPassword { get => configuration.GetSection("DhaInfo:Password").Value; }
        public static string DhaStandardLink { get => configuration.GetSection("DhaInfo:StandardLink").Value; }
        public static int DhaTimerSeconds { get => configuration.GetSection("DhaInfo:TimerSeconds").Value.ToSeconds(); }
        public static bool DhaIsActive { get => configuration.GetSection("DhaInfo:Active").Value.ToBoolean(); }
        #endregion

        #region aaNews
        public static string AaUserName { get => configuration.GetSection("AaInfo:UserName").Value; }
        public static string AaUserCode { get => configuration.GetSection("AaInfo:UserCode").Value; }
        public static string AaPassword { get => configuration.GetSection("AaInfo:Password").Value; }
        public static string AaStandardLink { get => configuration.GetSection("AaInfo:StandardLink").Value; }
        public static string AaTextLink { get => configuration.GetSection("AaInfo:TextLink").Value; }
        public static string AaPictureLink { get => configuration.GetSection("AaInfo:PictureLink").Value; }
        public static string AaVideoLink { get => configuration.GetSection("AaInfo:VideoLink").Value; }
        public static int AaTimerSeconds { get => configuration.GetSection("AaInfo:TimerSeconds").Value.ToSeconds(); }
        public static bool AaIsActive { get => configuration.GetSection("AaInfo:Active").Value.ToBoolean(); }

        public static string AaFilterCategory { get => configuration.GetSection("AaInfo:FilterCategory").Value; }
        public static string AaFilterPriority { get => configuration.GetSection("AaInfo:FilterPriority").Value; }
        public static string AaFilterPackage { get => configuration.GetSection("AaInfo:FilterPackage").Value; }
        public static string AaFilterType { get => configuration.GetSection("AaInfo:FilterType").Value; }
        public static string AaFilterLanguage { get => configuration.GetSection("AaInfo:FilterLanguage").Value; }
        public static string AaFilterLimit { get => configuration.GetSection("AaInfo:FilterLimit").Value; }
        #endregion

        #region foreks
        public static string ForeksGoldGramLink { get => configuration.GetSection("Foreks:GoldGramLink").Value; }
        public static string ForeksAuthorizationToken { get => configuration.GetSection("Foreks:AuthorizationToken").Value; }
        public static int ForeksSeconds { get => configuration.GetSection("Foreks:TimerSeconds").Value.ToSeconds(); }
        public static string ForeksGoldQuarterLink { get => configuration.GetSection("Foreks:GoldQuarterLink").Value; }
        public static string ForeksDollarLink { get => configuration.GetSection("Foreks:DollarLink").Value; }
        public static string ForeksEuroLink { get => configuration.GetSection("Foreks:EuroLink").Value; }
        public static string ForeksBISTLink { get => configuration.GetSection("Foreks:BISTLink").Value; }
        public static bool ForeksIsActive { get => configuration.GetSection("Foreks:Active").Value.ToBoolean(); }
        #endregion

        #region sitemap
        public static string MainUrl { get => configuration.GetSection("XmlInfo:MainUrl").Value; }
        public static string MobileMainUrl { get => configuration.GetSection("XmlInfo:MobileMainUrl").Value; }
        public static string WebUIMapBasePath { get => configuration.GetSection("XmlInfo:WebUIBasePath").Value; }
        public static string MobileUIBasePath { get => configuration.GetSection("XmlInfo:MobileUIBasePath").Value; }
        public static string XmlNamespace { get => configuration.GetSection("XmlInfo:XmlNamespace").Value; }
        public static string Xsi { get => configuration.GetSection("XmlInfo:Xsi").Value; }
        public static string SchemeLocation { get => configuration.GetSection("XmlInfo:SchemeLocation").Value; }
        public static int SiteMapSeconds { get => configuration.GetSection("XmlInfo:TimerSeconds").Value.ToSeconds(); }
        public static bool SiteMapIsActive { get => configuration.GetSection("XmlInfo:Active").Value.ToBoolean(); }
        #endregion

        #region prayer times
        public static string PrayerTimeLink { get => configuration.GetSection("PrayerTimeLink").Value; }
        public static int PrayerTimesSeconds { get => configuration.GetSection("PrayerTimesSeconds").Value.ToSeconds(); }
        public static bool PrayerTimeIsActive { get => configuration.GetSection("PrayerTimeIsActive").Value.ToBoolean(); }
        #endregion

    }
}
