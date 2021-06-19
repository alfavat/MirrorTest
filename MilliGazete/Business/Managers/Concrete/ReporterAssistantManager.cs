using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class ReporterAssistantManager : IReporterAssistantService
    {
        private readonly IReporterDal _reporterDal;
        private readonly IMapper _mapper;
        public ReporterAssistantManager(IReporterDal ReporterDal, IMapper mapper)
        {
            _reporterDal = ReporterDal;
            _mapper = mapper;
        }

        public async Task<Reporter> GetById(int reporterId)
        {
            return await _reporterDal.Get(p => p.Id == reporterId && !p.Deleted);
        }

        public async Task<ReporterDto> GetViewById(int reporterId)
        {
            var data = _reporterDal.GetList(p => p.Id == reporterId && !p.Deleted).Include(f => f.ProfileImage);
            return await _mapper.ProjectTo<ReporterDto>(data).FirstOrDefaultAsync();
        }

        public async Task Update(Reporter reporter)
        {
            await _reporterDal.Update(reporter);
        }

        public async Task Delete(Reporter reporter)
        {
            reporter.Deleted = true;
            await _reporterDal.Update(reporter);
        }

        public async Task<Reporter> Add(Reporter reporter)
        {
            await _reporterDal.Add(reporter);
            return reporter;
        }

        public async Task<List<ReporterDto>> GetList()
        {
            var list = _reporterDal.GetList(p=> !p.Deleted).Include(f => f.ProfileImage);
            return await _mapper.ProjectTo<ReporterDto>(list).ToListAsync();
        }
    }
}
