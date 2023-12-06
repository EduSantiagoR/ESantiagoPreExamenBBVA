using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Compra
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoPreExamenBBVAContext context = new DL.ESantiagoPreExamenBBVAContext())
                {
                    var query = context.Compras.FromSqlRaw("CompraGetAll");
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Compra compra = new ML.Compra();
                            compra.Producto = new ML.Producto();

                            compra.IdCompra = item.IdCompra;
                            compra.Producto.IdProducto = item.IdProducto;
                            compra.Producto.Tipo = item.Tipo;
                            compra.Producto.Precio = item.Precio;
                            compra.Ingreso = item.Ingreso;
                            compra.Cambio = item.Cambio;
                            compra.Total = item.Total.Value;
                            result.Objects.Add(compra);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se han podido recuperar las compras.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int idCompra)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoPreExamenBBVAContext context = new DL.ESantiagoPreExamenBBVAContext())
                {
                    var query = context.Compras.FromSqlRaw($"CompraGetById {idCompra}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        result.Object = new object();
                        ML.Compra compra = new ML.Compra();
                        compra.Producto = new ML.Producto();

                        compra.IdCompra = query.IdCompra;
                        compra.Producto.IdProducto = query.IdProducto;
                        compra.Producto.Precio = query.Precio;
                        compra.Producto.Tipo = query.Tipo;
                        compra.Ingreso = query.Ingreso;
                        compra.Cambio = query.Cambio;
                        compra.Total = query.Total.Value;

                        result.Object = compra;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se ha podido recuperar el detalle de la compra";
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
        public static ML.Result Add(ML.Compra compra)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoPreExamenBBVAContext context = new DL.ESantiagoPreExamenBBVAContext())
                {
                    int rowsAffected = context.Database.ExecuteSqlRaw($"CompraAdd {compra.Producto.IdProducto},{compra.Ingreso},{compra.Cambio}");
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al registrar la compra.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int idCompra)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoPreExamenBBVAContext context = new DL.ESantiagoPreExamenBBVAContext())
                {
                    int rowsAffected = context.Database.ExecuteSqlRaw($"CompraDelete {idCompra}");
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al eliminar la compra.";
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