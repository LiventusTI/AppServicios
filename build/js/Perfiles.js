// En este js se detallan las restricciones para cada uno de los perfiles disponibles en la App
var perfil = $("#perfil").val();

if (perfil == 1) { // PERFIL TECNICO
    //LEAKTEST
    $(".crearsolicitud").hide();
    $(".gestionarleaktestSP").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionarreservaSP").hide();
    $(".historicoserviciosSP").hide();
    $(".historicoserviciosNaviera").hide();
    $(".historicoservicios").hide();
    $(".historicoserviciosPorSemana").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    //DATOS
    $(".gestiondedatos").hide();
}
if (perfil == 2) { // PERFIL SUPERVISOR LEAKTEST
    //LEAKTEST
    $(".gestionarleaktestSP").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionarreservaSP").hide();
    $(".historicoserviciosSP").hide();
    $(".historicoserviciosNaviera").hide();
    $(".historicoservicios").hide();
    $(".historicoserviciosPorSemana").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    //DATOS
    $(".gestiondedatos").hide();
}
if (perfil == 3) { // PERFIL ASISTENTE DE OPERACIONES
    //LEAKTEST
    $(".gestionarleaktestSP").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".historicoserviciosNaviera").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    $("#PowerBI").show();
}
if (perfil == 4) { //PERFIL JEFE DE OPERACIONES
    //SERVICIOS
    $(".historicoserviciosNaviera").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".GestionarLogisticaControlador").hide();
    $(".GestionRetrievalControlador").hide();
    $(".GestionarMovimientoControlador").hide();
    $("#PowerBI").show();
}

if (perfil == 5) { // PERFIL COORDINADOR DE RETRIEVAL
    //LEAKTEST
    $(".gestionleaktest").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //SERVICIOS
    $(".gestionarreserva").hide();
    $(".gestionarreservaSP").hide();
    $(".historicoserviciosSP").hide();
    $(".historicoserviciosNaviera").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".GestionarLogisticaDeposito").hide();
}

if (perfil == 6) { // PERFIL SUPERVISOR DE RETRIEVAL
    //LEAKTEST
    $(".gestionleaktest").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //SERVICIOS
    $(".gestionarreservaSP").hide();
    $(".historicoserviciosSP").hide();
    $(".historicoserviciosNaviera").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".GestionarLogisticaDeposito").hide();
}

if (perfil == 7) { //PERFIL ABOGADO - NO HABILITADO
    $(".crearsolicitud").hide();
    $(".gestionarsolicitud").hide();
    $(".inventariocontenedores").hide();
    $(".disponiblescontenedores").hide();
    $(".crearreserva").hide();
}

if (perfil == 8) {  // PERFIL ENCARGADO DE LABORATORIO
    //LEAKTEST
    $(".gestionleaktest").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    //DATOS
    $(".gestiondedatos").hide();
}

if (perfil == 9) { //PERFIL INGENIERO DE SOPORTE LAB
    //LEAKTEST
    $(".gestionleaktest").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    //DATOS
    $(".gestiondedatos").hide();
    $("#PowerBI").show();
}

if (perfil == 10) { //PERFIL FACTURADOR
    //LEAKTEST
    $(".gestionleaktest").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionservicios").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    //DATOS
    $(".gestiondedatos").hide();
}

if (perfil == 11) { //PERFIL GERENTE SUPPLY CHAIN - NO HABILITADO
    $("#PowerBI").show();
}

if (perfil == 12) { //PERFIL GERENTE LEGAL
    //LEAKTEST
    $(".crearsolicitud").hide();
    $(".gestionarleaktestSP").hide();
    //SERVICIOS
    $(".historicoserviciosNaviera").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".comparacionservicios").hide();

    //FACTURACION
    $(".gestionfacturacion").hide();

    //DATOS
    $(".gestiondedatos").hide();
    $("#PowerBI").show();
}

if (perfil == 13) { //PERFIL GERENTE FINANZAS
    //LEAKTEST
    $(".crearsolicitud").hide();
    $(".gestionarleaktestSP").hide();
    //SERVICIOS
    $(".historicoserviciosNaviera").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".comparacionservicios").hide();
    $(".aprobacionlegal").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();

    //DATOS
    $(".gestiondedatos").hide();
    $("#PowerBI").show();
}

