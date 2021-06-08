using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsCommentsController : MainController
    {
        private INewsCommentService _newsCommentService;
        public NewsCommentsController(INewsCommentService newsCommentService)
        {
            _newsCommentService = newsCommentService;
        }

        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(DateTime? fromCreatedAt, DateTime? toCreatedAt,
            string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1, bool? approved = null, int? newsId = null, int? userId = null)
        {
            return GetResponse(_newsCommentService.GetListByPaging(new NewsCommentPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                FromCreatedAt = fromCreatedAt,
                ToCreatedAt = toCreatedAt,
                Approved = approved,
                NewsId = newsId,
                UserId = userId
            }, out int total), total);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int newsCommentId)
        {
            return GetResponse(await _newsCommentService.GetById(newsCommentId));
        }

        [HttpGet("getbynewsid")]
        public IActionResult GetByNewsId(int newsId, int limit = 10, int page = 1)
        {
            return GetResponse(_newsCommentService.GetByNewsId(newsId, limit, page, out int total), total);
        }

        [HttpGet("getusercommentlistbypaging")]
        public IActionResult GetUserCommentListByPaging(DateTime? fromCreatedAt, DateTime? toCreatedAt,
            string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1, bool? approved = null, int? newsId = null)
        {
            return GetResponse(_newsCommentService.GetUserCommentListByPaging(new NewsCommentPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                FromCreatedAt = fromCreatedAt,
                ToCreatedAt = toCreatedAt,
                Approved = approved,
                NewsId = newsId
            }, out int total), total);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(NewsCommentAddDto newsCommentAddDto)
        {
            return GetResponse(await _newsCommentService.Add(newsCommentAddDto));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(NewsCommentUpdateDto newsCommentUpdateDto)
        {
            return GetResponse(await _newsCommentService.Update(newsCommentUpdateDto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int newsCommentId)
        {
            return GetResponse(await _newsCommentService.Delete(newsCommentId));
        }

        [HttpPost("deleteusercommentbyid")]
        public async Task<IActionResult> DeleteUserCommentById(int newsCommentId)
        {
            return GetResponse(await _newsCommentService.DeleteUserCommentById(newsCommentId));
        }

        [HttpPost("changeapprovedstatus")]
        public async Task<IActionResult> ChangeApprovedStatus(ChangeApprovedStatusDto dto)
        {
            return GetResponse(await _newsCommentService.ChangeApprovedStatus(dto));
        }
    }
}