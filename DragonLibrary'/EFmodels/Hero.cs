using System;

namespace DragonLibrary_.EFmodels
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public int Weapon { get; set; }
    }
}
