using System;

namespace DragonLibrary_.Models
{
    public class Dragon
    {
        public Dragon(int id, string name, int hp, DateTime created, int maxHp)
        {
            Id = id;
            Name = name;
            Hp = hp;
            Created = created;
            MaxHp = maxHp;
        }

        public int Id { get; }
        public string Name { get; }
        public int Hp { get; }
        public DateTime Created { get; }
        public int MaxHp { get; }
    }
}
