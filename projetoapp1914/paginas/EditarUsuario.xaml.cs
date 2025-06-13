using projetoapp1914.Models;

namespace projetoapp1914.paginas;

public partial class EditarUsuario : ContentPage
{
	public EditarUsuario()
	{
		InitializeComponent();
	}

    private async void Salvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(nomeEntry.Text) ||
                string.IsNullOrWhiteSpace(senhaEntry.Text))
            {
                await DisplayAlert("Aviso", "Por favor, preencha os campos", "OK");
                return;
            }

            Usuario usuario_anexado = BindingContext as Usuario;

            Usuario u = new Usuario
            {
                Codigo = usuario_anexado.Codigo,
                Nome = nomeEntry.Text,
                Senha = senhaEntry.Text
            };

            await App.Db.UpdateUsuario(u);
            await DisplayAlert("Sucesso!", "Registro Alterado", "OK");
            await Navigation.PopAsync();
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
}