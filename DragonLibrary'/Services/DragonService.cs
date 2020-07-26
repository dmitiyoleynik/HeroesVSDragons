using DragonLibrary_.Models;
using Microsoft.Extensions.Configuration;
using RandomNameGeneratorLibrary;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public class DragonService : IDragonService
    {
        private readonly Random _random;
        private readonly ILogger _logger;
        private readonly int _pageSize;
        private readonly EFmodels.ApplicationDBContext _context;

        public DragonService(ILogger logger,
            EFmodels.ApplicationDBContext context,
            IConfiguration configuration)
        {
            _random = new Random();
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pageSize = configuration.GetValue<int>("PageSize");
        }

        public async Task<int> CreateDragonAsync()
        {
            string name = CreateDragonName();
            int hp = _random.Next(80, 100);
            DateTime created = DateTime.Now;

            var dragon = new EFmodels.Dragon { Hp = hp, MaxHp = hp, Created = DateTime.Now, Name = name };
            _context.Dragons.Add(dragon);
            await _context.SaveChangesAsync();

            return _context.Dragons.FirstOrDefault(d => d.Name == name).Id;

        }

        public Task<IEnumerable<Dragon>> FilterDragonsByHp(IEnumerable<Dragon> dragons, int hpMoreThen = 0, int hpLessThen = 100)
        {
            return Task.FromResult(dragons.Where(d => d.Hp < hpLessThen && d.Hp > hpMoreThen));
        }

        public Task<IEnumerable<Dragon>> FilterDragonsByMaxHp(IEnumerable<Dragon> dragons, int maxHpMoreThen, int maxHpLessThen)
        {
            return Task.FromResult(dragons.Where(d => d.Hp < maxHpLessThen && d.Hp > maxHpMoreThen));
        }

        public Task<IEnumerable<Dragon>> FilterDragonsByNameAsync(IEnumerable<Dragon> allDragons, string beginningOfTheName)
        {
            return Task.FromResult(allDragons.Where(d => d.Name.StartsWith(beginningOfTheName)));
        }

        public Task<Dragon> FindDragonByIdAsync(int id)
        {
            var dragon = _context.Dragons.FirstOrDefault(d => d.Id == id);

            Dragon resultDragon;
            if (dragon == null)
            {
                resultDragon = null;
            }
            else
            {
                resultDragon = new Dragon(dragon.Id,
                dragon.Name,
                dragon.Hp,
                dragon.Created,
                dragon.MaxHp
                );
            }

            return Task.FromResult(resultDragon);
        }

        public Task<IEnumerable<Dragon>> GetDragonsAsync()
        {
            return Task.FromResult(_context.Dragons.OrderBy(d => d.Name)
                .Select(d => new Dragon(d.Id, d.Name, d.Hp, d.Created, d.MaxHp))
                .AsEnumerable());
        }

        public Task<IEnumerable<Dragon>> GetPageWithDragonsAsync(IEnumerable<Dragon> allDragons, int pageNumber)
        {
            return Task.FromResult(allDragons.Skip((pageNumber - 1) * _pageSize)
                        .Take(_pageSize)
                        .AsEnumerable());
        }

        private string CreateDragonName()
        {
            var personNameGenerator = new PersonNameGenerator();

            //(в начале и в конце не должно быть пробелов, первый символ всегда заглавный, латинские символы, пробелы, [4, 20] размер);
            var dragonName = personNameGenerator.GenerateRandomFirstName();

            return dragonName;
        }
    }
}
