﻿using DragonLibrary_.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace DragonLibrary_.Services
{
    public class HeroService : IHeroService
    {
        private readonly Random _random;
        private readonly IJWTService _jWTService;
        private readonly ILogger _logger;
        private const string HeroNamePattern = @"[A-Za-z0-9][A-Za-z\s0-9]{2,18}[A-Za-z0-9]";
        private readonly EFmodels.ApplicationDBContext _context;
        private readonly int _pageSize;
        public HeroService(IJWTService jWTService,
            ILogger logger,
            EFmodels.ApplicationDBContext context,
            IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jWTService = jWTService ?? throw new ArgumentNullException(nameof(jWTService));
            _random = new Random();
            _pageSize = configuration.GetValue<int>("PageSize");
        }

        public async Task<string> CreateHeroAsync(string name)
        {
            _logger.Debug("Try to create hero {@name}", name);

            bool isNameUnique = ValidateHeroName(name);

            if (isNameUnique)
            {
                var hero = CreateHero(name);
                _context.Heroes.Add(hero);
                await _context.SaveChangesAsync();
                var token = _jWTService.GetToken(hero.Name);

                return token;
            }
            else
            {
                _logger.Error("Error while adding {@name}.", name);

                throw new Exception("Hero already exists or name not right.");
            }
        }

        public Task<IEnumerable<Hero>> GetAllHeroes()
        {
            _logger.Debug("Getting all heroes.");

            return Task.FromResult(_context.Heroes.OrderBy(h => h.Name)
                .Select(h => new Hero(h.Id, h.Name, h.Created, h.Weapon))
                .AsEnumerable());
        }

        public Task<IEnumerable<Hero>> GetPageWithHeroesAsync(IEnumerable<Hero> allHeroes, int pageNumber)
        {
            _logger.Debug("Getting {@number} page of heroes.", pageNumber);

            return Task.FromResult(allHeroes.Skip((pageNumber - 1) * _pageSize)
                        .Take(_pageSize)
                        .AsEnumerable());
        }

        public Task<IEnumerable<Hero>> FilterHeroesByNameAsync(IEnumerable<Hero> heroes, string beginningOfTheName)
        {
            _logger.Debug("Filtering heroes by name.");

            return Task.FromResult(heroes.Where(h => h.Name.StartsWith(beginningOfTheName)));
        }

        public Task<IEnumerable<Hero>> FilterHeroesCreatedBeforeAsync(IEnumerable<Hero> heroes, DateTime filteringTime)
        {
            _logger.Debug("Filtering heroes by time (created before).");

            return Task.FromResult(heroes.Where(h => h.Created < filteringTime));
        }

        public Task<IEnumerable<Hero>> FilterHeroesCreatedAfterAsync(IEnumerable<Hero> heroes, DateTime filteringTime)
        {
            _logger.Debug("Filtering heroes by time (created after).");

            return Task.FromResult(heroes.Where(h => h.Created > filteringTime));
        }

        public Task<IEnumerable<Hero>> GetSortedHeroesAsync(int id)
        {
            _logger.Debug("Sorting heroes.");

            return Task.FromResult(_context.Heroes.Where(h => h.Id == id)
                .Select(h => new Hero(h.Id, h.Name, h.Created, h.Weapon))
                .AsEnumerable());
        }

        private EFmodels.Hero CreateHero(string name)
        {
            var creationTime = DateTime.Now;
            var weapon = _random.Next(1, 6);

            var hero = new EFmodels.Hero { Name = name, Created = creationTime, Weapon = weapon };
            _logger.Debug("Hero {@hero} created.",hero);

            return hero;
        }

        private bool ValidateHeroName(string name)
        {
            bool isNameUnique = !_context.Heroes.Any(h => h.Name == name);

            Regex regex = new Regex(HeroNamePattern);
            bool isNameValid = regex.Replace(name, "").Length == 0;

            _logger.Debug("HeroService.ValidateHeroName: isNameUnique {@isNameUnique} isNameValid {@isNameValid}",
                isNameUnique, isNameValid);

            return isNameUnique && isNameValid;
        }

        public bool IsHeroExists(string name)
        {
            return _context.Heroes.Any(h => h.Name == name);
        }

        public Hero GetHeroByName(string name)
        {
            var hero = _context.Heroes.FirstOrDefault(h => h.Name == name);

            if (hero == null)
            {
                throw new Exception("Hero doesn't exists.");
            }

            return new Hero(hero.Id, hero.Name, hero.Created, hero.Weapon);
        }
    }
}
