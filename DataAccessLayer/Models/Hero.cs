using System;

namespace DataAccessLayer.Models
{
    public class Hero
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public int Weapon { get; set; }//I would like to change it to Enum in future
    }
}
/*
 * 
 * 
Идентификатор (id);+
Имя (в начале и в конце не должно быть пробелов, латинские символы, цифры, пробелы, [4, 20] размер);+-
Время создания героя в системе (дата и время);+
Оружие (целое число от 1 до 6, определяется случайным образом при создании);+
*/