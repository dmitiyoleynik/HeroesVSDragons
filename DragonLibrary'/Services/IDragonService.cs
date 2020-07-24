using DragonLibrary_.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.Services
{
    public interface IDragonService
    {
        IEnumerable<Dragon> GetDragons();
    }
}
