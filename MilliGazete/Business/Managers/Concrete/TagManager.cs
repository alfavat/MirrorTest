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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class TagManager : ITagService
    {
        private readonly ITagAssistantService _tagAssistantService;
        private readonly IMapper _mapper;

        public TagManager(ITagAssistantService tagAssistantService, IMapper mapper)
        {
            _tagAssistantService = tagAssistantService;
            _mapper = mapper;
        }

        [SecuredOperation("TagGet")]
        [PerformanceAspect()]
        public IDataResult<List<TagDto>> GetListByPaging(TagPagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<TagDto>>(_tagAssistantService.GetListByPaging(pagingDto, out total));
        }

        [SecuredOperation("TagGet")]
        [PerformanceAspect()]
        public async Task<IDataResult<List<TagDto>>> SearchByTagName(string tagName)
        {
            return new SuccessDataResult<List<TagDto>>(await _tagAssistantService.SearchByTagName(tagName));
        }

        [SecuredOperation("TagGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<TagDto>>> GetList()
        {
            return new SuccessDataResult<List<TagDto>>(await _tagAssistantService.GetList());
        }

        [SecuredOperation("TagGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<Tag>> GetById(int tagId)
        {
            var tag = await _tagAssistantService.GetById(tagId);
            if (tag == null)
            {
                return new ErrorDataResult<Tag>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<Tag>(tag);
        }

        [SecuredOperation("TagUpdate")]
        [ValidationAspect(typeof(TagUpdateValidator))]
        [LogAspect()]
        [CacheRemoveAspect("ITagService.Get")]
        public async Task<IDataResult<int>> Update(TagUpdateDto tagUpdateDto)
        {
            var tag = await _tagAssistantService.GetById(tagUpdateDto.Id);
            if (tag == null)
            {
                return new ErrorDataResult<int>(0, Messages.RecordNotFound);
            }
            var tagExists = await _tagAssistantService.GetByTagNameOrUrl(tagUpdateDto.TagName, tagUpdateDto.Url);
            if (tagExists != null && tagExists.Id != tagUpdateDto.Id)
            {
                return new SuccessDataResult<int>(tagExists.Id, Messages.RecordAlreadyExists);
            }
            var cto = _mapper.Map(tagUpdateDto, tag);
            await _tagAssistantService.Update(cto);
            return new SuccessDataResult<int>(cto.Id, Messages.Updated);
        }

        [SecuredOperation("TagAdd")]
        [ValidationAspect(typeof(TagAddValidator))]
        [LogAspect()]
        [CacheRemoveAspect("ITagService.Get")]
        public async Task<IDataResult<int>> Add(TagAddDto tagAddDto)
        {
            var tagExists = await _tagAssistantService.GetByTagNameOrUrl(tagAddDto.TagName, tagAddDto.Url);
            if (tagExists != null)
            {
                return new SuccessDataResult<int>(tagExists.Id, Messages.RecordAlreadyExists);
            }
            var tag = _mapper.Map<Tag>(tagAddDto);
            tag.CreatedAt = DateTime.Now;
            await _tagAssistantService.Add(tag);
            return new SuccessDataResult<int>(tag.Id, Messages.Added);
        }

        [SecuredOperation("TagDelete")]
        [LogAspect()]
        [CacheRemoveAspect("ITagService.Get")]
        public async Task<IResult> Delete(int tagId)
        {
            var tag = await _tagAssistantService.GetById(tagId);
            if (tag == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            tag.Deleted = true;
            await _tagAssistantService.Update(tag);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("TagUpdate")]
        [LogAspect()]
        [CacheRemoveAspect("ITagService.Get")]
        public async Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto)
        {
            var tag = await _tagAssistantService.GetById(changeActiveStatusDto.Id);
            if (tag == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            tag.Active = changeActiveStatusDto.Active;
            await _tagAssistantService.Update(tag);
            return new SuccessResult(Messages.Updated);
        }
    }
}
