using SQLite;

namespace projetoapp1914.Models
{
    public class Autor
    {
        string _nome;
        string _pseud;
        string _obsv;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nome { get => _nome; set 
            {
                if(value == null)
                {
                    throw new Exception("Por favor, preencha o campo");
                }
                _nome = value;
            }
        }

        public string Pseudonimo
        {
            get => _pseud; set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha o campo");
                }
                _pseud = value;
            }
        }

        public string Observacoes
        {
            get => _obsv; set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha o campo");
                }
                _obsv = value;
            }
        }
    }
}
