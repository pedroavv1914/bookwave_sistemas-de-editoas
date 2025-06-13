using projetoapp1914.Models;
using SQLite;

namespace projetoapp1914.Helpers
{
    public class SQLiteDatabaseHelpers
    {
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDatabaseHelpers(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Autor>().Wait();
            _conn.CreateTableAsync<Livro>().Wait();
            _conn.CreateTableAsync<Editora>().Wait();
            _conn.CreateTableAsync<Usuario>().Wait();
        }

        public Task<int> Insert(Autor a)
        {
            return _conn.InsertAsync(a);
        }

        public Task<int> Update(Autor a)
        {
            string sql = "UPDATE Autor SET Nome=?, Pseudonimo=?, Observacoes=? WHERE Id=?";
            return _conn.ExecuteAsync(sql, a.Nome, a.Pseudonimo, a.Observacoes, a.Id);
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Autor>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Autor>> GetAll()
        {
            return _conn.Table<Autor>().ToListAsync();
        }

        public Task<List<Autor>> Search(string q)
        {
            string sql = "SELECT * FROM Autor WHERE Nome LIKE ?";
            return _conn.QueryAsync<Autor>(sql, $"%{q}%", $"%{q}%");
        }

        public async Task<List<Autor>> GetAllAutoresAsync()
        {
            return await _conn.Table<Autor>().ToListAsync();
        }

        // ------------------- LIVRO ---------------------

        public Task<int> InsertLivro(Livro l)
        {
            return _conn.InsertAsync(l);
        }

        public Task<List<Livro>> UpdateLivro(Livro l)
        {
            string sql = "UPDATE Livro SET Nome=?, AnoPublicacao=?, Sbn=?, LivroObservacoes=?, CodigoLivro=?, AutorId=?, NomeAutor=?, EditoraId=?, NomeEditora=? WHERE Id=?";

            return _conn.QueryAsync<Livro>(sql, l.Nome, l.AnoPublicacao, l.Sbn, l.LivroObservacoes, l.CodigoLivro, l.AutorId, l.NomeAutor, l.EditoraId, l.NomeEditora, l.Id);
        }

        public Task<int> DeleteLivro(int id)
        {
            return _conn.Table<Livro>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Livro>> GetAllLivro()
        {
            return _conn.Table<Livro>().ToListAsync();
        }
        public Task<List<Livro>> SearchLivro(string q)
        {
            string sql = "SELECT * FROM Livro WHERE Nome LIKE ?";
            return _conn.QueryAsync<Livro>(sql, $"%{q}%", $"%{q}%");
        }

        public Task<List<Livro>> GetLivrosPorAutorIdAsync(int autorId)
        {
            return _conn.Table<Livro>()
                            .Where(l => l.AutorId == autorId)
                            .ToListAsync();
        }

        public Task<List<Livro>> GetLivrosPorEditoraIdAsync(int editoraId)
        {
            return _conn.Table<Livro>()
                            .Where(l => l.EditoraId == editoraId)
                            .ToListAsync();
        }

        // ------------------- EDITORA ---------------------

        public Task<int> InsertEditora(Editora ed)
        {
            return _conn.InsertAsync(ed);
        }

        public Task<List<Editora>> UpdateEditora(Editora ed)
        {
            string sql = "UPDATE Editora SET Nome=?, Sigla=?, Observacoes=? WHERE Id=?";

            return _conn.QueryAsync<Editora>(sql, ed.Nome, ed.Sigla, ed.Observacoes, ed.Id);
        }

        public Task<int> DeleteEditora(int id)
        {
            return _conn.Table<Editora>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Editora>> GetAllEditora()
        {
            return _conn.Table<Editora>().ToListAsync();
        }

        public Task<List<Editora>> SearchEditora(string q)
        {
            string sql = "SELECT * FROM Editora WHERE Nome LIKE ?";
            return _conn.QueryAsync<Editora>(sql, $"%{q}%", $"%{q}%");
        }

        public async Task<List<Editora>> GetAllEditorasAsync()
        {
            return await _conn.Table<Editora>().ToListAsync();
        }

        // ------------------- USUÁRIOS ---------------------

        public Task<int> InsertUsuario(Usuario u)
        {
            return _conn.InsertAsync(u);
        }

        public Task<List<Usuario>> UpdateUsuario(Usuario u)
        {
            string sql = "UPDATE Usuario SET Nome=?, Senha=? WHERE Codigo=?";

            return _conn.QueryAsync<Usuario>(sql, u.Nome, u.Senha, u.Codigo);
        }

        public Task<int> DeleteUsuario(int id)
        {
            return _conn.Table<Usuario>().DeleteAsync(i => i.Codigo == id);
        }

        public Task<List<Usuario>> GetAllUsuario()
        {
            return _conn.Table<Usuario>().ToListAsync();
        }

        public Task<List<Usuario>> SearchUsuario(string q)
        {
            string sql = "SELECT * FROM Usuario WHERE Nome LIKE ?";
            return _conn.QueryAsync<Usuario>(sql, $"%{q}%", $"%{q}%");
        }

        public async Task<List<Usuario>> GetAllUsuariosAsync()
        {
            return await _conn.Table<Usuario>().ToListAsync();
        }
    }
}
