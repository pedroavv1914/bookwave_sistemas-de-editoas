using projetoapp1914.Models;

namespace projetoapp1914.paginas;

public partial class EditarEditora : ContentPage
{
	public EditarEditora()
	{
		InitializeComponent();
	}

    private async void Salvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(editoraEntry.Text) ||
                string.IsNullOrWhiteSpace(siglaEntry.Text) ||
                string.IsNullOrWhiteSpace(obsEditoraEntry.Text))
            {
                await DisplayAlert("Aviso", "Por favor, preencha os campos", "OK");
                return;
            }

            Editora editora_anexado = BindingContext as Editora;

            Editora ed = new Editora
            {
                Id = editora_anexado.Id,
                Nome = editoraEntry.Text,
                Sigla = siglaEntry.Text,
                Observacoes = obsEditoraEntry.Text
            };

            await App.Db.UpdateEditora(ed);
            await DisplayAlert("Sucesso!", "Registro Alterado", "OK");
            await Navigation.PopAsync();
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
}