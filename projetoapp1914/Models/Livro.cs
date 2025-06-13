using SQLite;

namespace projetoapp1914.Models
{
    public class Livro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } 
        public string Nome { get; set; }
        public string AnoPublicacao { get; set; }
        public string Sbn { get; set; }
        public string LivroObservacoes { get; set; }
        public int CodigoLivro { get; set; }
        public int AutorId { get; set; }
        public string NomeAutor { get; set; }
        public int EditoraId { get; set; }
        public string NomeEditora { get; set; }
    }
}
