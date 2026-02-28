namespace GameTracker_Mobile;

public partial class EliminarJuegoPage : ContentPage
{
    GestorJuegos gestor = new GestorJuegos();

    public EliminarJuegoPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarLista();
    }

    void CargarLista()
    {
        listaBorrar.ItemsSource = null;
        listaBorrar.ItemsSource = gestor.ObtenerJuegos();
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Juego juego)
        {
            // Confirmación para no borrar por error
            bool respuesta = await DisplayAlert("Confirmar", $"¿Seguro que quieres borrar {juego.Nombre}?", "Sí, borrar", "Cancelar");

            if (respuesta)
            {
                gestor.EliminarJuego(juego.Id);
                CargarLista(); // Refrescamos la lista
                await DisplayAlert("Eliminado", "El juego ha sido removido", "OK");
            }

            // Deseleccionamos para poder volver a tocar el mismo si quisiéramos
            ((ListView)sender).SelectedItem = null;
        }
    }
}