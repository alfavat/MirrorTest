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
    public class ArticlesController : MainController
    {
        private INewsService _newsService;
        private readonly INewsPositionService _newsPositionService;

        public ArticlesController(INewsService newsService, INewsPositionService newsPositionService)
        {
            _newsService = newsService;
            _newsPositionService = newsPositionService;
        }

        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(DateTime? fromCreatedAt, DateTime? toCreatedAt,
            string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1, bool? approved = null, int? authorId = null, int? userId = null)
        {
            return GetResponse(_newsService.GetListByPaging(new NewsPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                FromCreatedAt = fromCreatedAt,
                ToCreatedAt = toCreatedAt,
                Approved = approved,
                NewsTypeEntityId = (int)Entity.Enums.NewsTypeEntities.Article,
                AuthorId = authorId,
                UserId = userId
            }, out int total), total);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int articleId)
        {
            return GetResponse(await _newsService.GetViewById(articleId));
        }

        [HttpGet("getbyurl")]
        public async Task<IActionResult> GetByUrl(string url)
        {
            return GetResponse(await _newsService.GetViewByUrl(url));
        }


        [HttpGet("getlastweekmostviewed")]
        public async Task<IActionResult> GetLastWeekMostViewedArticles(int limit = 10)
        {
            return GetResponse(await _newsService.GetLastWeekMostViewedArticles(limit));
        }

        [HttpGet("getbyauthorid")]
        public async Task<IActionResult> GetListByAuthorId(int authorId)
        {
            return GetResponse(await _newsService.GetListByAuthorId(authorId));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NewsAddDto dto)
        {
            return GetResponse(await _newsService.Add(dto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int articleId)
        {
            return GetResponse(await _newsService.Delete(articleId));
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
        public async Task<IActionResult> IncreaseShareCount(int id)
        {
            return GetResponse(await _newsService.IncreaseShareCount(id));
        }
    }
}