using DragonLibrary_.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public interface IHitService
    {
        Task<IEnumerable<Hit>> GetHitsAsync();
    }
}
