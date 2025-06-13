using SQLite;

namespace projetoapp1914.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
