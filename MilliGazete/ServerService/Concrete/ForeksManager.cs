using Entity.Dtos;
using ServerService.Abstract;
using ServerService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static ServerService.Helper.HttpHelper;

namespace ServerService.Concrete
{
    public class ForeksManager : IForeksService
    {
        public void GetGoldGram() => GetData(AppSettingsHelper.ForeksGoldGramLink, "GAUTRY", "Altın", "");

        public void GetGoldQuarter() => GetData(AppSettingsHelper.ForeksGoldQuarterLink, "SGCEYREK", "Ç.Altın", "");

        public void GetDollar() => GetData(AppSettingsHelper.ForeksDollarLink, "USDTRY", "Dolar", "$");

        public void GetEuro() => GetData(AppSettingsHelper.ForeksEuroLink, "EURTRY", "Euro", "€");

        public void GetBIST() => GetData(AppSettingsHelper.ForeksBISTLink, "XU100", "BIST", "");

        private void GetData(string link, string code, string currencyName, string symbol)
        {
            link = string.Format(link, code, "Ask,DailyChangePercent,DateTime,Ticker,Last");
            var data = ForeksHelper.GetData<ForeksDto>(link);
            var list = new List<CurrencyAddDto>();
            if (data != null && data.Any())
            {
                data.ForEach(f =>
                {
                    list.Add(new CurrencyAddDto
                    {
                        CurrencyName = currencyName,
                        CurrencyValue = f.Ask == 0 ? f.Last : f.Ask,
                        DailyChangePercent = f.DailyChangePercent,
                        DailyChangeStatus = f.DailyChangePercent > 0,
                        LastUpdateDate = f.DateTime.UnixTimeStampToDateTime(),
                        ShortKey = f.Ticker,
                        Symbol = symbol
                    });
                });
            }
            AddArray(list);
        }

        private void AddArray(List<CurrencyAddDto> list)
        {
            try
            {
                using (HttpHelper http = new HttpHelper())
                {
                    var saveRequest = http.Request<dynamic>(new RequestObject()
                    {
                        Url = AppSettingsHelper.ApiLink + "currencies/addarray",
                        AuthorizationHeaderValue = "bearer " + AccessToken.Token,
                        Method = "POST",
                        Body = list
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetAllData()
        {
            GetGoldGram();
            Thread.Sleep(10 * 1000);
            GetDollar();
            Thread.Sleep(10 * 1000);
            GetEuro();
            Thread.Sleep(10 * 1000);
            GetBIST();
        }
    }
}
