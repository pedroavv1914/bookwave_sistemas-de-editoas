using projetoapp1914.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace projetoapp1914.paginas;

public partial class cadastroAutor : ContentPage
{
    ObservableCollection<Autor> lista = new ObservableCollection<Autor>();

    public cadastroAutor()
    {
        InitializeComponent();
        lstAutor.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();
            List<Autor> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));
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
            if (string.IsNullOrWhiteSpace(autorEntry.Text) ||
                string.IsNullOrWhiteSpace(pseudEntry.Text) ||
                string.IsNullOrWhiteSpace(obsvEntry.Text))
            {
                await DisplayAlert("Aviso", "Por favor, preencha todos os campos", "OK");
                return;
            }

            Autor a = new Autor
            {
                Nome = autorEntry.Text,
                Pseudonimo = pseudEntry.Text,
                Observacoes = obsvEntry.Text
            };

            await App.Db.Insert(a);
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

            autorEntry.Text = string.Empty;
            pseudEntry.Text = string.Empty;
            obsvEntry.Text = string.Empty;

            OnAppearing();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void btnvoltarAutores(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void buscaedEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;
            lista.Clear();

            List<Autor> tmp = await App.Db.Search(q);
            tmp.ForEach(i => lista.Add(i));
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
            Autor a = selecionado.BindingContext as Autor;

            bool confirm = await DisplayAlert("Tem certeza?", $"Remover {a.Nome}", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(a.Id);
                lista.Remove(a);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void lstAutor_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem == null) return;

            Autor a = e.SelectedItem as Autor;
            Navigation.PushAsync(new paginas.EditarAutor
            {
                BindingContext = a,
            });

            ((ListView)sender).SelectedItem = null;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}