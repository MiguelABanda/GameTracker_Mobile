namespace GameTracker_Mobile;

public partial class Juego
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Plataforma { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    public string Notas { get; set; } = "";
    public int HorasInvertidas { get; set; }
    public int MinutosInvertidos { get; set; }

    public string TiempoTotal => $"{HorasInvertidas}h {MinutosInvertidos}m";
}