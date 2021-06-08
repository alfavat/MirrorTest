using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : MainController
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _tagService.GetList());
        }

        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(bool? active, DateTime? fromCreatedAt, DateTime? toCreatedAt, string query,
            int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1)
        {
            return GetResponse(_tagService.GetListByPaging(new TagPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                Active = active,
                FromCreatedAt = fromCreatedAt,
                ToCreatedAt = toCreatedAt
            }, out int total), total);
        }

        [HttpGet("searchbytagname")]
        public async Task<IActionResult> SearchByTagName(string tagName = null)
        {
            return GetResponse(await _tagService.SearchByTagName(tagName));
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int tagId)
        {
            return GetResponse(await _tagService.GetById(tagId));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(TagAddDto tagAddDto)
        {
            return GetResponse(await _tagService.Add(tagAddDto));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(TagUpdateDto tagUpdateDto)
        {
            return GetResponse(await _tagService.Update(tagUpdateDto));
        }

        [HttpPost("changeactivestatus")]
        public async Task<IActionResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto)
        {
            return GetResponse(await _tagService.ChangeActiveStatus(changeActiveStatusDto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int tagId)
        {
            return GetResponse(await _tagService.Delete(tagId));
        }
    }
}
