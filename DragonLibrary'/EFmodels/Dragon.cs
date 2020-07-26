using System;

namespace DragonLibrary_.EFmodels
{
    public class Dragon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public DateTime Created { get; set; }
        public DateTime Died { get; set; }

    }
}
