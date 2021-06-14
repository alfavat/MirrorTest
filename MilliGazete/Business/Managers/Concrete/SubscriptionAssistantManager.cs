using AutoMapper;
using Business.Managers.Abstract;
using Core.Utilities.Helper.Abstract;
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
    public class SubscriptionAssistantManager : ISubscriptionAssistantService
    {
        private readonly ISubscriptionDal _subscriptionDal;
        private readonly IMailHelper _mailHelper;
        private readonly IOptionAssistantService _optionAssistantService;
        private readonly IMapper _mapper;
        public SubscriptionAssistantManager(ISubscriptionDal subscriptionDal, IMailHelper mailHelper, IOptionAssistantService optionAssistantService, IMapper mapper)
        {
            _subscriptionDal = subscriptionDal;
            _mailHelper = mailHelper;
            _mapper = mapper;
            _optionAssistantService = optionAssistantService;
        }

        public List<SubscriptionDto> GetListByPaging(SubscriptionPagingDto pagingDto, out int total)
        {
            var list = _subscriptionDal.GetList().Include(f=>f.City).Include(f=>f.District);
            var query = _mapper.ProjectTo<SubscriptionDto>(list);

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.FullName.Contains(pagingDto.Query) || f.Email.Contains(pagingDto.Query) ||
                f.Address.Contains(pagingDto.Query) || f.Phone.Contains(pagingDto.Query) ||
                f.Description.Contains(pagingDto.Query));

            if (pagingDto.CityId.HasValue)
                query = query.Where(f => f.CityId == pagingDto.CityId.Value);

            if (pagingDto.DistrictId.HasValue)
                query = query.Where(f => f.DistrictId == pagingDto.DistrictId.Value);

            if (pagingDto.FromCreatedAt.HasValue && pagingDto.ToCreatedAt.HasValue)
                query = query.Where(f => f.CreatedAt >= pagingDto.FromCreatedAt.Value && f.CreatedAt <= pagingDto.ToCreatedAt.Value);

            total = query.Count();
            var data = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            return data.ToList();
        }

        public async Task<Subscription> GetById(int subscriptionId)
        {
            return await _subscriptionDal.Get(p => p.Id == subscriptionId);
        }

        public async Task Add(Subscription subscription)
        {
            await _subscriptionDal.Add(subscription);
            OptionDto option = await _optionAssistantService.GetView();
            Task.Run(() => SendEmail(subscription, option.Email));
        }

        private bool SendEmail(Subscription subscription, string toEmail)
        {
            return _mailHelper.SendEmail(Translator.GetByKey("subscriptionEmailSubject"),subscription.Email + "<br/>" + subscription.Description, new string[] { toEmail });
        }

        public async Task<List<SubscriptionDto>> GetList()
        {
            var list = _subscriptionDal.GetList().Include(f=>f.District);
            return await _mapper.ProjectTo<SubscriptionDto>(list).ToListAsync();
        }

        public async Task<SubscriptionDto> GetViewById(int subscriptionId)
        {
            var data = await _subscriptionDal.GetList(p => p.Id == subscriptionId).Include(f => f.City).Include(f=>f.District).FirstOrDefaultAsync();
            if (data != null)
            {
                return _mapper.Map<SubscriptionDto>(data);
            }
            return null;
        }
    }
}
