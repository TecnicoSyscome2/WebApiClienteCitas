using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApi.Citas.ClientesApp.Modelos;

namespace WebApi.Citas.ClientesApp.Auth
{
    public interface IautenticacionService
    {
        string Authenticate(userModel pUsuario);
    }
}
