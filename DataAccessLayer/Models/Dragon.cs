using System;

namespace DataAccessLayer.Models
{
    public class Dragon
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int HP { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
/*
Идентификатор (id);+
Имя (в начале и в конце не должно быть пробелов, первый символ всегда заглавный, латинские символы, пробелы, [4, 20] размер);+
Количество жизней (целое число от 80 до 100 определяется случайным образом при создании);+
Время создания дракона в системе (дата и время).
*/