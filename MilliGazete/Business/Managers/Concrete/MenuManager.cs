using AutoMapper;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class MenuManager : IMenuService
    {
        private readonly IMenuAssistantService _menuAssistantService;
        private readonly IMapper _mapper;

        public MenuManager(IMenuAssistantService menuAssistantService, IMapper mapper)
        {
            _menuAssistantService = menuAssistantService;
            _mapper = mapper;
        }

        [PerformanceAspect()]
        [CacheAspect()]
        public async Task<IDataResult<List<MenuViewDto>>> GetActiveList()
        {
            return new SuccessDataResult<List<MenuViewDto>>(await _menuAssistantService.GetActiveList());
        }

        [SecuredOperation("MenuGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MenuViewDto>>> GetList()
        {
            return new SuccessDataResult<List<MenuViewDto>>(await _menuAssistantService.GetList());
        }

        [SecuredOperation("MenuGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<MenuViewDto>> GetById(int id)
        {
            var Menu = await _menuAssistantService.GetById(id);
            if (Menu == null)
            {
                return new ErrorDataResult<MenuViewDto>(Messages.RecordNotFound);
            }
            var data = _mapper.Map<MenuViewDto>(Menu);
            return new SuccessDataResult<MenuViewDto>(data);
        }

        [SecuredOperation("MenuUpdate")]
        [ValidationAspect(typeof(MenuUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IMenuService.Get")]
        public async Task<IResult> Update(MenuUpdateDto dto)
        {
            var data = await _menuAssistantService.GetById(dto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var cto = _mapper.Map(dto, data);
            await _menuAssistantService.Update(cto);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("MenuAdd")]
        [ValidationAspect(typeof(MenuAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IMenuService.Get")]
        public async Task<IResult> Add(MenuAddDto dto)
        {
            var data = _mapper.Map<Menu>(dto);
            await _menuAssistantService.Add(data);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("MenuDelete")]
        [LogAspect()]
        [CacheRemoveAspect("IMenuService.Get")]
        public async Task<IResult> Delete(int id)
        {
            var data = await _menuAssistantService.GetById(id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            data.Deleted = true;
            await _menuAssistantService.Update(data);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("MenuUpdate")]
        [LogAspect()]
        [CacheRemoveAspect("IMenuService.Get")]
        public async Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto)
        {
            var data = await _menuAssistantService.GetById(changeActiveStatusDto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            data.Active = changeActiveStatusDto.Active;
            await _menuAssistantService.Update(data);
            return new SuccessResult(Messages.Updated);
        }
    }
}
