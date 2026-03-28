using System;

namespace GameTracker_Mobile;

public partial class DetalleJuegoPage : ContentPage
{
    GestorJuegos gestor = new GestorJuegos();
    Juego juegoActual;

    public DetalleJuegoPage(Juego juego)
    {
        InitializeComponent();
        juegoActual = juego;

        // Llenamos los selectores de tiempo (0 a 24 horas y 0 a 59 minutos)
        for (int h = 0; h <= 24; h++) pickerHoras.Items.Add(h.ToString());
        for (int m = 0; m <= 59; m++) pickerMinutos.Items.Add(m.ToString());

        // Cargamos los datos en la pantalla
        lblNombre.Text = juegoActual.Nombre;
        lblHistorial.Text = string.IsNullOrWhiteSpace(juegoActual.Notas) ? "Sin registros." : juegoActual.Notas;

        // Ponemos los relojes en 0
        pickerHoras.SelectedIndex = 0;
        pickerMinutos.SelectedIndex = 0;
    }

    // MÉTODO PARA VER NOTAS (Corrige error MAUIX)
    private async void OnVerNotasClicked(object sender, EventArgs e)
    {
        string contenido = string.IsNullOrWhiteSpace(juegoActual.Notas)
                           ? "Aún no tienes notas."
                           : juegoActual.Notas;

        await DisplayAlert("Notas de " + juegoActual.Nombre, contenido, "Cerrar");
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        try
        {
            // 1. Guardar la nueva nota sin borrar la anterior
            if (!string.IsNullOrWhiteSpace(txtNotas.Text))
            {
                string fecha = DateTime.Now.ToString("dd/MM HH:mm");
                juegoActual.Notas += $"\n[{fecha}]: {txtNotas.Text}";
            }

            // 2. Sumar el tiempo nuevo al total
            int hExtra = int.Parse(pickerHoras.SelectedItem?.ToString() ?? "0");
            int mExtra = int.Parse(pickerMinutos.SelectedItem?.ToString() ?? "0");

            int minTotales = juegoActual.MinutosInvertidos + mExtra;

            // Si pasamos de 60 minutos, sumamos la hora
            juegoActual.MinutosInvertidos = minTotales % 60;
            juegoActual.HorasInvertidas += hExtra + (minTotales / 60);

            // 3. Actualizar el archivo JSON
            gestor.ActualizarJuego(juegoActual);

            // Actualizar la pantalla
            lblHistorial.Text = juegoActual.Notas;
            txtNotas.Text = string.Empty;
            pickerHoras.SelectedIndex = 0;
            pickerMinutos.SelectedIndex = 0;

            await DisplayAlert("Éxito", "¡Tiempo y notas guardados!", "OK");
        }
        catch (Exception)
        {
            await DisplayAlert("Error", "No se pudo guardar. Intenta de nuevo.", "OK");
        }
    }

    private void OnVolverClicked(object sender, EventArgs e)
    {
        // 1. En lugar de NavigationPage, reiniciamos el AppShell
        // Esto destruye la página actual y recarga el menú lateral (3 rayitas)
        App.Current.MainPage = new AppShell();

        // 2. Opcional: Si quieres que al abrirse el Shell se vaya directo 
        // a la pestaña de Biblioteca (y no a la de Agregar):
        Shell.Current.GoToAsync("//ListaJuegosPage", false);
    }

}