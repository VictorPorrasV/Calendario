using Calendario.Models;
using Calendario.Repositorio.Interfaces;
using Microsoft.Data.SqlClient;

namespace Calendario.Repositorio.Servicios
{
    public class UsuarioRepositorio:IUsuario
    {

        public int ObtenerUsuarioID()
        {
            return 1;
        }

    }
}
