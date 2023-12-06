using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Compra
    {
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public decimal Ingreso { get; set; }
        public decimal Cambio { get; set; }
        public decimal? Total { get; set; }

        public string Tipo { get; set; }
        public decimal Precio { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
