using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : MainController
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _categoryService.GetList());
        }

        [HttpGet("getactivelist")]
        public async Task<IActionResult> GetActiveList()
        {
            return GetResponse(await _categoryService.GetActiveList());
        }

        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(DateTime? fromCreatedAt, DateTime? toCreatedAt, bool? isStatic, bool? active,
            int? parentCategoryId, string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1)
        {
            return GetResponse(_categoryService.GetListByPaging(new CategoryPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                Active = active,
                FromCreatedAt = fromCreatedAt,
                ToCreatedAt = toCreatedAt,
                IsStatic = isStatic,
                ParentCategoryId = parentCategoryId
            }, out int total), total);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int categoryId)
        {
            return GetResponse(await _categoryService.GetById(categoryId));
        }

        [HttpPost("changeactivestatus")]
        public async Task<IActionResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto)
        {
            return GetResponse(await _categoryService.ChangeActiveStatus(changeActiveStatusDto));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            return GetResponse(await _categoryService.Add(categoryAddDto));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            return GetResponse(await _categoryService.Update(categoryUpdateDto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            return GetResponse(await _categoryService.Delete(categoryId));
        }
    }
}