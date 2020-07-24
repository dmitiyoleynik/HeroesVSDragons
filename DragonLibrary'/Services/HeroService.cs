using DragonLibrary_.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public class HeroService: IHeroService
    {
        private readonly IList<Hero> _heroes;

        public HeroService()
        {
            _heroes = new List<Hero>();
            _heroes.Add(new Hero(1, "Hero1", DateTime.Now, 1));
            _heroes.Add(new Hero(2, "Hero2", DateTime.Now, 2));
            _heroes.Add(new Hero(3, "Hero3", DateTime.Now, 3));
            _heroes.Add(new Hero(4, "Hero4", DateTime.Now, 4));
            _heroes.Add(new Hero(5, "Hero5", DateTime.Now, 5));
        }

        //public int createHero(string name)
        //{
        //    throw new NotImplementedException();
        //}

        public Task<IEnumerable<Hero>> GetHeroesAsync()
        {
            return Task.FromResult(_heroes.AsEnumerable());
        }

        public Task<IEnumerable<Hero>> GetSortedHeroesAsync(int id)
        {
            return Task.FromResult(_heroes.Where(h=>h.Id==id).AsEnumerable());
        }
    }
}