if (perfil == 14) { //PERFIL GERENTE LABORATORIO - NO HABILITADO
}

if (perfil == 15) { //PERFIL GERENTE GENERAL
    //LEAKTEST
    $(".crearsolicitud").hide();
    $(".gestionarleaktestSP").hide();
    //SERVICIOS
    $(".historicoserviciosNaviera").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".comparacionservicios").hide();
    $(".aprobacionlegal").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //DATOS
    $(".gestiondedatos").hide();
    $("#PowerBI").show();
}

if (perfil == 16) { //PERFIL NAVIERA
    $(".gestionleaktest").hide();
    $(".gestioncontenedores").hide();
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionarreservaSP").hide();
    $(".historicoserviciosSP").hide();
    $(".gestionarreserva").hide();
    $(".historicoservicios").hide();
    $(".historicoserviciosPorSemana").hide();
    $(".gestionardepositos").hide();
    $(".gestionardepositos2").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    $(".gestionbaterias").hide();
    

    $(".menulogistca").hide();
    $(".gestiondedatos").hide();
}

if (perfil == 17) {//PERFIL PROCESOS
    //LEAKTEST
    $(".gestionleaktest").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionarreservaSP").hide();
    $(".historicoserviciosSP").hide();
    $(".historicoserviciosNaviera").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    //DATOS
    $(".gestiondedatos").hide();
}

if (perfil == 18) { //PERFIL GERENTE COMERCIAL
    //LEAKTEST
    $(".crearsolicitud").hide();
    $(".gestionarleaktestSP").hide();
    //SERVICIOS
    $(".historicoserviciosNaviera").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".comparacionservicios").hide();
    $(".aprobacionlegal").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();

    //DATOS
    $(".gestiondedatos").hide();
    $("#PowerBI").show();
}

if (perfil == 19) { //PERFIL BODEGUERO
    //LEAKTEST
    $(".crearsolicitud").hide();
    $(".gestionarleaktestSP").hide();
    $(".gestionarsolicitud").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionarreservaSP").hide();
    $(".historicoserviciosSP").hide();
    $(".historicoserviciosNaviera").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".GestionRetrievalControlador").hide();
    $(".GestionarMovimientoControlador").hide();
    //DATOS
    $(".gestiondedatos").hide();

    $("#Tecnica").hide();
    $("#DMS").hide();
    $("#CRM").hide();
    $("#PowerBI").hide();
    $("#Planificacion").hide();
    $("#ALAB").hide();
    $("#botones").hide();
}

if (perfil == 1020)//PERFIL TECNICO SP
{
    //LEAKTEST
    $(".crearsolicitud").hide();
    $(".gestionarsolicitud").hide();
    $(".historicoresultados").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionarreserva").hide();
    $(".historicoservicios").hide();
    $(".historicoserviciosPorSemana").hide();
    $(".historicoserviciosNaviera").hide();
    $(".historicoserviciosPorSemana").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    //DATOS
    $(".gestiondedatos").hide();
}

if (perfil == 1021)//PERFIL SUPERVISOR SP
{
    //LEAKTEST
    $(".crearsolicitud").hide();
    $(".gestionarsolicitud").hide();
    $(".historicoresultados").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionarreserva").hide();
    $(".historicoservicios").hide();
    $(".historicoserviciosPorSemana").hide();
    $(".historicoserviciosNaviera").hide();
    $(".historicoserviciosPorSemana").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    //DATOS
    $(".gestiondedatos").hide();
}


if (perfil == 1022)//PERFIL ANALISTA SP
{
    //LEAKTEST
    $(".crearsolicitud").hide();
    $(".gestionarsolicitud").hide();
    $(".historicoresultados").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionarreserva").hide();
    $(".historicoservicios").hide();
    $(".historicoserviciosPorSemana").hide();
    $(".historicoserviciosNaviera").hide();
    $(".historicoserviciosPorSemana").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //DATOS
    $(".mantenedorantepuerto").hide();
    $(".mantenedorcourier").hide();
    $(".mantenedorfreightforwarder").hide();
    $(".mantenedorretrievalprovider").hide();
    $(".mantenedorserviceprovider").hide();
    $(".mantenedormaquinaria").hide();
    $(".mantenedorpais").hide();
    $(".mantenedornaviera").hide();
    $(".mantenedornaviera").hide();
    //LOGISTICA
    $(".menulogistca").hide();
}

