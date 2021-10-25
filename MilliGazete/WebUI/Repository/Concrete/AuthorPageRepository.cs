using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class AuthorPageRepository : IAuthorPageRepository
    {
        public async Task<IDataResult<List<AuthorDto>>> GetAuthorList()
        {
            return await ApiHelper.GetApiAsync<List<AuthorDto>>("authors/getlist");
        }

        //public async Task<IDataResult<AuthorWithDetailsDto>> GetAuthorByName(string nameSurename)
        //{
        //    return await ApiHelper.GetApiAsync<AuthorWithDetailsDto>("authors/getbyname?nameSurename=" + nameSurename);
        //}
        public async Task<IDataResult<AuthorWithDetailsDto>> GetAuthorByUrl(string url)
        {
            return await ApiHelper.GetApiAsync<AuthorWithDetailsDto>("authors/getbyurl?url=" + url);
        }
    }
}
