namespace GameTracker_Mobile
{
    public partial class DetalleJuegoPage : ContentPage
    {
        Juego _juego;
        GestorJuegos _gestor = new GestorJuegos();

        public DetalleJuegoPage(Juego juego)
        {
            InitializeComponent();
            _juego = juego;

            lblNombre.Text = _juego.Nombre;
            txtNotas.Text = _juego.Estado;
        }

        private async void OnGuardarNotasClicked(object sender, EventArgs e)
        {
            _juego.Estado = txtNotas.Text;
            _gestor.ActualizarJuego(_juego);
            await DisplayAlert("Guardado", "Tus notas se han actualizado.", "OK");
        }

        private async void OnVolverClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}