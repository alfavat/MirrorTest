using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : MainController
    {
        private IPageService _pageService;
        public PagesController(IPageService PageService)
        {
            _pageService = PageService;
        }


        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(DateTime? fromCreatedAt, DateTime? toCreatedAt,
            string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1)
        {
            return GetResponse(_pageService.GetListByPaging(new PagePagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                FromCreatedAt = fromCreatedAt,
                ToCreatedAt = toCreatedAt
            }, out int total), total);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _pageService.GetList());
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int pageId)
        {
            return GetResponse(await _pageService.GetById(pageId));
        }

        [HttpGet("getbyurl")]
        public async Task<IActionResult> GetByUrl(string url)
        {
            return GetResponse(await _pageService.GetByUrl(url));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(PageAddDto pageAddDto)
        {
            return GetResponse(await _pageService.Add(pageAddDto));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(PageUpdateDto pageUpdateDto)
        {
            return GetResponse(await _pageService.Update(pageUpdateDto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int pageId)
        {
            return GetResponse(await _pageService.Delete(pageId));
        }
    }
}