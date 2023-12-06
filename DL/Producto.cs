using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Producto
    {
        public Producto()
        {
            Compras = new HashSet<Compra>();
        }

        public int IdProducto { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal Precio { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
    }
}
