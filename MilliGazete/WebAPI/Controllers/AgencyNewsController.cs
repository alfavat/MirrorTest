using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyNewsController : MainController
    {
        private readonly IAgencyNewsService _agencyNewsService;

        public AgencyNewsController(IAgencyNewsService agencyNewsService)
        {
            _agencyNewsService = agencyNewsService;
        }

        [HttpPost("addarray")]
        public async Task<IActionResult> AddArray(List<NewsAgencyAddDto> dto)
        {
            return GetResponse(await _agencyNewsService.AddArray(dto));
        }

        [HttpPost("copynewsfromagencynews")]
        public async Task<IActionResult> CopyNewsFromAgencyNews(AgencyNewsCopyDto dto)
        {
            return GetResponse(await _agencyNewsService.CopyNewsFromAgencyNews(dto));
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _agencyNewsService.GetList());
        }

        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(DateTime? fromPublishedAt, DateTime? toPublishedAt,
            string query, int? newsAgencyId = null, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1)
        {
            return GetResponse(_agencyNewsService.GetListByPaging(new NewsAgencyPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                FromPublishedAt = fromPublishedAt,
                ToPublishedAt = toPublishedAt,
                NewsAgencyId = newsAgencyId
            }, out int total), total);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int agencyNewsId)
        {
            return GetResponse(await _agencyNewsService.GetById(agencyNewsId));
        }

    }
}
