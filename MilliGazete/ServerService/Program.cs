using ServerService.Abstract;
using ServerService.Helper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerService
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiHelper.GetToken();
            LoadTimers();
            Console.ReadKey(true);
        }

        private static void LoadTimers()
        {
            if (AppSettingsHelper.AaIsActive)
            {
                var aaNewsTimer = new Timer(GetAaNews, null, 0, AppSettingsHelper.AaTimerSeconds);
            }
            if (AppSettingsHelper.DhaIsActive)
            {
                var dhaNewsTimer = new Timer(GetDhaNews, null, 0, AppSettingsHelper.DhaTimerSeconds);
            }
            if (AppSettingsHelper.IhaIsActive)
            {
                var ihaNewsTimer = new Timer(GetIhaNews, null, 0, AppSettingsHelper.IhaTimerSeconds);
            }
            if (AppSettingsHelper.ForeksIsActive)
            {
                var foreksTimer = new Timer(GetForeksData, null, 0, AppSettingsHelper.ForeksSeconds);
            }
            if (AppSettingsHelper.SiteMapIsActive)
            {
                var siteMapTimer = new Timer(GetSiteMapData, null, 0, AppSettingsHelper.SiteMapSeconds);
            }
            if (AppSettingsHelper.PrayerTimeIsActive)
            {
                var siteMapTimer = new Timer(GetPrayerTimes, null, 0, AppSettingsHelper.PrayerTimesSeconds);
            }
            Console.ReadLine();
        }

        private static void GetAaNews(object o) => ProcessMethod(() => InstanceFactory.GetInstance<IAaNewsService>().GetAaNews(), "GetAaNews");

        private static void GetDhaNews(object o) => ProcessMethod(() => InstanceFactory.GetInstance<IDhaNewsService>().GetDhaNews(), "GetDhaNews");

        private static void GetIhaNews(object o) => ProcessMethod(() => InstanceFactory.GetInstance<IIhaNewsService>().GetIhaNews(), "GetIhaNews");

        private static void GetForeksData(object o) => ProcessMethod(() => InstanceFactory.GetInstance<IForeksService>().GetAllData(), "GetForeksData");

        private static void GetSiteMapData(object o) => ProcessMethod(() => InstanceFactory.GetInstance<ISiteMapService>().CreateSiteMapXml(), "GetSiteMapData");

        private static void GetPrayerTimes(object o) => ProcessMethod(() => InstanceFactory.GetInstance<IPrayerTimeService>().GetLastPrayerTimeInfo(), "GetPrayerTimes");

        private static void ProcessMethod(Action action, string methodName)
        {
            Task.Run(() =>
            {
                try
                {
                    action();
                    Console.WriteLine(methodName + " Updated At : " + DateTime.Now.ToString());
                }
                catch (Exception ec)
                {
                    WriteErrorLog(methodName, ec);
                }
            });
        }

        private static void WriteErrorLog(string method, Exception ec)
        {
            Console.WriteLine(method + " ==> Error Message : " + ec.Message +
                (ec.InnerException != null ?
                (ec.InnerException.InnerException == null ? "" : ec.InnerException.InnerException.Message) +
                ec.InnerException.Message : "") + ", " + DateTime.Now.ToString()); ;
        }
    }
}
