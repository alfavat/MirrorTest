using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Repository.Concrete
{
    public class AuthorPageRepository : IAuthorPageRepository
    {
        public IDataResult<List<AuthorDto>> GetAuthorList()
        {
            return ApiHelper.GetApi<List<AuthorDto>>("authors/getlist");
        }

        public IDataResult<AuthorWithDetailsDto> GetAuthorByName(string nameSurename)
        {
            return ApiHelper.GetApi<AuthorWithDetailsDto>("authors/getbyname?nameSurename=" + nameSurename);
        }
    }
}
