using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ICurrencyAssistantService
    {
        Task<List<CurrencyDto>> GetList();
        Task<Currency> GetByShortKey(string shortKey);
        Task Update(Currency data);
        Task Add(Currency data);
    }
}
