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
    public class AdvertisementAssistantManager : IAdvertisementAssistantService
    {
        private readonly IAdvertisementDal _advertisementDal;
        private readonly IMapper _mapper;
        public AdvertisementAssistantManager(IAdvertisementDal advertisementDal, IMapper mapper)
        {
            _advertisementDal = advertisementDal;
            _mapper = mapper;
        }
        public async Task<List<AdvertisementDto>> GetActiveList()
        {
            var list = _advertisementDal.GetList(f => !f.Deleted && f.Active);
            return await _mapper.ProjectTo<AdvertisementDto>(list).ToListAsync();
        }

        public async Task<Advertisement> GetById(int id)
        {
            return await _advertisementDal.Get(p => p.Id == id && !p.Deleted);
        }

        public async Task Update(Advertisement advertisement)
        {
            await _advertisementDal.Update(advertisement);
        }

        public async Task Delete(Advertisement advertisement)
        {
            await _advertisementDal.Delete(advertisement);
        }

        public async Task Add(Advertisement advertisement)
        {
            await _advertisementDal.Add(advertisement);
        }

        public async Task<List<AdvertisementDto>> GetList()
        {
            var list = _advertisementDal.GetList(p => !p.Deleted);
            return await _mapper.ProjectTo<AdvertisementDto>(list).ToListAsync();
        }
    }
}
