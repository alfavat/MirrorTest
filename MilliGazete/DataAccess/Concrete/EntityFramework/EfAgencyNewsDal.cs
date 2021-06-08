﻿using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAgencyNewsDal : EfEntityRepositoryBase<AgencyNews>, IAgencyNewsDal
    {
        public EfAgencyNewsDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }

        public async Task AddWithFiles(AgencyNews agencyNews, List<AgencyNewsFile> files)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    Db.AgencyNews.Add(agencyNews);
                    Db.SaveChanges();
                    files.ForEach(f => f.AgencyNewsId = agencyNews.Id);
                    Db.AgencyNewsFile.AddRange(files);
                    Db.SaveChanges();
                    await transaction.CommitAsync();
                }

                catch (System.Exception ec)
                {
                    await transaction.RollbackAsync();
                    throw ec;
                }
            }
        }
        public async Task DeleteAllByAgencyId(int newsAgencyEntityId)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    var data = Db.AgencyNews.Where(f => f.NewsAgencyEntityId == newsAgencyEntityId).Include(f => f.AgencyNewsFile).ToList();
                    data.ForEach(news =>
                    {
                        if (news.AgencyNewsFile != null && news.AgencyNewsFile.Any())
                        {
                            Db.AgencyNewsFile.RemoveRange(news.AgencyNewsFile.ToList());
                        }
                    });
                    Db.AgencyNews.RemoveRange(data);
                    Db.SaveChanges();

                    await transaction.CommitAsync();
                }

                catch (System.Exception ec)
                {
                    await transaction.RollbackAsync();
                    throw ec;
                }
            }
        }

        public async Task<AgencyNewsFile> GetAgencyNewsFileById(int id)
        {
            return await Db.AgencyNewsFile.FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
