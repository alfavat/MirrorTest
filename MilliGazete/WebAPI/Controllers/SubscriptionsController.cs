using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : MainController
    {
        private ISubscriptionService _subscriptionService;
        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _subscriptionService.GetList());
        }

        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(DateTime? fromCreatedAt, DateTime? toCreatedAt,
            int? cityId,int? districtId, string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1)
        {
            return GetResponse(_subscriptionService.GetListByPaging(new SubscriptionPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                CityId = cityId,
                DistrictId = districtId,
                FromCreatedAt = fromCreatedAt,
                ToCreatedAt = toCreatedAt,
            }, out int total), total);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int subscriptionId)
        {
            return GetResponse(await _subscriptionService.GetById(subscriptionId));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(SubscriptionAddDto subscriptionAddDto)
        {
            return GetResponse(await _subscriptionService.Add(subscriptionAddDto));
        }
    }
}