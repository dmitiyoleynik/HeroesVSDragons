using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.EFmodels
{
    public class Hit
    {
        public int Id { get; set; }
        public int Power { get; set; }
        public DateTime ExecutionTime { get; set; }
        public int HeroId { get; set; }
        public int DragonId { get; set; }
        public Dragon Dragon { get; set; }
        public Hero Hero { get; set; }
    }
}
