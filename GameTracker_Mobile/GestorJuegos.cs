using System.Text.Json;

namespace GameTracker_Mobile;

public class GestorJuegos
{
    // Usamos un nombre de archivo nuevo para limpiar cualquier basura anterior
    string ruta = Path.Combine(FileSystem.AppDataDirectory, "juegos_vfinal.json");

    public List<Juego> ObtenerJuegos()
    {
        try
        {
            if (!File.Exists(ruta)) return new List<Juego>();
            string json = File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<Juego>>(json) ?? new List<Juego>();
        }
        catch
        {
            return new List<Juego>();
        }
    }

    // ESTE MÉTODO CORRIGE EL ERROR DE MAINPAGE
    public bool ExisteJuego(string nombre, string plataforma)
    {
        var lista = ObtenerJuegos();
        return lista.Any(x => x.Nombre.ToLower() == nombre.ToLower() && x.Plataforma == plataforma);
    }

    public void AgregarJuego(Juego juego)
    {
        var lista = ObtenerJuegos();
        juego.Id = lista.Count > 0 ? lista.Max(x => x.Id) + 1 : 1;
        lista.Add(juego);
        File.WriteAllText(ruta, JsonSerializer.Serialize(lista));
    }

    public void ActualizarJuego(Juego juegoEditado)
    {
        var lista = ObtenerJuegos();
        var index = lista.FindIndex(x => x.Id == juegoEditado.Id);
        if (index != -1)
        {
            lista[index] = juegoEditado;
            File.WriteAllText(ruta, JsonSerializer.Serialize(lista));
        }
    }

    // ESTE MÉTODO CORRIGE EL ERROR DE ELIMINARJUEGOPAGE
    public void EliminarJuego(int id)
    {
        var lista = ObtenerJuegos();
        lista.RemoveAll(x => x.Id == id);
        File.WriteAllText(ruta, JsonSerializer.Serialize(lista));
    }
}