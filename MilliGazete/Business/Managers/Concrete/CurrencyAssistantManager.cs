using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class CurrencyAssistantManager : ICurrencyAssistantService
    {
        private readonly ICurrencyDal _currencyDal;
        private readonly IMapper _mapper;

        public CurrencyAssistantManager(ICurrencyDal currencyDal, IMapper mapper)
        {
            _currencyDal = currencyDal;
            _mapper = mapper;
        }

        public async Task Update(Currency data)
        {
            await _currencyDal.Update(data);
        }

        public async Task Add(Currency data)
        {
            await _currencyDal.Add(data);
        }

        public async Task<List<CurrencyDto>> GetList()
        {
            return await _mapper.ProjectTo<CurrencyDto>(_currencyDal.GetList()).ToListAsync();
        }

        public async Task<Currency> GetByShortKey(string shortKey)
        {
            return await _currencyDal.Get(f => f.ShortKey.ToLower().Trim() == shortKey.Trim().ToLower());
        }
    }
}
