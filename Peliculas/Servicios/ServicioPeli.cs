using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Peliculas.Models;

namespace Peliculas.Servicios
{
    public class ServicioPeli : IServicioPeli
    {
        //Variables de entorno necesarias para hacer las consultas al API
        private static string key;
        private static string baseUrl;
        
        //constructor para dar valor a las variables
        public ServicioPeli()
        {
            //el builder nos ayuda a tomar los valores declarados en el appsettings.json
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            key = builder.GetSection("ApiSettings:key").Value;
            baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        //Metodo usado para hacer la consulta de detalle de pelicula al API mediante un metodo get
        //@string idpeli = id de la pelicula dado por el API
        public async Task<Pelicula> Detalle(string idpeli)
        {
            Pelicula pelicula = new Pelicula();

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            //Se realiza la peticon al API
            var response = await cliente.GetAsync($"?apikey={key}&i={idpeli}");

            //Si todo va bien se retorna el json de la pelicula para mostrarse posteriormente
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Pelicula>(jsonResponse);
                pelicula = resultado;
            }

            return pelicula;

        }

        //Metodo usado para hacer la consulta de todas peliculas al API mediante un metodo get
        //@string nombre = palabra clave de la pelicula para buscar mediante el API
        //@int page = numero de pagina de la pelicua a buscar dentro del API
        public async Task<ResultApiModify> Listar(string nombre,int page)
        {
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);

            ResultApiModify peliculasModify = new ResultApiModify();
            try
            {   
                //Condicional para hacer peticion a paginas
                if (page > 1)
                {
                    //Se realiza la peticon al API
                    var response = await cliente.GetAsync($"?apikey={key}&s={nombre}&type=movie&page={page}");

                    //Si todo va bien se retorna el json del resultado(Lista de peliculas y totalResults) del API
                    //para mostrarse posteriormente
                    if (response.IsSuccessStatusCode)
                    {

                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var resultado = JsonConvert.DeserializeObject<ResultApi>(jsonResponse);

                        //Se convierte de ResultApi a ResultApiModify para trabajar mejor con los int
                        peliculasModify.totalResults = int.Parse(resultado.totalResults);
                        peliculasModify.Response = resultado.Response;
                        peliculasModify.Search = resultado.Search;
                    }
                }
                else
                {
                    //Se realiza la peticon al API
                    var response = await cliente.GetAsync($"?apikey={key}&s={nombre}&type=movie");

                    //Si todo va bien se retorna el json del resultado(Lista de peliculas y totalResults) del API
                    //para mostrarse posteriormente
                    if (response.IsSuccessStatusCode)
                    {

                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var resultado = JsonConvert.DeserializeObject<ResultApi>(jsonResponse);

                        //Se convierte de ResultApi a ResultApiModify para trabajar mejor con los int
                        peliculasModify.totalResults = int.Parse(resultado.totalResults);
                        peliculasModify.Response = resultado.Response;
                        peliculasModify.Search = resultado.Search;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return peliculasModify;
        }


    }
}
