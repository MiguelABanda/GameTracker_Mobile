namespace GameTracker_Mobile;

public partial class EliminarJuegoPage : ContentPage
{
    GestorJuegos gestor = new GestorJuegos();
    public EliminarJuegoPage() { InitializeComponent(); }

    protected override void OnAppearing() { base.OnAppearing(); Cargar(); }

    void Cargar() { listaBorrar.ItemsSource = null; listaBorrar.ItemsSource = gestor.ObtenerJuegos(); }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Juego j)
        {
            bool res = await DisplayAlert("Borrar", $"¿Eliminar {j.Nombre} de la lista?", "Sí", "No");
            if (res)
            {
                gestor.EliminarJuego(j.Id);
                Cargar();
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}