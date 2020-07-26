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
        //Task GetHeroDamageStatistic(int id);
    }
}
