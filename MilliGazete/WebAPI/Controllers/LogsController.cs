using Business.Managers.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : MainController
    {
        private readonly ILogService _logService;
        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("getlistbypaging")]
        public IActionResult GetListByPaging(DateTime? fromCreatedAt, DateTime? toCreatedAt, string auditType, int? userId, string query, 
            int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1)
        {
            return GetResponse(_logService.GetListByPaging(new Entity.Dtos.LogPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                UserId = userId,
                FromCreateDate = fromCreatedAt,
                ToCreateDate = toCreatedAt,
                AuditType = auditType
            }, out int total), total);
        }
    }
}