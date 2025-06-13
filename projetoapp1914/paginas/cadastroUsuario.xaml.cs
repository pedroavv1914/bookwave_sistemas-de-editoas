using projetoapp1914.Models;
using System.Collections.ObjectModel;

namespace projetoapp1914.paginas;

public partial class cadastroUsuario : ContentPage
{
    ObservableCollection<Usuario> lista = new ObservableCollection<Usuario>();

    public cadastroUsuario()
	{
		InitializeComponent();

        lstUsuarios.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();
            List<Usuario> tmp = await App.Db.GetAllUsuario();
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
            if (string.IsNullOrWhiteSpace(nomeEntry.Text) ||
                string.IsNullOrWhiteSpace(senhaEntry.Text))
            {
                await DisplayAlert("Aviso", "Por favor, preencha todos os campos", "OK");
                return;
            }

            Usuario u = new Usuario
            {
                Nome = nomeEntry.Text,
                Senha = senhaEntry.Text,
            };

            await App.Db.InsertUsuario(u);
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

            nomeEntry.Text = string.Empty;
            senhaEntry.Text = string.Empty;

            OnAppearing();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void btnvoltarUsuarios(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void buscaUsuariosEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;
            lista.Clear();

            List<Usuario> tmp = await App.Db.SearchUsuario(q);
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
            Usuario u = selecionado.BindingContext as Usuario;

            bool confirm = await DisplayAlert("Tem certeza?", $"Remover {u.Nome}", "Sim", "Não");

            if (confirm)
            {
                await App.Db.DeleteUsuario(u.Codigo);
                lista.Remove(u);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void lstUsuario_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem == null) return;

            Usuario u = e.SelectedItem as Usuario;
            Navigation.PushAsync(new paginas.EditarUsuario
            {
                BindingContext = u,
            });

            ((ListView)sender).SelectedItem = null;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}