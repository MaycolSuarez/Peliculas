using Peliculas.Models;

namespace Peliculas.Servicios
{
    //Interfaz usada para los Metodos que se usaran en las consultas
    public interface IServicioPeli
    {
        Task<ResultApiModify> Listar(string nombre, int page);
        Task<Pelicula> Detalle(string idpeli);
    }
}
