namespace projetoapp1914.paginas;

public partial class paginaInicial : ContentPage
{
    public paginaInicial()
    {
        InitializeComponent();
    }

    private async void OnCadastroButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new cadastroLivro());
    }

    private async void OnListaButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new cadastroEditora());
    }

    private async void btnbuscarlivro(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new cadastroAutor());
    }

    private async void btnSobre(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new sobre());
    }

    private async void btncadastrarUsuarios(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new cadastroUsuario());
    }
}