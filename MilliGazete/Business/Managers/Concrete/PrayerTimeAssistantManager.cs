using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class PrayerTimeAssistantManager : IPrayerTimeAssistantService
    {
        private readonly IPrayerTimeDal _prayerTimeDal;
        private readonly IMapper _mapper;
        public PrayerTimeAssistantManager(IPrayerTimeDal PrayerTimeDal, IMapper mapper)
        {
            _prayerTimeDal = PrayerTimeDal;
            _mapper = mapper;
        }

        public async Task Add(PrayerTime item)
        {
            await _prayerTimeDal.Add(item);
        }

        public async Task Update(PrayerTime item)
        {
            await _prayerTimeDal.Update(item);
        }

        public async Task<List<PrayerTimeDto>> GetList()
        {
            var list = _prayerTimeDal.GetList();
            return await _mapper.ProjectTo<PrayerTimeDto>(list).ToListAsync();
        }

        public async Task<List<PrayerTimeDto>> GetPrayerTimeByCityId(int cityId)
        {
            var list = _prayerTimeDal.GetList(f => f.CityId == cityId && f.PrayerDate.Date == System.DateTime.Now.Date);
            return await _mapper.ProjectTo<PrayerTimeDto>(list).ToListAsync();
        }
    }
}
