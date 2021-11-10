using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class NewspaperRepository : INewspaperRepository
    {
        public async Task<IDataResult<List<NewspaperDto>>> GetList()
        {
            return await ApiHelper.GetApiAsync<List<NewspaperDto>>("Newspapers/gettodaylist");
        }

        public async Task<IDataResult<NewspaperDto>> GetMilliGazeteNewspaper()
        {
            return await ApiHelper.GetApiAsync<NewspaperDto>("Newspapers/getmilligazetenewspaper");
        }
    }
}
