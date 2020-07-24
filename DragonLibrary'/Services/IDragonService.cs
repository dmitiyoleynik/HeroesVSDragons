using DragonLibrary_.Models;
using System.Collections.Generic;

namespace DragonLibrary_.Services
{
    public interface IDragonService
    {
        IEnumerable<Dragon> GetDragons();
    }
}
