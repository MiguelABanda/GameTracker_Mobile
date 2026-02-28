namespace GameTracker_Mobile;

public partial class ListaJuegosPage : ContentPage
{
    GestorJuegos gestor = new GestorJuegos();

    public ListaJuegosPage() { InitializeComponent(); }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        listaJuegos.ItemsSource = gestor.ObtenerJuegos();
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Juego juego)
        {
            await Navigation.PushAsync(new DetalleJuegoPage(juego));
            ((ListView)sender).SelectedItem = null;
        }
    }
}