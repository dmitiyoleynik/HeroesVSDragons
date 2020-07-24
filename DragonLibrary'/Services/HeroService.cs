﻿using DragonLibrary_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public class HeroService: IHeroService
    {
        private readonly IList<Hero> _heroes;
        private readonly Random _random;
        private readonly IJWTService _jWTService;

        public HeroService(IJWTService jWTService)
        {
            _jWTService = jWTService;
            _random = new Random();
            _heroes = new List<Hero>();
            _heroes.Add(new Hero(1, "Hero1", DateTime.Now, 1));
            _heroes.Add(new Hero(2, "Hero2", DateTime.Now, 2));
            _heroes.Add(new Hero(3, "Hero3", DateTime.Now, 3));
            _heroes.Add(new Hero(4, "Hero4", DateTime.Now, 4));
            _heroes.Add(new Hero(5, "Hero5", DateTime.Now, 5));
        }

        public Task<string> CreateHeroAsync(string name)
        {
            bool isNameUnique = !_heroes.Any(h => h.Name == name);

            if (isNameUnique)
            {
                var newId = _heroes.Count+1;
                var hero = CreateHero(newId, name);
                _heroes.Add(hero);
                var token = _jWTService.GetToken(hero.Name);
                
                return Task.FromResult(token);
            }
            throw new Exception("Tipo exception");
        }

        public Task<IEnumerable<Hero>> GetHeroesAsync()
        {
            return Task.FromResult(_heroes.AsEnumerable());
        }

        public Task<IEnumerable<Hero>> GetSortedHeroesAsync(int id)
        {
            return Task.FromResult(_heroes.Where(h=>h.Id==id).AsEnumerable());
        }

        private Hero CreateHero(int id, string name)
        {
            var creationTime = DateTime.Now;
            var weapon = _random.Next(1, 6);

            return new Hero(id, name, creationTime, weapon);
        }
    }
}
