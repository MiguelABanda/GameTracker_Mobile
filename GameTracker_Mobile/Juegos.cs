using System.Text.Json;

namespace GameTracker_Mobile
{
    public enum GeneroJuego { Accion, Aventura, RPG, Shooter, Deportes, Terror }

    public class Juego
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Plataforma { get; set; } = string.Empty;
        public GeneroJuego Genero { get; set; }
        public string Estado { get; set; } = string.Empty; 
    }

    public class GestorJuegos
    {
        private string archivoJson = Path.Combine(FileSystem.AppDataDirectory, "biblioteca.json");

        public List<Juego> ObtenerJuegos()
        {
            if (!File.Exists(archivoJson)) return new List<Juego>();
            try
            {
                string json = File.ReadAllText(archivoJson);
                return JsonSerializer.Deserialize<List<Juego>>(json) ?? new List<Juego>();
            }
            catch { return new List<Juego>(); }
        }

        public void AgregarJuego(Juego nuevo)
        {
            var lista = ObtenerJuegos();
            nuevo.Id = lista.Count > 0 ? lista.Max(j => j.Id) + 1 : 1;
            lista.Add(nuevo);
            GuardarTodo(lista);
        }

        public void ActualizarJuego(Juego juegoActualizado)
        {
            var lista = ObtenerJuegos();
            var index = lista.FindIndex(j => j.Id == juegoActualizado.Id);
            if (index != -1)
            {
                lista[index] = juegoActualizado;
                GuardarTodo(lista);
            }
        }

        public void EliminarJuego(int id)
        {
            var lista = ObtenerJuegos();
            lista.RemoveAll(j => j.Id == id);
            GuardarTodo(lista);
        }

        private void GuardarTodo(List<Juego> lista)
        {
            File.WriteAllText(archivoJson, JsonSerializer.Serialize(lista));
        }
    }
}