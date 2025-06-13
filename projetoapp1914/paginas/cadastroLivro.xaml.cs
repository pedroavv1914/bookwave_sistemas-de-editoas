using projetoapp1914.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace projetoapp1914.paginas;

public partial class cadastroLivro : ContentPage
{
    ObservableCollection<Livro> lista = new ObservableCollection<Livro>();
    ObservableCollection<Autor> listaAutores = new ObservableCollection<Autor>();
    ObservableCollection<Editora> listaEditoras = new ObservableCollection<Editora>();

    public cadastroLivro()
    {
        try
        {
            InitializeComponent();

            picAutores.ItemsSource = listaAutores;
            picAutores.ItemDisplayBinding = new Binding("Nome");

            picEditoras.ItemsSource = listaEditoras;
            picEditoras.ItemDisplayBinding = new Binding("Nome");

            lstLivro.ItemsSource = lista;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao construir página: {ex.Message}\n{ex.StackTrace}");
        }
    }

    public async Task CarregarDadosAsync()
    {
        try
        {
            lista.Clear();
            listaAutores.Clear();
            listaEditoras.Clear();

            List<Livro> livros = await App.Db.GetAllLivro();
            List<Autor> autores = await App.Db.GetAllAutoresAsync();
            List<Editora> editoras = await App.Db.GetAllEditorasAsync();

            foreach (var livro in livros)
            {
                var autor = autores.FirstOrDefault(a => a.Id == livro.AutorId);
                var editora = editoras.FirstOrDefault(e => e.Id == livro.EditoraId);

                livro.NomeAutor = autor?.Nome ?? "Autor desconhecido";
                livro.NomeEditora = editora?.Nome ?? "Editora desconhecida";

                lista.Add(livro);
            }

            autores.ForEach(a => listaAutores.Add(a));
            editoras.ForEach(e => listaEditoras.Add(e));
        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await CarregarDadosAsync();
    }

    private async void Salvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(livroEntry.Text) ||
                string.IsNullOrWhiteSpace(anoPublicacaoEntry.Text) ||
                string.IsNullOrWhiteSpace(sbnEntry.Text) ||
                string.IsNullOrWhiteSpace(obsvLivroEntry.Text) ||
                (picAutores.SelectedIndex == -1) ||
                (picEditoras.SelectedIndex == -1))
            {
                await DisplayAlert("Aviso", "Por favor, preencha todos os campos", "OK");
                return;
            }

            Autor autorSelecionado = (Autor)picAutores.SelectedItem;
            Editora editoraSelecionada = (Editora)picEditoras.SelectedItem;

            Livro l = new Livro
            {
                Nome = livroEntry.Text,
                AnoPublicacao = anoPublicacaoEntry.Text,
                Sbn = sbnEntry.Text,
                LivroObservacoes = obsvLivroEntry.Text,
                AutorId = autorSelecionado.Id,
                EditoraId = editoraSelecionada.Id
            };

            await App.Db.InsertLivro(l);
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

            livroEntry.Text = string.Empty;
            anoPublicacaoEntry.Text = string.Empty;
            sbnEntry.Text = string.Empty;
            obsvLivroEntry.Text = string.Empty;
            picAutores.SelectedIndex = -1;
            picEditoras.SelectedIndex = -1;

            await CarregarDadosAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void btnvoltarlivro_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void lstLivro_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem == null)
                return;

            Livro l = e.SelectedItem as Livro;

            await Navigation.PushAsync(new paginas.EditarLivro
            {
                BindingContext = l,
            });

            ((ListView)sender).SelectedItem = null;

            await CarregarDadosAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }


    private async void buscaEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            lista.Clear();

            List<Livro> livros = await App.Db.SearchLivro(q);
            List<Autor> autores = await App.Db.GetAllAutoresAsync();
            List<Editora> editoras = await App.Db.GetAllEditorasAsync();

            foreach (var livro in livros)
            {
                var autor = autores.FirstOrDefault(a => a.Id == livro.AutorId);
                var editora = editoras.FirstOrDefault(e => e.Id == livro.EditoraId);

                livro.NomeAutor = autor?.Nome ?? "Autor desconhecido";
                livro.NomeEditora = editora?.Nome ?? "Editora desconhecida";

                lista.Add(livro);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }


    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;

            Livro l = selecionado.BindingContext as Livro;

            bool confirm = await DisplayAlert("Tem certeza?", $"Remover {l.Nome}", "Sim", "Não");

            if (confirm)
            {
                await App.Db.DeleteLivro(l.Id);
                lista.Remove(l);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}
