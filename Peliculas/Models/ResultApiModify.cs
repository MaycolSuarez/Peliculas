namespace Peliculas.Models
{
    //Modelo de la respuesta del API mofificado totalResults en int para poder trabajar sin porblema desde javascript 
    public class ResultApiModify
    {
        public List<Pelicula>? Search { get; set; }
        public int? totalResults { get; set; }
        public string? Response { get; set; }
    }
}
