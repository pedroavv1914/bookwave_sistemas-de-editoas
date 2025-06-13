using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoapp1914.Models
{
    public class Editora
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Observacoes { get; set; }

    }
}
