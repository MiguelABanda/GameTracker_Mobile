namespace GameTracker_Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // IMPORTANTE: Aquí cambiamos NavigationPage por AppShell
        MainPage = new AppShell();
    }
}