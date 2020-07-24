using DragonLibrary_.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public interface IHeroService
    {
        Task<IEnumerable<Hero>> GetHeroesAsync();
        Task<IEnumerable<Hero>> GetSortedHeroesAsync(int id);
        Task<string> CreateHeroAsync(string name);
    }
}
