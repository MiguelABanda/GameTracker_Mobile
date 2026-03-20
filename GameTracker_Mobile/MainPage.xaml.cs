namespace GameTracker_Mobile;

public partial class MainPage : ContentPage
{
    GestorJuegos gestor = new GestorJuegos();

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnAgregarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text) || pickerPlataforma.SelectedIndex == -1)
        {
            await DisplayAlert("Error", "Completa los datos", "OK");
            return;
        }

        var nuevo = new Juego
        {
            Nombre = txtNombre.Text,
            Plataforma = pickerPlataforma.SelectedItem.ToString(),
            Genero = pickerGenero.SelectedIndex != -1 ?
                     (GeneroJuego)System.Enum.Parse(typeof(GeneroJuego), pickerGenero.SelectedItem.ToString()) :
                     GeneroJuego.Accion,
            Estado = "Pendiente"
        };

        gestor.AgregarJuego(nuevo);
        txtNombre.Text = "";
        await DisplayAlert("Éxito", "Juego guardado", "OK");
    }
}