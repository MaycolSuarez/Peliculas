using Microsoft.AspNetCore.Mvc;
using Peliculas.Models;
using System.Diagnostics;

using Peliculas.Servicios;

namespace Peliculas.Controllers
{
    public class HomeController : Controller
    {
        //Se instancia la interfaz necesaria pasa usar los metodos creados
        private readonly IServicioPeli _servicioPeli;

        //Se indica al controller que va usar los servicios de la instancia
        public HomeController(IServicioPeli servicioPeli)
        {
            _servicioPeli = servicioPeli;
        }
        //Vista del index, donde se listan las peliculas
        public IActionResult Index()
        {
            return View();
        }
        //Metodo Get para Listar las peliculas
        [HttpGet]
        public async Task<IActionResult> Listar(string nombre, int page)
        {
            ResultApiModify peliculas = await _servicioPeli.Listar(nombre,page);

            if (peliculas.totalResults != null) 
                return View("Index", peliculas);
            return View("Error");
        }
        //vista de Pelicula, donde se muestra a detalle la pelicula seleccionada al hacer click sobre su imagen
        public async Task<IActionResult> Pelicula(string idpeli)
        {   
            Pelicula peliculaModel = new Pelicula();    

            if (idpeli != "" || idpeli != null)
            {
                peliculaModel = await _servicioPeli.Detalle(idpeli);
            }
            return View(peliculaModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //vista de Error, para evitar que falle abruptamente la app
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}