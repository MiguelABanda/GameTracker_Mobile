using System;
using System.Collections.Generic;
using System.Linq;

namespace GameTracker_Mobile;

public partial class ListaJuegosPage : ContentPage
{
    // Instancia del gestor de datos
    GestorJuegos gestor = new GestorJuegos();

    public ListaJuegosPage()
    {
        InitializeComponent();
    }

    // Este método se dispara cada vez que entras a la página o regresas de detalles
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarListaSegura();
    }

    private void CargarListaSegura()
    {
        // Usamos Dispatcher para que la carga no choque con la interfaz de Android
        Dispatcher.Dispatch(() =>
        {
            try
            {
                var juegos = gestor.ObtenerJuegos();

                // Verificamos que el control x:Name="listaJuegos" exista en el XAML
                if (listaJuegos != null)
                {
                    // Limpiamos y reasignamos para refrescar horas y notas nuevas
                    listaJuegos.ItemsSource = null;
                    listaJuegos.ItemsSource = juegos;
                }
            }
            catch (Exception ex)
            {
                // Si algo falla, lo vemos en la consola de Visual Studio
                Console.WriteLine("Error al cargar biblioteca: " + ex.Message);
            }
        });
    }

    // Método para cuando tocas un juego en la lista
    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        // 1. Validación de seguridad: si no hay selección, no hacemos nada
        if (e.SelectedItem == null) return;

        if (e.SelectedItem is Juego juegoSeleccionado)
        {
            // 2. IMPORTANTE: Quitamos la selección (el color gris) de inmediato.
            // Esto evita que al volver la app intente abrirlo dos veces y se cierre.
            ((ListView)sender).SelectedItem = null;

            // 3. Navegamos a la página de detalles pasando el objeto juego
            await Navigation.PushAsync(new DetalleJuegoPage(juegoSeleccionado));
        }
    }

    // Opcional: Si tienes un botón para ir a registrar uno nuevo
    private async void OnAgregarNuevoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}