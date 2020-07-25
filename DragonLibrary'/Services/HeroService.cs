using DragonLibrary_.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DragonLibrary_.Services
{
    public class HeroService : IHeroService
    {
        private readonly Random _random;
        private readonly IJWTService _jWTService;
        private readonly ILogger _logger;
        private const string HeroNamePattern = @"[A-Za-z0-9][A-Za-z\s0-9]{2,18}[A-Za-z0-9]";
        private readonly EFmodels.ApplicationDBContext _context;
        public HeroService(IJWTService jWTService,
            ILogger logger,
            EFmodels.ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jWTService = jWTService ?? throw new ArgumentNullException(nameof(jWTService));
            _random = new Random();
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

                throw new Exception("Tipo exception");//change it to own exception
            }
        }

        public Task<IEnumerable<Hero>> GetHeroesAsync()
        {
            return Task.FromResult(
                _context.Heroes.Select(h => new Hero(h.Id, h.Name, h.Created, h.Weapon)).
                AsEnumerable());
        }

        public Task<IEnumerable<Hero>> GetSortedHeroesAsync(int id)
        {
            return Task.FromResult(_context.Heroes.Where(h => h.Id == id).
                Select(h=>new Hero(h.Id,h.Name,h.Created,h.Weapon)).
                AsEnumerable());
        }

        private EFmodels.Hero CreateHero(string name)
        {
            var creationTime = DateTime.Now;
            var weapon = _random.Next(1, 6);

            var hero = new EFmodels.Hero { Name = name, Created = creationTime, Weapon = weapon };
            return hero;
        }
        private bool ValidateHeroName(string name)
        {
            bool isNameUnique = !_context.Heroes.Any(h => h.Name == name);
            
            Regex regex = new Regex(HeroNamePattern);
            bool isNameValid = regex.Replace(name,"").Length==0;

            _logger.Debug("HeroService.ValidateHeroName: isNameUnique {@isNameUnique} isNameValid {@isNameValid}",
                isNameUnique, isNameValid);

            return isNameUnique && isNameValid;
        }
    }
}
