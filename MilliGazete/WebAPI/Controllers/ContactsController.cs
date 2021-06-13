using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : MainController
    {
        private IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _contactService.GetList());
        }

        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(DateTime? fromCreatedAt, DateTime? toCreatedAt,
            int? cityId,int? districtId, string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1)
        {
            return GetResponse(_contactService.GetListByPaging(new ContactPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                FromCreatedAt = fromCreatedAt,
                ToCreatedAt = toCreatedAt,
            }, out int total), total);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int contactId)
        {
            return GetResponse(await _contactService.GetById(contactId));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(ContactAddDto contactAddDto)
        {
            return GetResponse(await _contactService.Add(contactAddDto));
        }
    }
}