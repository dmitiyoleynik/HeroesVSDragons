using DragonLibrary_.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public class HitService : IHitService
    {
        private readonly IList<Hit> _hits;
        private readonly ILogger _logger;

        public HitService(ILogger logger)
        {
            _logger = logger;
            _hits = new List<Hit>();
            _hits.Add(new Hit(5, DateTime.Now, 1, 1));
        }
        public Task<IEnumerable<Hit>> GetHitsAsync()
        {
            return Task.FromResult(_hits.AsEnumerable());
        }
    }
}
