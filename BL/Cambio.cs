using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cambio
    {
        public static ML.Result GetDineroDisponible()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoPreExamenBBVAContext context = new DL.ESantiagoPreExamenBBVAContext())
                {
                    var query = context.Cambios.FromSqlRaw("CambioGetDenominaciones").AsEnumerable().FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();
                        ML.Cambio cambio = new ML.Cambio(query.IdMaquina, query.Monedas10, query.Billetes50, query.Billetes100);
                        result.Object = cambio;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al recuperar las denominaciones.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
