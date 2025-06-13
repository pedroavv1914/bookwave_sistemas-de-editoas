using projetoapp1914.Models;
using System.Collections.ObjectModel;

namespace projetoapp1914.paginas;

public partial class cadastroEditora : ContentPage
{
    ObservableCollection<Editora> lista = new ObservableCollection<Editora>();

    public cadastroEditora()
    {
        InitializeComponent();

        lstEditora.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();
            List<Editora> tmp = await App.Db.GetAllEditora();

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
            if (string.IsNullOrWhiteSpace(editoraEntry.Text) ||
                string.IsNullOrWhiteSpace(siglaEntry.Text) ||
                string.IsNullOrWhiteSpace(obsEditoraEntry.Text))
            {
                await DisplayAlert("Aviso", "Por favor, preencha todos os campos", "OK");
                return;
            }

            Editora ed = new Editora
            {
                Nome = editoraEntry.Text,
                Sigla = siglaEntry.Text,
                Observacoes = obsEditoraEntry.Text
            };

            await App.Db.InsertEditora(ed);
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

            editoraEntry.Text = string.Empty;
            siglaEntry.Text = string.Empty;
            obsEditoraEntry.Text = string.Empty;

            OnAppearing();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void btnvoltarEditora(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void buscaedEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            lista.Clear();

            List<Editora> tmp = await App.Db.SearchEditora(q);

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

            Editora ed = selecionado.BindingContext as Editora;

            bool confirm = await DisplayAlert("Tem certeza?", $"Remover {ed.Nome}", "Sim", "Não");

            if (confirm)
            {
                await App.Db.DeleteEditora(ed.Id);
                lista.Remove(ed);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void lstEditora_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Editora ed = e.SelectedItem as Editora;

            Navigation.PushAsync(new paginas.EditarEditora
            {
                BindingContext = ed,
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

}