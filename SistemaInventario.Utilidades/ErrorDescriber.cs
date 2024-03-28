using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Utilidades
{

    //La clase IdentityErrorDescriber es una clase para indicar los errores 
    public class ErrorDescriber : IdentityErrorDescriber
    {

        //Sobre escribimos el metodo para que ahora mande el mensaje de minuscula requerida en password
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresLower),
                Description = "El password deberá tener al menos una letra minuscula"
            };
        }
    }
}
