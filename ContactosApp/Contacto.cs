using System;
using SQLite;

namespace ContactosApp
{
    [Table("Contactos")]
    public class Contacto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
