using DragonLibrary_.Models;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonLibrary_.Services
{
    public class DragonService : IDragonService
    {
        private readonly IList<Dragon> _dragons;
        private readonly Random _random;

        public DragonService()
        {
            _random = new Random();
            _dragons = new List<Dragon>();
            _dragons.Add(new Dragon(1, "Paturnax", 100, DateTime.Now));
        }
        public Task<string> CreateDragonAsync()
        {
            string name = CreateDragonName();
            int id = _dragons.Count;
            int hp = _random.Next(80, 100);
            DateTime created = DateTime.Now;

            var dragon = new Dragon(id, name, hp, created);
            _dragons.Add(dragon);

            return Task.FromResult(id.ToString());

        }

        public Task<IEnumerable<Dragon>> GetDragonsAsync()
        {
            return Task.FromResult(_dragons.AsEnumerable());
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
