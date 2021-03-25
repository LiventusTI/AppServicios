//Este js restringe solo la vista Gestion de servicios para los perfiles que tienen acceso a la misma
var perfil = $("#perfil").val();

if (perfil == 1) { // PERFIL TECNICO
    $("#AgregarReserva").hide();
    $("#EditarReserva").hide();
    $("#EditarReservaNave").hide();
    $("#CancelarServicio").hide();
    $("#DescancelarServicio").hide();
    $("#EliminarServicio").hide();
    $("#HistoricoControladoresBoton").hide();
    $("#HistoricoValidacion").hide();
    $("#ValidacionServicio").hide();
    $("#GenerarGraficos").hide();
    $("#GraficoUnitario").hide();
}

if (perfil == 2) { // PERFIL SUPERVISOR LEAKTEST
    $("#AgregarReserva").hide();
    $("#EditarReserva").hide();
    $("#EditarReservaNave").hide();
    $("#CancelarServicio").hide();
    $("#DescancelarServicio").hide();
    $("#EliminarServicio").hide();
    $("#HistoricoValidacion").hide();
    $("#ValidacionServicio").hide();
    $("#GenerarGraficos").hide();
    $("#GraficoUnitario").hide();
}

if (perfil == 3) { // PERFIL ASISTENTE DE OPERACIONES
    $("#GenerarGraficos").hide();
    $("#GraficoUnitario").hide();
}

if (perfil == 4) { //PERFIL JEFE DE OPERACIONES
    $("#GenerarGraficos").hide();
    $("#GraficoUnitario").hide();
    $("#botonesaccionesFacturacion").hide();
}

if (perfil == 5) { // PERFIL COORDINADOR DE RETRIEVAL
    $("#AgregarServ").hide();
    $("#Cancelar").hide();
    $("#Modificaciones").hide();
    $("#GenerarGraficos").hide();
    $("#GraficoUnitario").hide();
}

if (perfil == 6) { // PERFIL SUPERVISOR DE RETRIEVAL
    $("#AgregarServ").hide();
    $("#Cancelar").hide();
    $("#Modificaciones").hide();
    $("#GenerarGraficos").hide();
    $("#GraficoUnitario").hide();
}

if (perfil == 7) { //PERFIL ABOGADO - NO HABILITADO
    $("#AgregarServ").hide();
    $("#Editar").hide();
    $("#Cancelar").hide();
}

if (perfil == 10) { //PERFIL FACTURADOR
    $("#AgregarServ").hide();
    $("#Editar").hide();
    $("#Cancelar").hide();
    $("#GenerarGraficos").hide();
    $("#GraficoUnitario").hide();
}

if (perfil == 11) { //PERFIL GERENTE SUPPLY CHAIN - NO HABILITADO
    $("#AgregarServ").hide();
    $("#Editar").hide();
    $("#Cancelar").hide();
}

if (perfil == 12) {  //PERFIL GERENTE LEGAL
    $("#AgregarServ").hide();
    $("#Editar").hide();
    $("#Cancelar").hide();
}

if (perfil == 13) { //PERFIL GERENTE FINANZAS
    $("#AgregarServ").hide();
    $("#Editar").hide();
    $("#Cancelar").hide();
}

if (perfil == 16) { //PERFIL NAVIERA
    $("#AgregarServ").hide();
    $("#Editar").hide();
    $("#Cancelar").hide();
    $("#Leaktest").hide();
    $("#Controladores").hide();
    $("#Modificaciones").hide();
    $("#GenerarGraficos").hide();
    $("#GraficoUnitario").hide();
}

if (perfil == 17) //PERFIL PROCESOS
{
    $("#AgregarReserva").hide();
    $("#EditarReserva").hide();
    $("#EditarReservaNave").hide();
    $("#CancelarServicio").hide();
    $("#DescancelarServicio").hide();
    $("#EliminarServicio").hide();
    $("#HistoricoControladoresBoton").hide();
    $("#HistoricoValidacion").hide();
    $("#ValidacionServicio").hide();
    $("#EditarServicioViaje").hide();
    $("#AgregarNuevoServicio").hide();
    $("#GenerarServiceReport").hide();
    $("#ValidarServicios").hide();
    $("#CambiosPendientesEDI").hide();
}

if (perfil == 18) {  //PERFIL GERENTE COMERCIAL
    $("#AgregarReserva").hide();
    $("#EditarReserva").hide();
    $("#EditarReservaNave").hide();
    $("#CancelarServicio").hide();
    $("#DescancelarServicio").hide();
    $("#EliminarServicio").hide();
    $("#HistoricoControladoresBoton").hide();
    $("#HistoricoValidacion").hide();
    $("#ValidacionServicio").hide();
    $("#EditarServicioViaje").hide();
    $("#AgregarNuevoServicio").hide();
    $("#GenerarServiceReport").hide();
    $("#ValidarServicios").hide();
    $("#CambiosPendientesEDI").hide();
}

if (perfil == 1024)//PERFIL COMERCIAL
{
    $("#AgregarReserva").hide();
    $("#EditarReserva").hide();
    $("#EditarReservaNave").hide();
    $("#CancelarServicio").hide();
    $("#DescancelarServicio").hide();
    $("#EliminarServicio").hide();
    $("#HistoricoControladoresBoton").hide();
    $("#HistoricoValidacion").hide();
    $("#ValidacionServicio").hide();
    $("#EditarServicioViaje").hide();
    $("#AgregarNuevoServicio").hide();
    $("#GenerarServiceReport").hide();
    $("#ValidarServicios").hide();
    $("#CambiosPendientesEDI").hide();
}

if (perfil == 1025)//GERENTE/JEFE PROCESOS
{
    $("#AgregarReserva").hide();
    $("#EditarReserva").hide();
    $("#EditarReservaNave").hide();
    $("#CancelarServicio").hide();
    $("#DescancelarServicio").hide();
    $("#EliminarServicio").hide();
    $("#HistoricoControladoresBoton").hide();
    $("#HistoricoValidacion").hide();
    $("#ValidacionServicio").hide();
    $("#EditarServicioViaje").hide();
    $("#AgregarNuevoServicio").hide();
    $("#GenerarServiceReport").hide();
    $("#ValidarServicios").hide();
    $("#CambiosPendientesEDI").hide();
}

if (perfil == 1027)//OPERACIONES LOGISTICA
{
    $("#ValidarServicios").hide();
    $("#ValidarServiciosAdmin").show();
}