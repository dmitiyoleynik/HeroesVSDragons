using DragonLibrary_.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public interface IHitService
    {
        Task<IEnumerable<Hit>> GetHitsAsync();
        Task<IEnumerable<DamageStatistic>> GetHeroDamageStatistic(int id);
        Task CreateHit(Hero hero, int dragonId);
        Task<IEnumerable<DamageStatistic>> GetPageWithDamagesAsync(IEnumerable<DamageStatistic> damageStatistic, int pageNumber);
        Task<IEnumerable<DamageStatistic>> SortByNameAsc(IEnumerable<DamageStatistic> damageStatistic);
        Task<IEnumerable<DamageStatistic>> SortByNameDesc(IEnumerable<DamageStatistic> damageStatistic);

        Task<IEnumerable<DamageStatistic>> SortByDamageAsc(IEnumerable<DamageStatistic> damageStatistic);
        Task<IEnumerable<DamageStatistic>> SortByDamageDesc(IEnumerable<DamageStatistic> damageStatistic);
    }
}
