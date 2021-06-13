using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class DistrictAssistantManager : IDistrictAssistantService
    {
        private readonly IDistrictDal _districtDal;
        private readonly IMapper _mapper;
        public DistrictAssistantManager(IDistrictDal districtDal, IMapper mapper)
        {
            _districtDal = districtDal;
            _mapper = mapper;
        }
        public async Task<List<DistrictDto>> GetListByCityId(int id)
        {
            var list = _districtDal.GetList(prop=>prop.CityId == id);
            return await _mapper.ProjectTo<DistrictDto>(list).ToListAsync();
        }
    }
}
