using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CompraController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Compra.GetAll();
            ML.Compra compra = new ML.Compra();
            compra.Compras = result.Objects;
            return View(compra);
        }
        [HttpGet]
        public IActionResult Comprar()
        {
            ML.Result result = BL.Producto.GetAll();
            ML.Producto producto = new ML.Producto();
            producto.Productos = result.Objects;
            return View(producto);
        }
        [HttpPost]
        public IActionResult Comprar(int idProducto, int dineroIngresado)
        {
            ML.Result result = BL.Producto.GetById(idProducto);
            ML.Producto producto = (ML.Producto)result.Object;
            if(producto.Precio <= dineroIngresado)
            {   
                int cambio = dineroIngresado - int.Parse(producto.Precio.ToString("0"));
                if (cambio > 0)
                {
                    ML.Result resultDenominaciones = BL.Cambio.GetDineroDisponible();
                    ML.Cambio demoninaciones = (ML.Cambio)resultDenominaciones.Object;
                    ObtenerDenominaciones(cambio, demoninaciones);
                    //Validar que las denominaciones seleccionadas cubran el total del cambio a devolver.
                    if(cambio == (demoninaciones.Monedas10 * 10 + demoninaciones.Billetes50 * 50 + demoninaciones.Billetes100 * 100))
                    {
                        ML.Compra compra = new ML.Compra();
                        compra.Producto = new ML.Producto();
                        compra.Producto.IdProducto = idProducto;
                        compra.Ingreso = dineroIngresado;
                        compra.Cambio = cambio;
                        //Realizar la compra
                        ML.Result resultOperacion = BL.Compra.Add(compra);
                        if (resultOperacion.Correct)
                        {
                            ViewBag.Mensaje = $"Gracias por tu compra :) Tu cambio: {cambio}. \nMonedas de 10: {demoninaciones.Monedas10}. \nBilletes 50: {demoninaciones.Billetes50}. \nBilletes de 100: {demoninaciones.Billetes100}";
                        }
                        else
                        {
                            ViewBag.Mensaje = resultOperacion.Message;
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "Lo sentimos :( La maquina no cuenta con las denominaciones necesarias para dar tu cambio. Se te devolverá el dinero ingresado.";
                    }
                }
                else
                {
                    ML.Compra compra = new ML.Compra();
                    compra.Producto = new ML.Producto();
                    compra.Producto.IdProducto = idProducto;
                    compra.Ingreso = dineroIngresado;
                    compra.Cambio = cambio;
                    //Realizar la compra
                    ML.Result resultOperacion = BL.Compra.Add(compra);
                    if (resultOperacion.Correct)
                    {
                        ViewBag.Mensaje = "Gracias por su compra. Vuelva pronto :)";
                    }
                    else
                    {
                        ViewBag.Mensaje = resultOperacion.Message;
                    }
                }
            }
            else
            {
                ViewBag.Mensaje = "El dinero ingresado no es suficiente para cubrir el total de la compra :(";
            }
            return PartialView("Modal");
        }

        //Método para obtener las denominaciones del cambio a devolver.
        private ML.Cambio ObtenerDenominaciones(int cambioSaliente, ML.Cambio cambio)
        {
            Dictionary <int,int> monedasDisponibles = new Dictionary<int, int> 
            {
                { 100, cambio.Billetes100 },
                { 50, cambio.Billetes50 },
                { 10, cambio.Monedas10 }
            };

            List<int> denominaciones = new List<int>(monedasDisponibles.Keys);

            Dictionary<int, int> resultado = new Dictionary<int, int>
            {
                { 100, 0 },
                { 50, 0 },
                { 10, 0 }
            };

            foreach(var denominacion in denominaciones)
            {
                int cantidadDisponible = monedasDisponibles[denominacion];
                int cantidadMonedas = Math.Min(cambioSaliente / denominacion, cantidadDisponible);
                if (cantidadMonedas > 0)
                {
                    resultado[denominacion] += cantidadMonedas;
                    cambioSaliente -= cantidadMonedas * denominacion;
                    monedasDisponibles[denominacion] -= cantidadMonedas;

                    if(cambioSaliente == 0)
                    {
                        break;
                    }
                }
            }
            cambio.Monedas10 = resultado[10];
            cambio.Billetes50 = resultado[50];
            cambio.Billetes100 = resultado[100];
            return cambio;
        }
    }
}
