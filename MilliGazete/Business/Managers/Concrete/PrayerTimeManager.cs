using AutoMapper;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class PrayerTimeManager : IPrayerTimeService
    {
        private readonly IPrayerTimeAssistantService _prayerTimeAssistantService;
        private readonly IMapper _mapper;

        public PrayerTimeManager(IPrayerTimeAssistantService PrayerTimeAssistantService, IMapper mapper)
        {
            _prayerTimeAssistantService = PrayerTimeAssistantService;
            _mapper = mapper;
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<PrayerTimeDto>>> GetList()
        {
            return new SuccessDataResult<List<PrayerTimeDto>>(await _prayerTimeAssistantService.GetList());
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<PrayerTimeDto>>> GetPrayerTimeByCityId(int cityId)
        {
            return new SuccessDataResult<List<PrayerTimeDto>>(await _prayerTimeAssistantService.GetPrayerTimeByCityId(cityId));
        }

        [SecuredOperation("Service,Admin")]
        [PerformanceAspect()]
        [CacheRemoveAspect("IPrayerTimeService.Get")]
        public async Task<IResult> AddArray(List<PrayerTimeAddDto> prayerTimes)
        {
            if (!prayerTimes.HasValue())
            {
                return new ErrorResult(Messages.NoRecordFound);
            }

            var allPrayerTimes = await _prayerTimeAssistantService.GetList();

            foreach (var item in prayerTimes)
            {
                System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("tr-TR");
                DateTime dt = DateTime.Parse(item.PrayerDate, cultureinfo);
                var prayerTime = _mapper.Map<PrayerTime>(item);
                prayerTime.PrayerDate = dt;
                var prayerExists = allPrayerTimes.FirstOrDefault(u => u.CityId == prayerTime.CityId && u.PrayerDate.Date == prayerTime.PrayerDate.Date);
                if (prayerExists != null)
                {
                    prayerTime.Id = prayerExists.Id;
                    await _prayerTimeAssistantService.Update(prayerTime);
                }
                else await _prayerTimeAssistantService.Add(prayerTime);
            }
            return new SuccessResult(Messages.Added);
        }
    }
}
