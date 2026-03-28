namespace GameTracker_Mobile;

public partial class MainPage : ContentPage
{
    GestorJuegos gestor = new GestorJuegos();

    public MainPage() { InitializeComponent(); }

    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text) || pickerPlataforma.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Faltan datos", "OK");
            return;
        }

        // Bloqueo de duplicados
        if (gestor.ExisteJuego(txtNombre.Text, pickerPlataforma.SelectedItem.ToString()))
        {
            await DisplayAlert("Ya registrado", "Ese juego ya existe en esta plataforma", "OK");
            return;
        }

        var nuevo = new Juego
        {
            Nombre = txtNombre.Text,
            Plataforma = pickerPlataforma.SelectedItem.ToString(),
            Genero = pickerGenero.SelectedItem?.ToString() ?? "N/A",
            Estado = "Pendiente",
            FechaRegistro = DateTime.Now
        };

        gestor.AgregarJuego(nuevo);
        txtNombre.Text = "";
        await DisplayAlert("Éxito", "Juego guardado correctamente", "OK");
    }
}