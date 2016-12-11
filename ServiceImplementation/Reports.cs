using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using DomainRepository;
using ServiceInterface;

namespace ServiceImplementation
{
    public class ReportByLast7Days : BaseService, IReport
    {
        public ReportByLast7Days(ShortenURlDbContext shortenURlDbContext) : base(shortenURlDbContext)
        {

        }
        private string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssfff");
        }
        public async Task<List<DataPoint>> GetReport(int URLId)
        {
            var pastDate = DateTime.Now.Date.AddDays(-7);
            var combo = from p in ShortenURlDbContext.RequestHistory.Where(c => c.ShortUrlId == URLId)
                        where p.HitAt > pastDate
                        group p by DbFunctions.TruncateTime(p.HitAt).Value
                into pgroup
                        let count = pgroup.Count()
                        orderby pgroup.Key
                        select new DataPoint()
                        {
                            DateTime = pgroup.Key,
                            HitCount = count
                        };
            var model = await combo.ToListAsync();

            if (model != null && model.Any())
            {
                foreach (var dataPoint in model)
                {
                    dataPoint.TimeStamp = long.Parse(GetTimestamp(dataPoint.DateTime));
                }
            }
            return model;
        }
    }

    public class ReportByBrowser : BaseService, IReport
    {
        public ReportByBrowser(ShortenURlDbContext shortenURlDbContext) : base(shortenURlDbContext)
        {
        }

        public async Task<List<DataPoint>> GetReport(int URLId)
        {
            var results = from p in ShortenURlDbContext.RequestHistory.Where(c => c.ShortUrlId == URLId)
                          group p by p.Browser into g
                          let count = g.Count()
                          select new DataPoint() { Label = g.Key, LegendText = g.Key, HitCount = count };
            var model = await results.ToListAsync();
            
            if (model != null && model.Any())
            {
                var total = model.Sum(c => c.HitCount);
                foreach (var dataPoint in model)
                {
                    dataPoint.Percent = dataPoint.HitCount * 100 / total;
                }
            }
            return model;
        }
    }

    public class ReportByCountry : BaseService, IReport
    {
        public ReportByCountry(ShortenURlDbContext shortenURlDbContext) : base(shortenURlDbContext)
        {
        }

        public async Task<List<DataPoint>> GetReport(int URLId)
        {
            var results = from p in ShortenURlDbContext.RequestHistory.Where(c => c.ShortUrlId == URLId)
                          group p by p.Country into g
                          let count = g.Count()
                          select new DataPoint() { Label = g.Key, LegendText = g.Key, HitCount = count };
            var model = await results.ToListAsync();
            if (model != null && model.Any())
            {
                var total = model.Sum(c => c.HitCount);
                foreach (var dataPoint in model)
                {
                    dataPoint.Percent = dataPoint.HitCount * 100 / total;
                }
            }
            return model;
        }
    }

    public class ReportByPlatforms : BaseService, IReport
    {
        public ReportByPlatforms(ShortenURlDbContext shortenURlDbContext) : base(shortenURlDbContext)
        {
        }

        public async Task<List<DataPoint>> GetReport(int URLId)
        {
            var results = from p in ShortenURlDbContext.RequestHistory.Where(c => c.ShortUrlId == URLId)
                          group p by p.Platforms into g
                          let count = g.Count()
                          select new DataPoint() { Label = g.Key, LegendText = g.Key, HitCount = count };
            var model = await results.ToListAsync();
            if (model != null && model.Any())
            {
                var total = model.Sum(c => c.HitCount);
                foreach (var dataPoint in model)
                {
                    dataPoint.Percent = dataPoint.HitCount * 100 / total;
                }
            }
            return model;
        }
    }
}
