using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
namespace Business.Managers.Concrete
{
    public class LogAssistantManager : ILogAssistantService
    {
        private readonly IMapper _mapper;
        private readonly ILogDal _logDal;
        public LogAssistantManager(ILogDal logDal, IMapper mapper)
        {
            _logDal = logDal;
            _mapper = mapper;
        }

        public List<LogDto> GetListByPaging(LogPagingDto pagingDto, out int total)
        {
            var query = _logDal.GetList();

            if (pagingDto.UserId.HasValue)
                query = query.Where(f => f.UserId == pagingDto.UserId);

            if (pagingDto.FromCreateDate.HasValue && pagingDto.ToCreateDate.HasValue)
                query = query.Where(f => f.CreatedAt >= pagingDto.FromCreateDate && f.CreatedAt <= pagingDto.ToCreateDate);

            if (pagingDto.AuditType.StringNotNullOrEmpty())
                query = query.Where(f => f.Audit == pagingDto.AuditType);

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.MethodName.Contains(pagingDto.Query) || f.ClassName.Contains(pagingDto.Query));

            total = query.Count();
            var data = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            return _mapper.ProjectTo<LogDto>(data).ToList();
        }
    }
}
