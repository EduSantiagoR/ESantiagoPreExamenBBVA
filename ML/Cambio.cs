using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Cambio
    {
        public int IdMaquina { get; set; }
        public int Monedas10 { get; set; }
        public int Billetes50 { get; set; }
        public int Billetes100 { get; set; }

        public Cambio()
        {

        }
        public Cambio(int idMaquina, int monedas10, int billetes50, int billetes100)
        {
            IdMaquina = idMaquina;
            Monedas10 = monedas10;
            Billetes50 = billetes50;
            Billetes100 = billetes100;
        }
    }
}
