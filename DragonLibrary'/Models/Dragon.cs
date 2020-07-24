using System;

namespace DragonLibrary_.Models
{
    public class Dragon
    {
        public Dragon(int id, string name, int hp, DateTime created)
        {
            Id = id;
            Name = name;
            Hp = hp;
            MaxHp = hp;
            Created = created;
        }

        public int Id { get; }
        public string Name { get; }
        public int Hp { get; }
        public DateTime Created { get; }
        public DateTime Died { get; set; }
        public int MaxHp { get; set; }
    }
}
