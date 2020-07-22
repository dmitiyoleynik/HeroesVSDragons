using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Hit
    {
        public int Power { get; set; }
        public DateTime CreationDate { get; set; }
        public int HeroId { get; set; }
        public int DragonId { get; set; }
    }
}
/*
 * 
 Сила удара (какое количество единиц жизни отнимает)*;
Время удара;+
Принадлежность к игроку (id);+
Принадлежность к дракону (id).+
*Формула расчета силы удара = Сила Оружия героя + (случайное число от 1 до 3)
*/
