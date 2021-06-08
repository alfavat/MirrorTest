using Core.Utilities.Results;
using Entity.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;

namespace WebUI.Controllers
{
    public class NewsDetailsController : Controller
    {
        private readonly INewsDetailPageRepository _newsDetailPageRepository;
        private readonly INewsHitRepository _newsHitRepository;

        public NewsDetailsController(INewsDetailPageRepository newsDetailPageRepository,
            INewsHitRepository newsHitRepository)
        {
            _newsDetailPageRepository = newsDetailPageRepository;
            _newsHitRepository = newsHitRepository;
        }

        [Route("{categoryurl?}/{url?}/{id?}")]
        public async Task<IActionResult> Index(string categoryurl = "", string url = "", string id = "")
        {
            if (categoryurl.StringIsNullOrEmpty() || url.StringIsNullOrEmpty())
            {
                return Redirect("/index");
            }

            var result = await _newsDetailPageRepository.GetNewsWithDetails(url, Request.Cookies["Token"]);
            if (result.DataResultIsNotNull())
            {
                string referer = Request.Headers["Referer"].ToString();
                int newsHitEntityId = (int)NewsHitEntities.MainPageRedirect;
                if (referer.Contains("facebook") || referer.Contains("twitter"))
                {
                    newsHitEntityId = (int)NewsHitEntities.MainPageRedirect;
                }
                else if (referer.Contains("google") || referer.Contains("yandex"))
                {
                    newsHitEntityId = (int)NewsHitEntities.SearchEngineRedirc;
                }
                await _newsHitRepository.Add(result.Data.Id, newsHitEntityId);

                var commentResult = await _newsDetailPageRepository.GetNewsComments(result.Data.Id, 5, 1, Request.Cookies["Token"]);
                if (commentResult.DataResultIsNotNull())
                {
                    result.Data.CommentList = commentResult.Data.Data;
                    result.Data.CommentsCount = commentResult.Data.Count;
                }
                return View(result.Data);
            }
            return View("NotFound");//404 sayfasına yönlendirme olacak
        }


        [Route("newsdetails/loadmorenews")]
        public async Task<PartialViewResult> LoadMoreNewsAsync(string url = "", int page = 1)
        {
            var response = await _newsDetailPageRepository.GetNewsWithDetailsByPaging(url, "", 1, "id", page, 1, Request.Cookies["Token"]);
            if (response.DataResultIsNotNull() && response.Data.Data.HasValue())
            {
                var result = response.Data.Data.First();
                string referer = Request.Headers["Referer"].ToString();
                int newsHitEntityId = (int)NewsHitEntities.MainPageRedirect;
                if (referer.Contains("facebook") || referer.Contains("twitter"))
                {
                    newsHitEntityId = (int)NewsHitEntities.MainPageRedirect;
                }
                else if (referer.Contains("google") || referer.Contains("yandex"))
                {
                    newsHitEntityId = (int)NewsHitEntities.SearchEngineRedirc;
                }
                await _newsHitRepository.Add(result.Id, newsHitEntityId);

                var commentResult = await _newsDetailPageRepository.GetNewsComments(result.Id, 5, 1, Request.Cookies["Token"]);
                if (commentResult.DataResultIsNotNull())
                {
                    result.CommentList = commentResult.Data.Data;
                    result.CommentsCount = commentResult.Data.Count;
                }
                result.RelatedNewsCount = response.Data.Count;
                return PartialView("_NewsDetail", result);
            }
            return PartialView("_NewsDetail");
        }

        [Route("haber-detay-sayfa")]
        public async Task<PagingResult<List<NewsDetail>>> NewsDetailPagingAsync(string url = "", int page = 1)
        {
            var result = await _newsDetailPageRepository.GetNewsWithDetailsByPaging(url, "", 1, "id", page, 1, Request.Cookies["Token"]);
            if (result.DataResultIsNotNull())
            {
                var commentResult = await _newsDetailPageRepository.GetNewsComments(result.Data.Data[0].Id, 5, 1, Request.Cookies["Token"]);
                if (commentResult.DataResultIsNotNull())
                {
                    result.Data.Data[0].CommentList = commentResult.Data.Data;
                }
            }
            return result;
        }

        [Route("043fe94c-710d-4287-a337-b7c2a0d2857e/{id?}")]
        public async Task<IActionResult> Index(string id = "")
        {
            if (id.StringIsNullOrEmpty())
            {
                return Redirect("/index");
            }

            var result = await _newsDetailPageRepository.GetNewsWithDetailsById(id, true, Request.Cookies["Token"]);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            return View("NotFound");//404 sayfasına yönlendirme olacak
        }

        [Route("newsdetails/newscomments")]
        public async Task<PartialViewResult> NewsCommentsAsync(int newsId, int page)
        {
            var comments = await _newsDetailPageRepository.GetNewsComments(newsId, 5, page, Request.Cookies["Token"]);
            if (comments.DataResultIsNotNull())
            {
                return PartialView("_NewsComment", comments.Data.Data);
            }
            return PartialView("_NewsComment");
        }

        [HttpPost("newsdetails/addordeletecommentlike")]
        public async Task<IResult> AddOrUpdateCommentLikeAsync(int newsCommentId)
        {
            return await _newsDetailPageRepository.AddOrDeleteCommentLike(newsCommentId, Request.Cookies["Token"]);
        }
    }
}
