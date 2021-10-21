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
    public class NewsController : MainController
    {
        private readonly INewsService _newsService;
        private INewsPositionService _newsPositionService;

        public NewsController(INewsService newsService, INewsPositionService newsPositionService)
        {
            _newsPositionService = newsPositionService;
            _newsService = newsService;
        }

        [HttpGet("getlistforsitemap")]
        public async Task<IActionResult> GetListForSiteMap()
        {
            return GetResponse(await _newsService.GetListForSiteMap());
        }

        [HttpGet("gethistorybynewsid")]
        public async Task<IActionResult> GetHistoryByNewsId(int newsId)
        {
            return GetResponse(await _newsService.GetHistoryByNewsId(newsId));
        }

        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(DateTime? fromPublishedAt, DateTime? toPublishedAt, bool? approved, bool? active,
            string query, int? newsAgencyEntityId = null, int? newsTypeEntityId = null, bool? isDraft = null, int limit = 10,
            string orderBy = "Id", int page = 1, int ascending = 1, int? authorId = null, int? userId = null)
        {
            return GetResponse(_newsService.GetListByPaging(new NewsPagingDto()
            {
                Query = query,
                Limit = Math.Min(limit, 1000),
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                Active = active,
                FromPublishedAt = fromPublishedAt,
                ToPublishedAt = toPublishedAt,
                Approved = approved,
                IsDraft = isDraft,
                NewsAgencyEntityId = newsAgencyEntityId,
                NewsTypeEntityId = newsTypeEntityId,
                AuthorId = authorId,
                UserId = userId
            }, out int total), total);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int newsId)
        {
            return GetResponse(await _newsService.GetViewById(newsId));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NewsAddDto dto)
        {
            return GetResponse(await _newsService.Add(dto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return GetResponse(await _newsService.Delete(id));
        }

        [HttpPost("changeactivestatus")]
        public async Task<IActionResult> ChangeActiveStatus(ChangeActiveStatusDto dto)
        {
            return GetResponse(await _newsService.ChangeActiveStatus(dto));
        }


        [HttpPost("changeisdraftstatus")]
        public async Task<IActionResult> ChangeIsDraftStatus(ChangeIsDraftStatusDto dto)
        {
            return GetResponse(await _newsService.ChangeIsDraftStatus(dto));
        }

        [HttpPost("updatenewspositionorders")]
        public async Task<IActionResult> UpdateNewsPositionOrders(List<NewsPositionUpdateDto> newsPositions)
        {
            return GetResponse(await _newsPositionService.UpdateNewsPositionOrders(newsPositions));
        }

        [HttpGet("getnewspositionorders")]
        public async Task<IActionResult> GetOrdersByNewsPositionEntity(int newsPositionEntity, int limit = 15)
        {
            return GetResponse(await _newsPositionService.GetOrdersByNewsPositionEntityId(newsPositionEntity, limit));
        }

        [HttpPost("increasesharecount")]
        public async Task<IActionResult> IncreaseShareCount(int newsId)
        {
            return GetResponse(await _newsService.IncreaseShareCount(newsId));
        }
    }
}
