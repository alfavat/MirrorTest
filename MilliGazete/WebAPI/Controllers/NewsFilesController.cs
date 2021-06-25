using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsFilesController : MainController
    {
        private INewsFileService _newsFileService;
        public NewsFilesController(INewsFileService newsFileService)
        {
            _newsFileService = newsFileService;
        }

        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1, int newsFileTypeEntityId = 0)
        {
            return GetResponse(_newsFileService.GetListByPaging(new NewsFilePagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                NewsFileTypeEntityId = newsFileTypeEntityId
            }, out int total), total);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int newsFileId)
        {
            return GetResponse(await _newsFileService.GetById(newsFileId));
        }
    }
}