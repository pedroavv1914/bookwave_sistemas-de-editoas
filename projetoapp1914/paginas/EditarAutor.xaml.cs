using projetoapp1914.Models;

namespace projetoapp1914.paginas;

public partial class EditarAutor : ContentPage
{
    public EditarAutor()
    {
        InitializeComponent();
    }

    private async void Salvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(autorEntry.Text) ||
                string.IsNullOrWhiteSpace(pseudEntry.Text) ||
                string.IsNullOrWhiteSpace(obsvEntry.Text))
            {
                await DisplayAlert("Aviso", "Por favor, preencha os campos", "OK");
                return;
            }

            Autor autor_anexado = BindingContext as Autor;

            Autor a = new Autor
            {
                Id = autor_anexado.Id,
                Nome = autorEntry.Text,
                Pseudonimo = pseudEntry.Text,
                Observacoes = obsvEntry.Text
            };

            await App.Db.Update(a);
            await DisplayAlert("Sucesso!", "Registro Alterado", "OK");
            await Navigation.PopAsync();
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
}