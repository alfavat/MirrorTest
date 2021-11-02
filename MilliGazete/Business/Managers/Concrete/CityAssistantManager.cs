using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class CityAssistantManager : ICityAssistantService
    {
        private readonly ICityDal _cityDal;
        private readonly IMapper _mapper;
        public CityAssistantManager(ICityDal cityDal, IMapper mapper)
        {
            _cityDal = cityDal;
            _mapper = mapper;
        }
        public async Task<List<CityDto>> GetList()
        {
            var list = _cityDal.GetList(p => !p.Deleted).OrderBy(f => f.Name);
            return await _mapper.ProjectTo<CityDto>(list).ToListAsync();
        }
    }
}
