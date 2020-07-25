using DragonLibrary_.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public interface IHeroService
    {
        Task<IEnumerable<Hero>> GetPageWithHeroesAsync(IEnumerable<Hero> heroes, int pageNumber);
        Task<IEnumerable<Hero>> FilterHeroesByNameAsync(IEnumerable<Hero> heroes, string name);
        Task<IEnumerable<Hero>> FilterHeroesCreatedBeforeAsync(IEnumerable<Hero> heroes, DateTime filteringTime);
        Task<IEnumerable<Hero>> FilterHeroesCreatedAfterAsync(IEnumerable<Hero> heroes, DateTime filteringTime);
        bool IsHeroExists(string name);
        Task<IEnumerable<Hero>> GetAllHeroes();
        Task<IEnumerable<Hero>> GetSortedHeroesAsync(int id);
        Task<string> CreateHeroAsync(string name);
    }
}
