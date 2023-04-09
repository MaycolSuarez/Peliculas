namespace Peliculas.Models
{
    //Modelo de la respuesta del API
    public class ResultApi
    {
        public List<Pelicula>? Search { get; set; } 
        public string? totalResults { get; set; }
        public string? Response { get; set; }


    }
}
