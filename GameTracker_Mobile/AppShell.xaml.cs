namespace GameTracker_Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // REGISTRO DE RUTAS: Esto es vital para que Navigation.PushAsync no falle
        // Al registrar las rutas aquí, el Shell maneja mejor la "pila" de páginas
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ListaJuegosPage), typeof(ListaJuegosPage));
        Routing.RegisterRoute(nameof(DetalleJuegoPage), typeof(DetalleJuegoPage));

        // Si tienes una página para borrar, regístrala también:
        // Routing.RegisterRoute(nameof(EliminarJuegoPage), typeof(EliminarJuegoPage));
    }

    // Opcional: Este método ayuda a limpiar la memoria cuando cambias de sección
    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);

        // Si detectamos que el usuario vuelve atrás, podemos forzar una limpieza leve
        if (args.Source == ShellNavigationSource.Pop)
        {
            // Aquí podrías agregar lógica si fuera necesario, 
            // pero por ahora dejarlo así ayuda a la estabilidad.
        }
    }
}

