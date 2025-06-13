using projetoapp1914.Models;
using System.Collections.ObjectModel;

namespace projetoapp1914.paginas;

public partial class EditarLivro : ContentPage
{
    ObservableCollection<Autor> listaAutores = new ObservableCollection<Autor>();
    ObservableCollection<Editora> listaEditoras = new ObservableCollection<Editora>();

    public EditarLivro()
    {
        InitializeComponent();

        picAutores.ItemsSource = listaAutores;
        picAutores.ItemDisplayBinding = new Binding("Nome");

        picEditoras.ItemsSource = listaEditoras;
        picEditoras.ItemDisplayBinding = new Binding("Nome");
    }

    protected async override void OnAppearing()
    {
        try
        {
            listaAutores.Clear();
            listaEditoras.Clear();

            List<Autor> tmpAutores = await App.Db.GetAllAutoresAsync();
            List<Editora> tmpEditoras = await App.Db.GetAllEditorasAsync();

            tmpAutores.ForEach(a => listaAutores.Add(a));
            tmpEditoras.ForEach(e => listaEditoras.Add(e));

            if (BindingContext is Livro livro)
            {
                if (livro.AutorId > 0)
                {
                    var autorSelecionado = listaAutores.FirstOrDefault(a => a.Id == livro.AutorId);
                    picAutores.SelectedItem = autorSelecionado;
                }

                if (livro.EditoraId > 0)
                {
                    var editoraSelecionada = listaEditoras.FirstOrDefault(e => e.Id == livro.EditoraId);
                    picEditoras.SelectedItem = editoraSelecionada;
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("OPS", ex.Message, "OK");
        }
    }

    private async void Salvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(livroEntry.Text) ||
                string.IsNullOrWhiteSpace(anoPublicacaoEntry.Text) ||
                string.IsNullOrWhiteSpace(sbnEntry.Text) ||
                string.IsNullOrWhiteSpace(obsvLivroEntry.Text) ||
                picAutores.SelectedIndex == -1 ||
                picEditoras.SelectedIndex == -1)
            {
                await DisplayAlert("Aviso", "Por favor, preencha todos os campos", "OK");
                return;
            }

            Livro livro_anexado = BindingContext as Livro;
            Autor autorSelecionado = (Autor)picAutores.SelectedItem;
            Editora editoraSelecionada = (Editora)picEditoras.SelectedItem;

            Livro l = new Livro
            {
                Id = livro_anexado.Id,
                Nome = livroEntry.Text,
                AnoPublicacao = anoPublicacaoEntry.Text,
                Sbn = sbnEntry.Text,
                LivroObservacoes = obsvLivroEntry.Text,
                AutorId = autorSelecionado.Id,
                NomeAutor = autorSelecionado.Nome,
                EditoraId = editoraSelecionada.Id,
                NomeEditora = editoraSelecionada.Nome
            };

            await App.Db.UpdateLivro(l);

            await DisplayAlert("Sucesso!", "Registro Alterado", "OK");
            await Navigation.PopAsync();
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
}
