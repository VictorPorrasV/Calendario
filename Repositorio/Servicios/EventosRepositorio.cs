using Calendario.Models;
using Calendario.Repositorio.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace Calendario.Repositorio.Serivicios
{
    public class EventosRepositorio:IEventos
    {

        private readonly string _connectionString;

        public EventosRepositorio(IConfiguration connectionString)
        {
            _connectionString = connectionString.GetConnectionString("Conexion"); ;
        }

        public async Task<IEnumerable<Eventos>> GetAllEventosAsync()
        {
            using (var connectionString = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Eventos";
                return await connectionString.QueryAsync<Eventos>(sql);
            }
        }
        public async Task CreateEventoAsync(Eventos evento)
        {
            var sql = "INSERT INTO Eventos (Titulo, FechaInicio, FechaFin, Descripcion, Ubicacion, UsuarioId, Color) VALUES (@Titulo, @FechaInicio, @FechaFin, @Descripcion, @Ubicacion, @UsuarioId, @Color)";


            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, evento);
            }
        }
        public async Task UpdateEventoAsync(Eventos evento)
        {
            var sql = "UPDATE Eventos SET Titulo = @Titulo, FechaInicio = @FechaInicio, FechaFin = @FechaFin, " +
                      "Descripcion = @Descripcion, Ubicacion = @Ubicacion, UsuarioId = @UsuarioId, Color = @Color " +
                      "WHERE EventId = @EventId";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, evento);
            }
        }

        public async Task<Eventos> GetEventoByIdAsync(int id)
        {
            var sql = "SELECT * FROM Eventos WHERE EventId = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<Eventos>(sql, new { Id = id });
            }
        }
      
        public async Task DeleteEventoAsync(int id)
        {
            var sql = "DELETE FROM Eventos WHERE EventId = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }


    }
}
