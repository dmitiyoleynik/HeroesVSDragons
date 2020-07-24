using System;
using System.Collections.Generic;
using System.Text;

namespace DragonLibrary_.Models
{
    public class Hero
    {
        public Hero(int id, string name, DateTime created, int weapon)
        {
            Id = id;
            Name = name;
            Created = created;
            Weapon = weapon;
        }

        public int Id { get; }
        public string Name { get; }
        public DateTime Created { get; }
        public int Weapon { get; }
    }
}
