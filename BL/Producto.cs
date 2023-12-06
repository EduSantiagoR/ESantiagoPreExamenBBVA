using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoPreExamenBBVAContext context = new DL.ESantiagoPreExamenBBVAContext())
                {
                    var query = context.Productos.FromSqlRaw("ProductoGetAll");
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = item.IdProducto;
                            producto.Tipo = item.Tipo;
                            producto.Precio = item.Precio;
                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
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
        public static ML.Result GetById(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoPreExamenBBVAContext context = new DL.ESantiagoPreExamenBBVAContext())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetById {idProducto}").AsEnumerable().FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();
                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = query.IdProducto;
                        producto.Tipo = query.Tipo;
                        producto.Precio = query.Precio;
                        result.Object = producto;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido consultar el producto.";
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
