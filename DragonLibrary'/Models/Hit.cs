using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.Models
{
    public class Hit
    {
        public Hit(int power, DateTime executionTime, int heroId, int dragonId)
        {
            Power = power;
            ExecutionTime = executionTime;
            HeroId = heroId;
            DragonId = dragonId;
        }

        public int Power { get; }
        public DateTime ExecutionTime { get; }
        public int HeroId { get; }
        public int DragonId { get; }
    }
}
