using DragonLibrary_.Models;
using System.Collections.Generic;

namespace DragonLibrary_.Services
{
    public interface IHitService
    {
        IEnumerable<Hit> GetHits();
    }
}
