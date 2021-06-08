using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class OperationClaimAssistantManager : IOperationClaimAssistantService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        private readonly IMapper _mapper;

        public OperationClaimAssistantManager(IOperationClaimDal OperationClaimDal, IMapper mapper)
        {
            _operationClaimDal = OperationClaimDal;
            _mapper = mapper;
        }

        public async Task<List<OperationClaimDto>> GetList()
        {
            var list = _operationClaimDal.GetList();
            return await _mapper.ProjectTo<OperationClaimDto>(list).ToListAsync();
        }
    }
}
