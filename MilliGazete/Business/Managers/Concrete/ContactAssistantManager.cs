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
    public class ContactAssistantManager : IContactAssistantService
    {
        private readonly IContactDal _contactDal;
        private readonly IMailHelper _mailHelper;
        private readonly IMapper _mapper;
        public ContactAssistantManager(IContactDal contactDal, IMailHelper mailHelper, IMapper mapper)
        {
            _contactDal = contactDal;
            _mailHelper = mailHelper;
            _mapper = mapper;
        }

        public List<ContactDto> GetListByPaging(ContactPagingDto pagingDto, out int total)
        {
            var list = _contactDal.GetList();
            var query = _mapper.ProjectTo<ContactDto>(list);

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.FullName.Contains(pagingDto.Query) || f.Email.Contains(pagingDto.Query) ||
                f.Message.Contains(pagingDto.Query) || f.Phone.Contains(pagingDto.Query));

            if (pagingDto.FromCreatedAt.HasValue && pagingDto.ToCreatedAt.HasValue)
                query = query.Where(f => f.CreatedAt >= pagingDto.FromCreatedAt.Value && f.CreatedAt <= pagingDto.ToCreatedAt.Value);

            total = query.Count();
            var data = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            return data.ToList();
        }

        public async Task<Contact> GetById(int contactId)
        {
            return await _contactDal.Get(p => p.Id == contactId);
        }

        public async Task Add(Contact contact)
        {
            await _contactDal.Add(contact);
            Task.Run(() => SendEmail(contact));
        }

        private bool SendEmail(Contact contact)
        {
            return _mailHelper.SendEmail(Translator.GetByKey("contactEmailSubject"), Translator.GetByKey("contactEmailBody"), new string[] { contact.Email });
        }

        public async Task<List<ContactDto>> GetList()
        {
            var list = _contactDal.GetList();
            return await _mapper.ProjectTo<ContactDto>(list).ToListAsync();
        }

        public async Task<ContactDto> GetViewById(int contactId)
        {
            var data = await _contactDal.GetList(p => p.Id == contactId).FirstOrDefaultAsync();
            if (data != null)
            {
                return _mapper.Map<ContactDto>(data);
            }
            return null;
        }
    }
}
