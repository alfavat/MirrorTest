using Business.Managers.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsCommentLikesController : MainController
    {
        private INewsCommentLikeService _newsCommentLikeService;
        public NewsCommentLikesController(INewsCommentLikeService newsCommentLikeService)
        {
            _newsCommentLikeService = newsCommentLikeService;
        }

        [HttpPost("addordelete")]
        public async Task<IActionResult> AddOrDelete(int newsCommentId)
        {
            return GetResponse(await _newsCommentLikeService.AddOrDelete(newsCommentId));
        }
    }
}