using DragonLibrary_.Models;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public class HitService : IHitService
    {
        private readonly ILogger _logger;
        private readonly int _pageSize;
        private readonly Random _random;
        private readonly EFmodels.ApplicationDBContext _context;

        public HitService(ILogger logger,
            EFmodels.ApplicationDBContext context,
            IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pageSize = configuration.GetValue<int>("PageSize");
            _random = new Random();
        }

        public async Task CreateHit(Hero hero, int dragonId)
        {
            var power = _random.Next(1, 3) + hero.Weapon;
            var hit = new EFmodels.Hit { DragonId = dragonId, HeroId = hero.Id, ExecutionTime = DateTime.Now, Power = power };
            var dragon = _context.Dragons.FirstOrDefault(d => d.Id == dragonId);
            dragon.Hp -= power;
            _context.Dragons.Update(dragon);
            _context.Hits.Add(hit);

            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<DamageStatistic>> GetHeroDamageStatistic(int id)
        {
            var heroHits = _context.Hits.Where(h => h.HeroId == id);
            var damageStatistic = heroHits.GroupBy(h => h.DragonId)
               .Select(s => new 
               {
                   DragonName = s.Key,
                   SummDamage = s.Sum(s => s.Power)
               });
            var result = _context.Dragons.Join(damageStatistic,
                d => d.Id,
                s => s.DragonName,
                (d, s) => new DamageStatistic { DragonName = d.Name, SummDamage = s.SummDamage });

            return Task.FromResult(result.AsEnumerable());
        }

        public Task<IEnumerable<Hit>> GetHitsAsync()
        {
            return Task.FromResult(_context.Hits.Select(h => new Hit(h.Power, h.ExecutionTime, h.HeroId, h.DragonId)).AsEnumerable());
        }

        public Task<IEnumerable<DamageStatistic>> GetPageWithDamagesAsync(IEnumerable<DamageStatistic> damageStatistic, int pageNumber)
        {
            return Task.FromResult(damageStatistic.Skip((pageNumber - 1) * _pageSize)
                        .Take(_pageSize)
                        .AsEnumerable());
        }

        public Task<IEnumerable<DamageStatistic>> SortByDamageAsc(IEnumerable<DamageStatistic> damageStatistic)
        {
            return Task.FromResult(damageStatistic.OrderBy(s=>s.SummDamage).AsEnumerable());
        }

        public Task<IEnumerable<DamageStatistic>> SortByDamageDesc(IEnumerable<DamageStatistic> damageStatistic)
        {
            return Task.FromResult(damageStatistic.OrderByDescending(s=>s.SummDamage).AsEnumerable());
        }

        public Task<IEnumerable<DamageStatistic>> SortByNameAsc(IEnumerable<DamageStatistic> damageStatistic)
        {
            return Task.FromResult(damageStatistic.OrderBy(s => s.DragonName).AsEnumerable());
        }

        public Task<IEnumerable<DamageStatistic>> SortByNameDesc(IEnumerable<DamageStatistic> damageStatistic)
        {
            return Task.FromResult(damageStatistic.OrderByDescending(s => s.DragonName).AsEnumerable());
        }
    }
}
