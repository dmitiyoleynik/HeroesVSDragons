using DragonLibrary_.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public interface IDragonService
    {
        Task<IEnumerable<Dragon>> GetDragonsAsync();
        Task<int> /*Task */CreateDragonAsync();
        Task<Dragon> FindDragonByIdAsync(int id);
        Task<IEnumerable<Dragon>> GetPageWithDragonsAsync(IEnumerable<Dragon> dragons, int pageNumber);
        Task<IEnumerable<Dragon>> FilterDragonsByNameAsync(IEnumerable<Dragon> dragons, string beginningOfTheName);
        Task<IEnumerable<Dragon>> FilterDragonsByHp(IEnumerable<Dragon> dragons, int hpMoreThen, int hpLessThen);
        Task<IEnumerable<Dragon>> FilterDragonsByMaxHp(IEnumerable<Dragon> dragons, int maxHpMoreThen, int maxHpLessThen);
        //bool IsHeroExists(string name);
        //Task<IEnumerable<Hero>> GetAllHeroes();
        //Task<IEnumerable<Hero>> GetSortedHeroesAsync(int id);
        //Task<string> CreateHeroAsync(string name);
    }
}
