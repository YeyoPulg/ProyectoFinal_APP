using System.Security.Claims;

namespace ProyectoFinal_APP.Controllers
{
    public static class Funcion
    {
        public static bool ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
