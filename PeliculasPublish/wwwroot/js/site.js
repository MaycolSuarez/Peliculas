// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//funcion mostrar el paginado dinamico
window.onload = function () {
    var idpeli = document.getElementById('idPelicula');
    var miElemento = document.getElementById('paginacion');
    if (idpeli) {
        miElemento.style.display = 'block';
    } 
};
//funcion para hacer el llamado al metodo que consulta todas las peliculas que hagan match con el nombre
function Busqueda() {

    var valorInput = document.getElementById('pelicula').value;
    if (valorInput == "") {
        location.reload(true);
    } else {
        document.cookie = "pelicula=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
        document.cookie = "pelicula=" + document.getElementById('pelicula').value;

        var url = '/Home/Listar?nombre=' + encodeURIComponent(valorInput);
        window.location.href = url;
    }
}

//funcion para hacer el llamado al metodo que consulta a detalle cada pelicula
//@idplei = id de la pelicual consultado desde el api.
function Detalle(idpeli) {

    var url = '/Home/Pelicula?idpeli=' + encodeURIComponent(idpeli);
    window.location.href = url;
}