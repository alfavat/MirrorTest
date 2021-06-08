using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class OptionAssistantManager : IOptionAssistantService
    {
        private readonly IOptionDal _optionDal;
        private readonly IMapper _mapper;

        public OptionAssistantManager(IOptionDal OptionDal, IMapper mapper)
        {
            _optionDal = OptionDal;
            _mapper = mapper;
        }

        public async Task<Option> Get()
        {
            return await _optionDal.Get();
        }

        public async Task<OptionDto> GetView()
        {
            return _mapper.Map<OptionDto>(await _optionDal.Get());
        }

        public async Task Update(Option option)
        {
            await _optionDal.Update(option);
        }
    }
}