if (perfil == 1023)//PERFIL DEPOSITO
{
    //LEAKTEST
    $(".gestionleaktest").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionarreserva").hide();
    $(".historicoservicios").hide();
    $(".historicoserviciosPorSemana").hide();
    $(".gestionarreservaSP").hide();
    $(".historicoserviciosSP").hide();
    $(".historicoserviciosNaviera").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    //DATOS
    $(".gestiondedatos").hide();
    //BATERIAS
    $(".gestionbaterias").hide();
     $(".gestionardepositos2").hide();
}

if (perfil == 1028)//PERFIL DEPOSITO 2
{
    //LEAKTEST
    $(".gestionleaktest").hide();
    //CONTENEDORES
    $(".gestioncontenedores").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionarreserva").hide();
    $(".historicoservicios").hide();
    $(".historicoserviciosPorSemana").hide();
    $(".gestionarreservaSP").hide();
    $(".historicoserviciosSP").hide();
    $(".historicoserviciosNaviera").hide();
    $(".gestionarreservaEdi").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    //DATOS
    $(".gestiondedatos").hide();
    //BATERIAS
    $(".gestionbaterias").hide();

      $(".gestionardepositos").hide();
}

if (perfil == 1024)//PERFIL COMERCIAL
{
    //LEAKTEST
    $(".gestionleaktest").hide();
    //CONTROLADORES
    $(".gestioncontroladores").hide();
    //SERVICIOS
    $(".gestionarreservaSP").hide();
    $(".historicoserviciosSP").hide();
    $(".historicoserviciosNaviera").hide();
    $(".gestionardepositos").hide();
    $(".gestionarreservaEdi").hide();
    $(".comparacionservicios").hide();
    $(".aprobacionlegal").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".menulogistca").hide();
    //DATOS
    $(".gestiondedatos").hide();
}

if (perfil == 1025)//EQUIPO PROCESOS
{
    $(".historicoserviciosNaviera").hide();
    $("#PowerBI").show();
    $(".gestionalertas").show();
}

if (perfil == 1026)//EQUIPO TI
{
    $(".historicoserviciosNaviera").hide();
    $("#PowerBI").show();
}

if (perfil == 1027)//OPERACIONESLOGISTICA
{
    $("#PowerBI").show();
    $(".historicoserviciosNaviera").hide();
    $(".aprobacionlegal").hide();
    $(".comparacionservicios").hide();
    //FACTURACION
    $(".gestionfacturacion").hide();
    //LOGISTICA
    $(".GestionRetrievalControlador").hide();
    $(".GestionarMovimientoControlador").hide();
} 


//if (perfil == 1020 || perfil == 1021 || perfil == 1022) {
//   $(".mantenedorantepuerto").hide();
//    $(".mantenedorcourier").hide();
//    $(".mantenedorfreightforwarder").hide();
//    $(".mantenedorretrievalprovider").hide();
//    $(".mantenedorserviceprovider").hide();
//    $(".mantenedormaquinaria").hide();
//    $(".mantenedorpais").hide();
//    $(".mantenedornaviera").hide();
//    $(".mantenedornaviera").hide();
//}

//if (perfil == 1020 || perfil == 1021) {
//    $(".gestiondedatos").hide();
//}

//if(perfil == 1023)
//{
//    $(".gestionservicios").hide();
//    $(".gestionleaktest").hide();
//    $(".gestioncontenedores").hide();
//    $(".gestionfacturacion").hide();
//    $(".gestionarreservaEdi").hide();
//    $(".gestionleaktest").hide();
//    $(".menuclaims").hide();
//    $(".gestiondedatos").hide();
//    $(".menulogistca").hide();
//    $(".menuestadistica").hide();
//    $(".gestionarreserva").hide();
//    $(".historicoservicios").hide();
//}
