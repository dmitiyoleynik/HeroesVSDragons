﻿using DragonLibrary_.Models;
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
            _logger.Debug("Dragon {@dragon} created.", dragon);

            return _context.Dragons.FirstOrDefault(d => d.Name == name).Id;

        }

        public Task<IEnumerable<Dragon>> FilterDragonsByHp(IEnumerable<Dragon> dragons, int hpMoreThen = 0, int hpLessThen = 100)
        {
            _logger.Debug("Filtering dragons by hp.");

            return Task.FromResult(dragons.Where(d => d.Hp < hpLessThen && d.Hp > hpMoreThen));
        }

        public Task<IEnumerable<Dragon>> FilterDragonsByMaxHp(IEnumerable<Dragon> dragons, int maxHpMoreThen, int maxHpLessThen)
        {
            _logger.Debug("Filtering dragons by max hp.");

            return Task.FromResult(dragons.Where(d => d.Hp < maxHpLessThen && d.Hp > maxHpMoreThen));
        }

        public Task<IEnumerable<Dragon>> FilterDragonsByNameAsync(IEnumerable<Dragon> allDragons, string beginningOfTheName)
        {
            _logger.Debug("Filtering dragons by name.");

            return Task.FromResult(allDragons.Where(d => d.Name.StartsWith(beginningOfTheName)));
        }

        public Task<Dragon> FindDragonByIdAsync(int id)
        {
            var dbDragon = _context.Dragons.FirstOrDefault(d => d.Id == id);

            Dragon dragon;

            if (dbDragon == null || !IsDragonAlive(dbDragon))
            {
                _logger.Debug("Dragon not found.");

                dragon = null;
            }
            else
            {
                dragon = new Dragon(dbDragon.Id,
                dbDragon.Name,
                dbDragon.Hp,
                dbDragon.Created,
                dbDragon.MaxHp
                );
                
                _logger.Debug("Dragon {@dragon} found.", dragon);
            }

            return Task.FromResult(dragon);
        }

        public Task<IEnumerable<Dragon>> GetDragonsAsync()
        {
            return Task.FromResult(_context.Dragons.Where(d=>d.Hp>0)
                .OrderBy(d => d.Name)
                .Select(d => new Dragon(d.Id, d.Name, d.Hp, d.Created, d.MaxHp))
                .AsEnumerable());
        }

        public Task<IEnumerable<Dragon>> GetPageWithDragonsAsync(IEnumerable<Dragon> allDragons, int pageNumber)
        {
            _logger.Debug("Getting {@number} page of dragons.", pageNumber);

            return Task.FromResult(allDragons.Skip((pageNumber - 1) * _pageSize)
                        .Take(_pageSize)
                        .AsEnumerable());
        }

        private bool IsDragonAlive(EFmodels.Dragon dragon)
        {
            return dragon.Hp > 0;
        }

        private string CreateDragonName()
        {
            var personNameGenerator = new PersonNameGenerator();
            var dragonName = personNameGenerator.GenerateRandomFirstName();

            return dragonName;
        }
    }
}
