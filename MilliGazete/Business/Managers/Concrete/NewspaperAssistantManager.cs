using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewspaperAssistantManager : INewspaperAssistantService
    {
        private readonly INewspaperDal _newspaperDal;
        private readonly IMapper _mapper;
        public NewspaperAssistantManager(INewspaperDal newspaperDal, IMapper mapper)
        {
            _newspaperDal = newspaperDal;
            _mapper = mapper;
        }

        public async Task<Newspaper> GetById(int id)
        {
            return await _newspaperDal.Get(p => p.Id == id);
        }

        public async Task Update(Newspaper item)
        {
            await _newspaperDal.Update(item);
        }

        public async Task Delete(Newspaper item)
        {
            await _newspaperDal.Delete(item);
        }

        public async Task Add(Newspaper item)
        {
            await _newspaperDal.Add(item);
        }

        public async Task<List<NewspaperDto>> GetTodayList()
        {
            var today = DateTime.Now;
            var list = _newspaperDal.GetList(t => t.Active &&
                                                t.CreatedAt.Year == today.Year &&
                                                t.CreatedAt.Month == today.Month &&
                                                t.CreatedAt.Day == today.Day
                )
                .Include(f => f.MainImageFile)
                .Include(f => f.ThumbnailFile).AsQueryable();
            var data = await _mapper.ProjectTo<NewspaperDto>(list).ToListAsync();
            return data.OrderBy(f => f.Name).ToList();
        }

        public async Task<NewspaperDto> GetViewById(int id)
        {
            var data = await _newspaperDal.GetList(p => !p.Active && p.Id == id)
                .Include(f => f.ThumbnailFile)
                .Include(f => f.MainImageFile).FirstOrDefaultAsync();
            return _mapper.Map<NewspaperDto>(data);
        }

        public async Task<NewspaperDto> GetViewByName(string name)
        {
            var data = await _newspaperDal.GetList(p => p.Active && p.Name == name &&
                                                    p.CreatedAt.Year == DateTime.Now.Year &&
                                                    p.CreatedAt.Month == DateTime.Now.Month &&
                                                    p.CreatedAt.Day == DateTime.Now.Day
                                                    )
                .Include(f => f.MainImageFile)
                .Include(f => f.ThumbnailFile)
                .FirstOrDefaultAsync();
            return _mapper.Map<NewspaperDto>(data);
        }
    }
}
