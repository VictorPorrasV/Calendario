namespace Calendario.Models
{
    public class Eventos
    {

        public int EventId { get; set; }
        public string Titulo {  get; set; }
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public int UsuarioId { get; set; }

        public string Color { get; set; }

    }
}
