namespace projetoapp1914.paginas;

public partial class sobre : ContentPage
{
	public sobre()
	{
		InitializeComponent();
	}

    private async void btnvoltarSobre(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}