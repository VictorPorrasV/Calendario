using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using static Dapper.SqlMapper;

namespace Calendario.Models
{
    public class Usuarios
    {
       
     public int UsuarioId { get; set; }
     public string UserName { get; set; }
     public string  Email {  get; set; }
      
     public string  PasswordHash { get; set; }
     public int  RoleId { get; set; } 

    }
}
