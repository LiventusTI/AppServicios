using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plataforma.Models
{
    public class Clases
    {
        //public class ReservayServicio
        //{
        //    public int IdReserva { get; set; }
        //    public string Booking { get; set; }
        //    public string Naviera { get; set; }
        //    public string Viaje { get; set; }
        //    public string Exportador { get; set; }
        //    public DateTime FechaRegistro { get; set; }
        //    public DateTime FechaIniStacking { get; set; }
        //    public DateTime FechaFinStacking { get; set; }
        //    public DateTime Etd { get; set; }
        //    public DateTime Eta { get; set; }
        //    public string Commodity { get; set; }
        //    public string PuertoOrigen { get; set; }
        //    public string PuertoDestino { get; set; }
        //    public string SetPoint { get; set; }
        //    public int IdServicio { get; set; }
        //    public string Contenedor { get; set; }
        //    public string UltimaNave { get; set; }
        //    public string UltimoControlador { get; set; }
        //    public string Estado { get; set; }
        //    public string CortinaInstalada { get; set; }
        //    public string Gasificado { get; set; }
        //    public string Validado { get; set; }
        //    public DateTime FechaInstCortina { get; set; }
        //    public string TipoLugarCortina { get; set; }
        //    public string LugarCortina { get; set; }
        //    public int CantidadPurafil { get; set; }
        //    public DateTime FechaGasificacion { get; set; }
        //    public string TecnicoGasificador { get; set; }
        //    public string TipoLugarGasificacion { get; set; }
        //    public string LugarGasificacion { get; set; }
        //    public float CO2 { get; set; }
        //    public float O2 { get; set; }
        //    public string TratamientoCO2 { get; set; }
        //    public int CantidadFiltros { get; set; }
        //    public int CantidadCal { get; set; }
        //}

        public class ReservayServicio
        {
            public List<Clases.Reserva> Reserva = new List<Clases.Reserva>();
            public List<Clases.Servicio> Servicio = new List<Clases.Servicio>();
        }
        public class Reserva
        {
            public int Id { get; set; }
            public int IdNaviera { get; set; }
            public int IdPaisDestino { get; set; }
            public int IdCiudadDestino { get; set; }
            public int IdPuertoDestino { get; set; }
            public int IdPaisOrigen { get; set; }
            public int IdCiudadOrigen { get; set; }
            public int IdPuertoOrigen { get; set; }
            public int IdPaisExportador { get; set; }
            public int IdSetpoint { get; set; }
            public int IdCommodity { get; set; }
            public int IdExportador { get; set; }
            public int IdFreightForwarder { get; set; }
            public int IdDeadline { get; set; }
            public string Viaje { get; set; }
            public string Consignatario { get; set; }
            public int CantidadServicios { get; set; }
            public string Booking { get; set; }
            public int IdNave { get; set; }
            public DateTime? Eta { get; set; }
            public DateTime? Etd { get; set; }
            public DateTime? FechaIniStacking { get; set; }
            public DateTime? FechaFinStacking { get; set; }
            public string Deadline { get; set; }
            public string IdEstadoReserva { get; set; }
            public DateTime FechaRegistro { get; set; }
            public string Naviera { get; set; }
            public string PuertoDestino { get; set; }
            public string PuertoOrigen { get; set; }
            public string Setpoint { get; set; }
            public string Commodity { get; set; }
            public string Exportador { get; set; }
            public string FreightForwarder { get; set; }
            public string Nave { get; set; }
            public string PaisOrigen { get; set; }
            public string PaisDestino { get; set; }
            public string CiudadOrigen { get; set; }
            public string CiudadDestino { get; set; }
            public string PaisExportador { get; set; }
            public int Estado { get; set; }
            public int CO2 { get; set; }
            public int O2 { get; set; }
            public float Temperatura { get; set; }
            public string CommodityTecnica { get; set; }
            public float CO2Setpoint { get; set; }
            public float O2Setpoint { get; set; }
            public DateTime? EtaNave { get; set; }
            public string ServiceProvider { get; set; }
            public int IdServiceProvider { get; set; }
            public string Booking2 { get; set; }
            public int Servicios2 { get; set; }
            public string Booking3 { get; set; }
            public int Servicios3 { get; set; }
            public string Booking4 { get; set; }
            public int Servicios4 { get; set; }
            public string Booking5 { get; set; }
            public int Servicios5 { get; set; }
            public string Booking6 { get; set; }
            public int Servicios6 { get; set; }
            public string Booking7 { get; set; }
            public int Servicios7 { get; set; }
            public string Booking8 { get; set; }
            public int Servicios8 { get; set; }
            public string Booking9 { get; set; }
            public int Servicios9 { get; set; }
            public string Booking10 { get; set; }
            public int Servicios10 { get; set; }
            public string Booking11 { get; set; }
            public int Servicios11 { get; set; }
            public string Booking12 { get; set; }
            public int Servicios12 { get; set; }
            public string Booking13 { get; set; }
            public int Servicios13 { get; set; }
            public string Booking14 { get; set; }
            public int Servicios14 { get; set; }
            public string Booking15 { get; set; }
            public int Servicios15 { get; set; }
            public int Deposito { get; set; }
            public int LlevaModem { get; set; }
        }

        public class Notificaciones
        {
            public int IdRetrieval { get; set; }
            public string NombreRP { get; set; }
            public DateTime? FechaNotificacion { get; set; }
            public string CoordinadorRP { get; set; }
            public int CantidadControladores { get; set; }
            public string Naviera { get; set; }
            public string NodoRP { get; set; }
            public string TipoNotificacion { get; set; }
            public string EstadoNotificacion { get; set; }

        }

        public class ReservaEDI
        {
            public string TipoEdi { get; set; }
            public string AccionEdi { get; set; }
            public string Booking { get; set; }
            public string Exportador { get; set; }
            public string FreightForwarder { get; set; }
            public string Consignatario { get; set; }
            public string DepositoContenedor { get; set; }
            public string Terminal { get; set; }
            public string PuertoDestino { get; set; }
            public string PuertoOrigen { get; set; }
            public DateTime? Eta { get; set; }
            public DateTime? Etd { get; set; }
            public int CantidadContenedores { get; set; }
            public int IdNave { get; set; }
            public DateTime? FechaIniStacking { get; set; }
            public DateTime? FechaFinStacking { get; set; }
            public string Deadline { get; set; }
            public string IdEstadoReserva { get; set; }
            public DateTime FechaRegistro { get; set; }
            public string Naviera { get; set; }
            public string Setpoint { get; set; }
            public string Commodity { get; set; }
            public float CO2 { get; set; }
            public float O2 { get; set; }
            public float Temperatura { get; set; }
            public string CommodityTecnica { get; set; }
            public string Nave { get; set; }
            public string Viaje { get; set; }
            public List<string> Contenedores = new List<string>();
            public int Estado { get; set; }
            public string CodPuertoOrigen { get; set; }
            public string CodPuertoDestino { get; set; }
            public string NombreEDI { get; set; }
            public string Clausula { get; set; }
            public int ContenedorValido { get; set; }
            public string Contenedor { get; set; }
            public int IdReserva { get; set; }
        }
        public class Servicio
        {
            public int Id { get; set; }
            public int IdReserva { get; set; }
            public int IdContenedor { get; set; }
            public DateTime? Eta { get; set; }
            public int IdNave1 { get; set; }
            public int IdNave2 { get; set; }
            public int IdNave3 { get; set; }
            public DateTime? FechaCortina { get; set; }
            public int IdTipoLugarCortina { get; set; }
            public string TipoLugarCortina { get; set; }
            public int IdLugarCortina { get; set; }
            public string LugarCortina { get; set; }
            public int CantidadPurafil { get; set; }
            public string Controlador { get; set; }
            public int IdControlador { get; set; }
            public string Bateria { get; set; }
            public string HoraLlegada { get; set; }
            public string HoraSalida { get; set; }
            public DateTime? FechaInstControlador { get; set; }
            public int IdTipoLugarControlador { get; set; }
            public string TipoLugarControlador { get; set; }
            public int IdLugarInstControlador { get; set; }
            public string LugarInstControlador { get; set; }
            public int IdTecnicoInstalador { get; set; }
            public string TecnicoInstalador { get; set; }
            public float TemperaturaContenedor { get; set; }
            public int FolioServiceReport { get; set; }
            public int PrecintoSecurity { get; set; }
            public int Candado { get; set; }
            public DateTime? FechaGasificacion { get; set; }
            public int IdTipoLugarGasificacion { get; set; }
            public string TipoLugarGasificacion { get; set; }
            public int IdLugarGasificacion { get; set; }
            public string LugarGasificacion { get; set; }
            public int IdTecnicoGasificador { get; set; }
            public string TecnicoGasificador { get; set; }
            public int CO2 { get; set; }
            public int N2 { get; set; }
            public int IdTratamientoCO2 { get; set; }
            public string TratamientoCO2 { get; set; }
            public int Scrubber { get; set; }
            public int Cal { get; set; }
            public int IdEstadoServicio { get; set; }
            public int CortinaInstalada { get; set; }
            public int Gasificado { get; set; }
            public int Validado { get; set; }
            public string UltimaNave { get; set; }
            public string UltimoControlador { get; set; }
            public string Contenedor { get; set; }
            public string EstadoServicio { get; set; }
            public string N_CortinaInstalada { get; set; }
            public string N_Gasificado { get; set; }
            public string N_Validado { get; set; }
            public int CambioControlador { get; set; }
            public int Habilitado { get; set; }
            public int InstaladorCortina { get; set; }
            public int PuertoOrigen { get; set; }
            public string NombrePuertoOrigen { get; set; }
            public string NombrePaisOrigen { get; set; }
            public int PuertoDestino { get; set; }
            public string NombrePuertoDestino { get; set; }
            public string Consignatario { get; set; }
            public string NotaServicio { get; set; }
            public string NotaLogistica { get; set; }
            public string Nave2 { get; set; }
            public string Nave3 { get; set; }
            //CAMPOS DE RESERVA
            public string Booking { get; set; }
            public string Naviera { get; set; }
            public string Exportador { get; set; }
            public string Viaje { get; set; }
            public string Nave { get; set; }
            public string FiltrosScrubber { get; set; }
            //ADICIONALES
            public int IdPaisCortina { get; set; }
            public int IdCiudadCortina { get; set; }
            public int IdPaisControlador { get; set; }
            public int IdCiudadControlador { get; set; }
            public int IdPaisGasificacion { get; set; }
            public int IdCiudadGasificacion { get; set; }
            public int IdPaisSPControlador { get; set; }
            public int IdSPControlador { get; set; }
            public int IdPaisSPGasificacion { get; set; }
            public int IdSPGasificacion { get; set; }
            public string Usuario { get; set; }
            public string Selloperno1 { get; set; }
            public string Selloperno2 { get; set; }
            public string Sellotapa { get; set; }
            public string SelloPanel1 { get; set; }
            public string SelloPanel2 { get; set; }
            public string SelloSecurity { get; set; }
            public string ObservacionSellos { get; set; }
            public string Commodity { get; set; }
            public string Setpoint { get; set; }
            public string Instalador { get; set; }
            public DateTime? UltimaDescargaAppTecnica { get; set; }
            public string ServiceData { get; set; }
            public string Modem { get; set; }
            public DateTime? FechaInstModem { get; set; }
        }
        public class Naviera
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
        }
        public class Commodity
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
        }
        public class PuertoOrigen
        {
            public int Id { get; set; }
            public int IdCiudad { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
            public string NombreCiudad { get; set; }
            public int IdPais { get; set; }
            public string NombrePais { get; set; }
            public int IdContinente { get; set; }
            public string NombreContinente { get; set; }

        }
        public class PuertoDestino
        {
            public int Id { get; set; }
            public int IdCiudad { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
            public string NombreCiudad { get; set; }
            public int IdPais { get; set; }
            public string NombrePais { get; set; }
            public int IdContinente { get; set; }
            public string NombreContinente { get; set; }
            public string Latitud { get; set; }
            public string Longitud { get; set; }
            public string Radio { get; set; }

        }
        public class Ciudad
        {
            public int Id { get; set; }
            public int IdPais { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
            public string NombrePais { get; set; }
            public int IdContinente { get; set; }
            public string NombreContinente { get; set; }
        }
        public class Pais
        {
            public int Id { get; set; }
            public int IdContinente { get; set; }
            public string Nombre { get; set; }
            public string NombreContinente { get; set; }
            public int Activo { get; set; }
        }
        public class Continente
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
        }
        public class Deposito
        {
            public int Id { get; set; }
            public int IdCiudad { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
            public string NombreCiudad { get; set; }
            public string NombrePais { get; set; }
            public int IdPais { get; set; }
            public int IdContinente { get; set; }
            public string NombreContinente { get; set; }
            public string Latitud { get; set; }
            public string Longitud { get; set; }
            public string Radio { get; set; }
        }
        public class ReservaLeaktest
        {
            public int Id { get; set; }
            public int IdDeposito { get; set; }
            public string NombreDeposito { get; set; }
            public string NombreCiudad { get; set; }
            public string NombrePais { get; set; }
            public string TipoReserva { get; set; }
            public int IdNaviera { get; set; }
            public string NombreNaviera { get; set; }
            public DateTime? FechaEstimadaRealizacion { get; set; }
            public int IdEstado { get; set; }
            public DateTime FechaRegistro { get; set; }
            public string Hora { get; set; }
            public int CantidadScrubber { get; set; }
            public string Comentario { get; set; }
            public string NombreServiceProvider { get; set; }
        }
        public class EstadoReservaLeaktest
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
            public int Activo { get; set; }
        }
        public class Maquinaria
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
        }
        public class ResultadoLeaktest
        {
            public int Id { get; set; }
            public int IdReserva { get; set; }
            public int Estado { get; set; }
            public DateTime FechaRealizacion { get; set; }
            public string IdServiceProvider { get; set; }
            public string Tiempo { get; set; }
            public float Presion { get; set; }
            public string IdTecnico { get; set; }
            public string Contenedor { get; set; }
            public int AnoContenedor { get; set; }
            public string CajaContenedor { get; set; }
            public string IdMaquinaria { get; set; }
            public string Controlador { get; set; }
            public string Bateria { get; set; }
            public int Scrubber { get; set; }
            public string Selloperno1 { get; set; }
            public string Selloperno2 { get; set; }
            public string Sellotapa { get; set; }
            public int IdControlador { get; set; }
            public string Comentario { get; set; }
            public string Pais { get; set; }
            public string Deposito { get; set; }
            public int KitCortina { get; set; }
            public string TipoLugar { get; set; }
            public DateTime? FechaControlador { get; set; }
            public string Tecnico { get; set; }
            public string Modem { get; set; }
            public string BateriaModem { get; set; }
        }
        public class ReservaResultadoLeaktest
        {
            public int IdReserva { get; set; }
            public int IdResultado { get; set; }
            public string Deposito { get; set; }
            public string Naviera { get; set; }
            public DateTime FechaReserva { get; set; }
            public DateTime FechaRealizacion { get; set; }
            public string Maquinaria { get; set; }
            public string Contenedor { get; set; }
            public int EstadoResultado { get; set; }
            public string Tiempo { get; set; }
            public string Tecnico { get; set; }
            public string ServiceProvider { get; set; }
            public int Scrubber { get; set; }
        }
        public class EstadoResultadoLeaktest
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
            public int Activo { get; set; }
        }
        public class EstadoReserva
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
            public int Activo { get; set; }

        }
        public class EstadoServicio
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
            public int Activo { get; set; }

        }
        public class FreightForwarder
        {
            public int Id_FreightForwarder { get; set; }
            public string NombreFreightForwarder { get; set; }
            public int Estado { get; set; }
        }
        public class Setpoint
        {
            public int IdSetpoint { get; set; }
            public double O2 { get; set; }
            public double CO2 { get; set; }
            public int Activo { get; set; }
            public int IdaplicacionServicio { get; set; }
        }
        public class Claim
        {
            public int Id { get; set; }
            public int IdEstado { get; set; }
            public string CNT { get; set; }
            public string NotificacionJLT { get; set; }
            public string NotificacionOrion { get; set; }
            public string NotificacionGML { get; set; }
            public DateTime DOL { get; set; }
            public DateTime FechaRegistro { get; set; }
            public int ano { get; set; }
            public DateTime Vigencia { get; set; }
        }
        public class CierreClaim
        {
            public int Id { get; set; }
            public string Motivo { get; set; }
            public DateTime FechaCierre { get; set; }
            public string Descripcion { get; set; }
            public string Ubicacion { get; set; }
        }
        public class DetalleSiniestro
        {
            public int Id { get; set; }
            public int Monto { get; set; }
            public float MontoUSD { get; set; }
            public string Descripcion { get; set; }
            public string DescripcionShipping { get; set; }
            public string EstadoGML { get; set; }
        }
        public class EstadoClaim
        {
            public int Id { get; set; }
            public string Descripcion { get; set; }
            public int Activo { get; set; }
        }
        public class AntecedenteTecnico
        {
            public int Id { get; set; }
            public int ServiceReport { get; set; }
            public string Causa { get; set; }
        }
        public class Controlador
        {
            public int Id { get; set; }
            public int IdEstadoRecuperacion { get; set; }
            public string EstadoRecuperacion { get; set; }
            public int IdUltMovLogistico { get; set; }
            public int TransitoNodo { get; set; }
            public string TransitoNodoTxt { get; set; }
            public int Prioritario { get; set; }
            public string PrioritarioTxt { get; set; }
            public int Recuperado { get; set; }
            public string RecuperadoTxt { get; set; }
            public int Perdido { get; set; }
            public string PerdidoTxt { get; set; }
            public DateTime? FechaPerdida { get; set; }
            public string EstadoTecnico { get; set; }
            public string NumControlador { get; set; }
            public int DamageReport { get; set; }
            public string DamageReportTxt { get; set; }

        }
        public class MovimientoLogistico
        {
            public int Id { get; set; }
            public int IdControlador { get; set; }
            public int IdModem { get; set; }
            public string Modem { get; set; }
            public string Controlador { get; set; }
            public int IdContenedor { get; set; }
            public string Contenedor { get; set; }
            public string Booking { get; set; }
            public int TipoMovimiento { get; set; }
            public int IdTipoNodoOrigen { get; set; }
            public int IdNodoOrigen { get; set; }
            public int IdTipoNodoDestino { get; set; }
            public int IdNodoDestino { get; set; }
            public DateTime? Eta { get; set; }
            public string EtaString { get; set; }
            public string EtaVencido { get; set; }
            public int DiasEtaVencido { get; set; }
            public string NumeroEnvio { get; set; }
            public string EmpresaTransporte { get; set; }
            public int IdCourier { get; set; }
            public DateTime? FechaArribo { get; set; }
            public DateTime? FechaEnvio { get; set; }
            public int DiasEnNodo { get; set; }
            public string Nota { get; set; }
            public string ContinenteOrigen { get; set; }
            public string PaisOrigen { get; set; }
            public string CiudadOrigen { get; set; }
            public string ContinenteDestino { get; set; }
            public string PaisDestino { get; set; }
            public string CiudadDestino { get; set; }
            public int DiasEtr { get; set; }
            public string TipoNodoOrigen { get; set; }
            public string NodoOrigen { get; set; }
            public string TipoNodoDestino { get; set; }
            public string NodoDestino { get; set; }
            public string RetrievalProvider { get; set; }
            public string Nave { get; set; }
            public int IdNave { get; set; }
            public string Viaje { get; set; }
            public string Naviera { get; set; }
            public DateTime? FechaRecuperacion { get; set; }
            public int Validado { get; set; }
            public string Etastring { get; set; }
            public int Involucrados { get; set; }
            public string UsuarioMovimiento { get; set; }
            public string UsuarioRecuperacion { get; set; }
            public int Prioridad { get; set; }
            public string Estado { get; set; }
            public string Prioritario { get; set; }
            public DateTime? FechaUltAccLog { get; set; }
            public string Bateria { get; set; }
            public string MensajesErrores { get; set; }

        }
        public class LogisticaControlador
        {
            public List<Clases.Controlador> Controladores = new List<Clases.Controlador>();
            public List<Clases.MovimientoLogistico> MovLogisticos = new List<Clases.MovimientoLogistico>();
            public List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            public List<Clases.RecuperacionControlador> Recuperaciones = new List<Clases.RecuperacionControlador>();

        }
        public class RecuperacionControlador
        {
            public int Id { get; set; }
            public int IdControlador { get; set; }
            public string Controlador { get; set; }
            public int IdRetrievalProvider { get; set; }
            public string RetrievalProvider { get; set; }
            public int NumNotificaciones { get; set; }
            public string RetrievalProviderNoti { get; set; }
            public DateTime? FechaNotiRP { get; set; }
            public string FechaNotiVencida { get; set; }
            public DateTime? FechaRecordaRP { get; set; }
            public int ReintentoRecupera { get; set; }
            public DateTime? FechaRecuperacion { get; set; }
            public string Nota { get; set; }
            public DateTime? Etr { get; set; }
            public string EtrVencido { get; set; }
            public int DiasEtrVencido { get; set; }
            public int TipoNodoRecupera { get; set; }
            public int NodoRecupera { get; set; }
            public DateTime? FechaNotificacionReal { get; set; }
            public int IdModem { get; set; }
            public string Modem { get; set; }
            public int Involucrados { get; set; }

        }
        //public class SalidaControlador
        //{
        //    public int Id { get; set; }
        //    public int IdControlador { get; set; }
        //    public int IdEstadoLogControlador { get; set; }
        //    public int IdRetrievalProvider { get; set; }
        //    public int IdCoordinadorRetrieval { get; set; }
        //    public DateTime FechaRecuperacion { get; set; }

        //    public string Origen { get; set; }
        //    public string Destino { get; set; }
        //    public string Descripcion { get; set; }
        //    public DateTime FechaSalida { get; set; }
        //    public int DiasViaje { get; set; }
        //    public string Courier { get; set; }
        //    public string TicketEnvio { get; set; }
        //    public DateTime ETA { get; set; }
        //}
        //public class EntradaControlador
        //{
        //    public int Id { get; set; }
        //    public int IdControlador { get; set; }
        //    public int IdEstadoLogControlador { get; set; }
        //    public string Origen { get; set; }
        //    public string Destino { get; set; }
        //    public string Descripcion { get; set; }
        //    public DateTime FechaEntrada { get; set; }
        //    public int DiasViaje { get; set; }
        //    public string Courier { get; set; }
        //    public string TicketEnvio { get; set; }
        //    public DateTime ETA { get; set; }
        //}
        public class Auditoria
        {
            public int Id { get; set; }
            public int IdReferencia { get; set; }
            public string Usuario { get; set; }
            public DateTime Fecha { get; set; }
            public string Operacion { get; set; }
            public string Campo { get; set; }
        }
        public class AuditoriaServicio
        {
            public int Id { get; set; }
            public int IdReferencia { get; set; }
            public string Usuario { get; set; }
            public DateTime Fecha { get; set; }
            public string Operacion { get; set; }
            public string EstadoServicio { get; set; }
            public string Contenedor { get; set; }
            public string Controlador { get; set; }
            public string Tratamiento { get; set; }
            public int DetalleCortina { get; set; }
            public int DetalleGasificacion { get; set; }
            public string PuertoOrigen { get; set; }
            public string PuertoDestino { get; set; }
            public string Consignatario { get; set; }
            public string Viaje { get; set; }
            public int CortinaInstalada { get; set; }
            public int Gasificado { get; set; }
            public int Habilitado { get; set; }
            public int Validado { get; set; }
            public int Temperatura { get; set; }
            public string Candado { get; set; }
            public int CantidadFiltros { get; set; }
            public int CantidadCal { get; set; }
            public DateTime Eta { get; set; }
            public string UltimaNave { get; set; }
            public string UltimoControlador { get; set; }
            public string HoraLlegada { get; set; }
            public string HoraSalida { get; set; }
            public string SelloPerno1 { get; set; }
            public string SelloPerno2 { get; set; }
            public string SelloTapa { get; set; }
            public string SelloPanel1 { get; set; }
            public string SelloPanel2 { get; set; }
            public string SelloServicio { get; set; }

        }
        public class AuditoriaReserva
        {
            public int Id { get; set; }
            public int IdReferencia { get; set; }
            public string Usuario { get; set; }
            public DateTime Fecha { get; set; }
            public string Operacion { get; set; }
            public string PuertoDestino { get; set; }
            public string PuertoOrigen { get; set; }
            public string Setpoint { get; set; }
            public string Commodity { get; set; }
            public string Freightforwarder { get; set; }
            public string Booking { get; set; }
            public string Viaje { get; set; }
            public string Consignatario { get; set; }
            public int Cantidadservicios { get; set; }
            public DateTime Eta { get; set; }
            public string Nave { get; set; }
            public DateTime Etd { get; set; }
            public DateTime Fechainistacking { get; set; }
            public DateTime Fechafinstacking { get; set; }
            public string Exportador { get; set; }
            public int Estado { get; set; }



        }
        public class ServiceProvider
        {
            public int IdServiceProvider { get; set; }
            public int IdPais { get; set; }
            public string NombreServiceProvider { get; set; }
            public int Activo { get; set; }
            public string NombrePais { get; set; }
        }
        public class RegistroEstadoReservaLeaktest
        {
            public int Id { get; set; }
            public int IdEstadoReserva { get; set; }
            public int IdReserva { get; set; }
            public DateTime Fecha { get; set; }
        }
        public class RegistroEstadoResultadoLeaktest
        {
            public int Id { get; set; }
            public int IdEstadoResultado { get; set; }
            public int IdResultado { get; set; }
            public DateTime Fecha { get; set; }
        }
        public class RegistroEstadoServicio
        {
            public int Id { get; set; }
            public int IdEstadoServicio { get; set; }
            public int IdServicio { get; set; }
            public DateTime Fecha { get; set; }
        }
        public class Perfil
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public int Activo { get; set; }
        }
        public class Usuario
        {
            public string NombreUsuario { get; set; }
            public int IdUsuario { get; set; }
            public string Contrasena { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int IdPerfil { get; set; }
            public string Correo { get; set; }
            public int IdServiceProvider { get; set; }
        }
        public class Validar
        {
            public string Mensaje { get; set; }
            public Int32 validador { get; set; }
            public string tipo_mensaje { get; set; }
            public string titulo_mensaje { get; set; }

        }
        public class DeadLine
        {

            public int Id { get; set; }
            public int IdPais { get; set; }
            public string Descripcion { get; set; }
            public int DiasLimite { get; set; }
            public int Activo { get; set; }
            public string NombrePais { get; set; }
        }
        public class ReservaLeaktestCompleta
        {

            public List<ReservaLeaktest> Reservas = new List<ReservaLeaktest>();
            public List<Int32> CantidadTotal = new List<Int32>();
            public List<Int32> CantidadPorContenedores = new List<Int32>();
            public List<Int32> Ralizados = new List<Int32>();
            public List<string> Estado = new List<string>();

        }
        public class DetalleReservaLeaktest
        {
            public List<string> NombreMaquinaria = new List<string>();
            public List<int> Cantidad = new List<int>();
            public List<int> Realizados = new List<int>();
            public List<int> IdReserva = new List<int>();
            public List<int> Aprobados = new List<int>();
            public List<int> Rechazados = new List<int>();
            public string Motivo { get; set; }
            public int cantidadscrubber { get; set; }
        }
        public class Courier
        {
            public int Id_Courier { get; set; }
            public string NombreCourier { get; set; }
            public int Estado { get; set; }
        }
        public class Exportador
        {
            public int Id_Exportador { get; set; }
            public string NombreExportador { get; set; }
            public int Estado { get; set; }
            public int IdPais { get; set; }
            public string NombrePais { get; set; }
        }
        public class Tecnico
        {
            public int IdTecnico { get; set; }
            public string NombreTecnico { get; set; }
            public int IdPaisSP { get; set; }
            public int IdSP { get; set; }
            public string NombrePaisSP { get; set; }
            public string NombreSP { get; set; }


        }
        public class ResultadosLeaktest
        {
            public List<ResultadoLeaktest> Resultados = new List<ResultadoLeaktest>();
        }
        public class Comentarios
        {
            public int IdComentario { get; set; }
            public string descripcion { get; set; }
            public DateTime Fecha { get; set; }
        }
        public class Archivos
        {
            public int IdArchivo { get; set; }
            public string descripcion { get; set; }
            public DateTime Fecha { get; set; }
        }
        public class ListaComentarios
        {
            public List<Comentarios> listacomentarios = new List<Comentarios>();
        }
        public class ListaArchivos
        {
            public List<Archivos> listaarchivos = new List<Archivos>();
        }
        public class DetalleReservaLeaktestEditar
        {
            public int IdNaviera { get; set; }
            public int IdDeposito { get; set; }
            public DateTime FechaEstimada { get; set; }
            public int IdTipoReserva { get; set; }
            public int IdMaquinaria { get; set; }
            public int Cantidad { get; set; }
            public int Realizados { get; set; }
            public int Aprobados { get; set; }
            public string Hora { get; set; }
            public int CantidadScrubber { get; set; }
        }
        public class ResultadoLeaktestAll
        {
            public int IdLeaktest { get; set; }
            public int IdReserva { get; set; }
            public string NombrePais { get; set; }
            public string NombreCiudad { get; set; }
            public string NombreDeposito { get; set; }
            public string NombreMaquinaria { get; set; }
            public string NombreServiceProvider { get; set; }
            public string NombreUsuario { get; set; }
            public string Tiempo { get; set; }
            public string Controlador { get; set; }
            public string Bateria { get; set; }
            public string Contenedor { get; set; }
            public int Ano { get; set; }
            public string NombreCaja { get; set; }
            public DateTime Fecha { get; set; }
            public int Estado { get; set; }
            public int Scrubber { get; set; }
            public string Selloperno1 { get; set; }
            public string Selloperno2 { get; set; }
            public string Sellotapa { get; set; }
            public string Naviera { get; set; }
            public int KitCortina { get; set; }
            public string Modem { get; set; }
            public string BateriaModem { get; set; }

        }

        public class ResultadoLeaktestAllSP
        {
            public int IdResultado { get; set; }
            public int IdReserva { get; set; }
            public string NombrePais { get; set; }
            public string NombreCiudad { get; set; }
            public string NombreDeposito { get; set; }
            public string NombreMaquinaria { get; set; }
            public string NombreServiceProvider { get; set; }
            public string NombreUsuario { get; set; }
            public string Tiempo { get; set; }
            public string Contenedor { get; set; }
            public string Ano { get; set; }
            public DateTime? FechaRealizacion { get; set; }
            public string Selloperno1 { get; set; }
            public string Selloperno2 { get; set; }
            public string Sellotapa { get; set; }
            public string Naviera { get; set; }
            public DateTime? FechaEstimada { get; set; }
            public string HoraEstimada { get; set; }
            public int Estado { get; set; }
            public DateTime FechaRegistro { get; set; }
            public string Hora { get; set; }
            public string Commodity { get; set; }
            public string Checkbox { get; set; }
            public string Comentario { get; set; }
            public string EstadoLeaktest { get; set; }
            public string Validado { get; set; }
            public string PanelReutilizado { get; set; }
            public DateTime? FechaAccion { get; set; }
            public string UsuarioAccion { get; set; }
            public int IdControlador { get; set; }
            public string Controlador { get; set; }
            public string Bateria { get; set; }
            public DateTime? FechaControlador { get; set; }
            public int PreValidacionTecnica { get; set; }
            public string Modem { get; set; }
            public string BateriaModem { get; set; }
            public DateTime? FechaBateriaModem { get; set; }
        }
        public class Nave
        {
            public Int32 IdNave { get; set; }
            public Int32 IdNaviera { get; set; }
            public string NombreNaviera { get; set; }
            public string NombreNave { get; set; }
            public Int32 Estado { get; set; }

        }
        public class AntePuerto
        {
            public int Id { get; set; }
            public int IdCiudad { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
            public string NombreCiudad { get; set; }
            public int IdPais { get; set; }
            public string NombrePais { get; set; }
            public int IdContinente { get; set; }
            public string NombreContinente { get; set; }
            public string Latitud { get; set; }
            public string Longitud { get; set; }
            public string Radio { get; set; }
        }

        public class Terminal
        {
            public int Id { get; set; }
            public int IdCiudad { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
            public string NombreCiudad { get; set; }
            public int IdPais { get; set; }
            public string NombrePais { get; set; }
            public int IdContinente { get; set; }
            public string NombreContinente { get; set; }
            public string Latitud { get; set; }
            public string Longitud { get; set; }
            public string Radio { get; set; }
        }
        public class Packing
        {
            public int Id { get; set; }
            public int IdCiudad { get; set; }
            public int IdExportador { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
            public string NombreCiudad { get; set; }
            public string NombreExportador { get; set; }
            public int IdPais { get; set; }
            public string NombrePais { get; set; }
            public int IdContinente { get; set; }
            public string NombreContinente { get; set; }
            public string Latitud { get; set; }
            public string Longitud { get; set; }
            public string Radio { get; set; }
        }
        public class RetrievalProvider
        {
            public int IdRetrievalProvider { get; set; }
            public string NombreRetrievalProvider { get; set; }
            public int Activo { get; set; }
            public int IdPais { get; set; }
            public string NombrePais { get; set; }
            public string Correo { get; set; }
            public string Correo1 { get; set; }
            public string Correo2 { get; set; }
            public string Telefono { get; set; }
            public string Telefono1 { get; set; }
            public string Telefono2 { get; set; }
            public int Diaprearribo { get; set; }
            public int Diaprearribo1 { get; set; }
            public int Diaprearribo2 { get; set; }
            public int Diapostarribo { get; set; }
            public int IdNodo { get; set; }
            public int IdNaviera { get; set; }

        }
        public class Contenedor
        {
            public int IdContenedor { get; set; }
            public string NumeroContenedor { get; set; }
            public string NombreMaquinaria { get; set; }
            public string CajaContenedor { get; set; }
            public int AnoContenedor { get; set; }
            public string Pais { get; set; }
            public string Ciudad { get; set; }
            public string Deposito { get; set; }
            public string Naviera { get; set; }
            public int IdReserva { get; set; }
            public DateTime? Fecha { get; set; }
            public string Estado { get; set; }
            public int Scrubber { get; set; }
            public int EstadoContenedor { get; set; }
            public int IdMaquinaria { get; set; }
            public int IdCaja { get; set; }
            public int KitCortina { get; set; }
            public string Controlador { get; set; }
            public string Bateria { get; set; }
            public DateTime? FechaBateria { get; set; }
            public int DiasBateria { get; set; }
            public string DetalleAlerta { get; set; }

            public int CONT_DIAS_TRAVELING_CONTROLADOR { get; set; }
            public int CONT_DIAS_SLEEP_CONTROLADOR { get; set; }
            public int CONT_DIAS_TRAVELING_MODEM { get; set; }
            public int CONT_DIAS_SLEEP_MODEM { get; set; }
            public string Gestion { get; set; }
            public DateTime? FechaGestion { get; set; }
            public string Modem { get; set; }
            public DateTime? FechaBateriaModem { get; set; }
            public string BateriaModem { get; set; }
            public string EstadoOperacionControlador { get; set; }
            public string EstadoOperacionModem { get; set; }
        }

        public class Bateria
        {
            public string Checkbox { get; set; }
            public int IdBateria { get; set; }
            public string NumBateria { get; set; }
            public string UsuarioPrueba { get; set; }
            public string Estado { get; set; }
            public string TipoESN { get; set; }
            public string ESN { get; set; }
            public string Controlador { get; set; }
            public string Modem { get; set; }
            public DateTime? FechaAsociacion { get; set; }
            public int DiasInstalacion { get; set; }
            public float TensionVacio { get; set; }
            public int Valor { get; set; }
            public float TensionCarga { get; set; }
            public float DiferenciaVoltaje { get; set; }
            public string Resultado { get; set; }
            public string Modelo { get; set; }
            public string UsuarioAsociacion { get; set; }
            public int CONT_DIAS_TRAVELING { get; set; }
            public int CONT_DIAS_SLEEP { get; set; }
        }
        public class TipoLugar
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Estado { get; set; }
        }
        public class TratamientoCO2
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Estado { get; set; }
        }
        public class DetalleControlador
        {
            public int Id { get; set; }
            public int IdServicio { get; set; }
            public int IdControlador { get; set; }
            public DateTime? FechaInstalacion { get; set; }
            public int IdTipoLugar { get; set; }
            public int IdLugarControlador { get; set; }
            public int IdTecnico { get; set; }
            public string Bateria { get; set; }
            public string TipoLugar { get; set; }
            public string LugarControlador { get; set; }
            public string Tecnico { get; set; }
            public string Controlador { get; set; }

        }
        public class CantidadReservas
        {

            public string Booking { get; set; }
            public int TotalServicios { get; set; }
            public int Reservados { get; set; }
            public int EnViaje { get; set; }
            public int Cancelados { get; set; }
            public int NoRecuperados { get; set; }
            public int Recuperados { get; set; }
            public int EnLab { get; set; }

        }
        public class DetalleContenedor
        {
            public int IdReserva { get; set; }
            public string NombreMaquinaria { get; set; }
            public int IdContenedor { get; set; }
            public string Contenedor { get; set; }
            public int IdEstado { get; set; }
            public int IdMaquinaria { get; set; }
            public int CantidadScrubber { get; set; }
        }
        public class TipoSolicitudLeaktest
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
        }
        public class Bodega
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int IdCiudad { get; set; }
            public string NombreCiudad { get; set; }
            public int IdPais { get; set; }
            public string NombrePais { get; set; }
            public int IdContinente { get; set; }
            public string NombreContinente { get; set; }
            public int Activo { get; set; }
            public string Latitud { get; set; }
            public string Longitud { get; set; }
            public string Radio { get; set; }
        }
        public class AsociacionNavieraRetrieval
        {
            public int IdNaviera { get; set; }
            public int IdRetrievalProvider { get; set; }
        }
        public class AsociacionNodoRetrieval
        {
            public int IdTipoLugar { get; set; }
            public int IdNodo { get; set; }
            public int Dias { get; set; }
            public string NombreTipoLugar { get; set; }
            public string NombreNodo { get; set; }
            public int IdRetrieval { get; set; }
        }
        public class InvetarioContenedor
        {
            public int CantidadContenedores { get; set; }
            public int CantidadScrubber { get; set; }
            public List<string> Navieras = new List<string>();
            public List<int> Contenedores = new List<int>();
            public List<int> ContenedoresWS = new List<int>();
        }
        public class ProgramacionServicio
        {
            public int IdRegistro { get; set; }
            public DateTime FechaEstimada { get; set; }
            public string HoraEstimada { get; set; }
            public string TipoServicio { get; set; }
            public string TipoLugar { get; set; }
            public int IdTipoLugar { get; set; }
            public string Lugar { get; set; }
            public int IdLugar { get; set; }
            public string Contenedor { get; set; }
            public int IdContenedor { get; set; }
        }

        public class ServicioCompleto
        {
            public int IdReserva { get; set; }
            public string Booking { get; set; }
            public DateTime FechaRegistro { get; set; }
            public string Viaje { get; set; }
            public string Consignatario { get; set; }
            public string Usuario { get; set; }
            public DateTime? EtaPuerto { get; set; }
            public DateTime? EtaNave { get; set; }
            public DateTime? Etd { get; set; }
            public DateTime? FechaIniStacking { get; set; }
            public DateTime? FechaFinStacking { get; set; }
            public string Temperatura { get; set; }
            public int IdServicio { get; set; }
            public string Naviera { get; set; }
            public string PuertoDestino { get; set; }
            public string PuertoOrigen { get; set; }
            public string Commodity { get; set; }
            public string Setpoint { get; set; }
            public string Freightforwarder { get; set; }
            public string FreightforwarderEdi { get; set; }
            public string PaisExportador { get; set; }
            public string Exportador { get; set; }
            public string ExportadorEdi { get; set; }
            public string EstadoServicio { get; set; }
            public string Contenedor { get; set; }
            public string Controlador { get; set; }
            public string Nave1 { get; set; }
            public string Nave2 { get; set; }
            public string Nave3 { get; set; }
            public string TratamientoCo2 { get; set; }
            public string TipoLugarCortina { get; set; }
            public string LugarCortina { get; set; }
            public int PurafilCortina { get; set; }
            public DateTime? FechaCortina { get; set; }
            public string TecnicoCortina { get; set; }
            public string TipoLugarGasificacion { get; set; }
            public string LugarGasificacion { get; set; }
            public string TecnicoGasificacion { get; set; }
            public DateTime? FechaGasificacion { get; set; }
            public string Co2Gasificacion { get; set; }
            public string N2Gasificacion { get; set; }
            public int Habilitado { get; set; }
            public DateTime? FechaControlador { get; set; }
            public string TipoLugarControlador { get; set; }
            public string LugarControlador { get; set; }
            public string TecnicoControlador { get; set; }
            public string Bateria { get; set; }
            public int Validado { get; set; }
            public string PrecintoSecurity { get; set; }
            public string Candado { get; set; }
            public int Scrubber { get; set; }
            public string FiltroScrubber { get; set; }
            public int CantidadCalScrubber { get; set; }
            public string Horallegada { get; set; }
            public string HoraSalida { get; set; }
            public string NotaServicio { get; set; }
            public string NotasLogistica { get; set; }
            public string SelloPerno1 { get; set; }
            public string SelloPerno2 { get; set; }
            public string SelloTapa { get; set; }
            public string SelloPanel1 { get; set; }
            public string SelloPanel2 { get; set; }
            public string SelloSecurity { get; set; }
            public string ObservacionSellos { get; set; }
            public int Claim { get; set; }
            public string Motivo { get; set; }
            public string CommodityTecnica { get; set; }
            public DateTime? FechaCancelacion { get; set; }
            public string ContinenteDestino { get; set; }
            public string ContinenteOrigen { get; set; }
            public string Viaje2 { get; set; }
            public string Viaje3 { get; set; }
            public string PaisCortina { get; set; }
            public string PaisDeposito { get; set; }
            public string CiudadDeposito { get; set; }
            public string Deposito { get; set; }
            public string Checkbox { get; set; }
            public int Semana { get; set; }
            public string Validacion { get; set; }
            public string Habilitacion { get; set; }
            public string UltimaNave { get; set; }
            public string ServiceProvider { get; set; }
            public string PaisPuertoOrigen { get; set; }
            public string MovContenedor { get; set; }
            public int Alerta { get; set; }
            public int ValidacionServicio { get; set; }
            public int IdNaviera { get; set; }
            public int IdPuertoDestino { get; set; }
            public int Traspasado { get; set; }
            public string ColorEstado { get; set; }
            public int ValidacionTecnica { get; set; }
            public string GestionServicio { get; set; }
            public DateTime? FechaGestion { get; set; }
            public string Modem { get; set; }
            public int IdModem { get; set; }
            public DateTime? FechaPreZarpe { get; set; }
            public int ValidadoPreZarpe { get; set; }
            public int PosContenedorNave { get; set; }

            //campos legal//
            public DateTime? FechaAprobacion { get; set; }
            public int EstadoAprobacion { get; set; }

            //campos deposito
            public int EsDeposito { get; set; }
            public int NumeroOrdenCarga { get; set; }
            public string TipoEntrega { get; set; }
            public string Comentario { get; set; }
            public int ValidadoDeposito { get; set; }
            public int ContenedorConLeaktest { get; set; }
            //Cambio en proceso modem
            public int LlevaModem { get; set; }
            public DateTime? FechaModem { get; set; }
            public string TipoLugarModem { get; set; }
            public string LugarModem { get; set; }
            public string TecnicoModem { get; set; }
            public string DetalleAlerta { get; set; }
            //AsociacionBateria
            public DateTime? FechaAsociacionBateriaControlador { get; set; }
            public DateTime? FechaAsociacionBateriaModem { get; set; }
            public string BateriaModem { get; set; }
            //Sensores
            public string Sensor { get; set; }
            public int IdContenedor { get; set; }
            public int IdControlador { get; set; }

            //Nuevos Deposito
            public DateTime? FechaContenedor { get; set; }
            public string ControladorPorDeposito { get; set; }
            //Auditoria
            public string Accion { get; set; }
            public DateTime? FechaAccion { get; set; }
            public string UsuarioAccion { get; set; }
        }

        public class Validacion
        {
            public string controlador { get; set; }
            public string Controladoraux { get; set; }
            public string modem { get; set; }
            public string Modemaux { get; set; }
            public int IdServicio { get; set; }
            public int IdCommodity { get; set; }
            public string Commodityaux { get; set; }
            public string Contenedor { get; set; }
            public string Contenedoraux { get; set; }
            public DateTime FechaInicioServicio { get; set; }
            public string FechaInicioServicioaux { get; set; }
            public string Booking { get; set; }
            public string Bookingaux { get; set; }
            public int Validado { get; set; }
            public string ServiceData { get; set; }
            public int IdResultado { get; set; }
            public string Bateriaaux { get; set; }
            public string Alertaaux { get; set; }
            public string Gasificadoaux { get; set; }
            public string DamageReportaux { get; set; }
            public string AsociacionModemControladoraux { get; set; }
            public string ConexionModemAppTecnicaaux { get; set; }
            public string EstadoControladoraux { get; set; }
            public string EstadoModemaux { get; set; }
            public string ControladorEnergizadoaux { get; set; }
            public string Scrubber { get; set; }
        }


        public class HistoricoControlador
        {
            public string NumControlador { get; set; }
            public int IdControlador { get; set; }
            public string TipoMovimiento { get; set; }
            public string TipoLugarOrigen { get; set; }
            public string LugarOrigen { get; set; }
            public string TipoLugarDestino { get; set; }
            public string LugarDestino { get; set; }
            public string TipoLugarRecuperacion { get; set; }
            public string LugarRecuperacion { get; set; }
            public DateTime? FechaEnvio { get; set; }
            public DateTime? FechaEntrada { get; set; }
            public DateTime? FechaRecuperacion { get; set; }
            public DateTime? Fecha { get; set; }
            public int IdMovimiento { get; set; }
            public int IdModem { get; set; }
            public string Modem { get; set; }
        }

        public class Controladores
        {
            public int IdControlador { get; set; }
            public int IdModem { get; set; }
            public string EstadoRecuperacion { get; set; }
            public string TransitoNodoTxt { get; set; }
            public string PrioritarioTxt { get; set; }
            public string ComentarioPerdida { get; set; }
            public string RecuperadoTxt { get; set; }
            public string PerdidoTxt { get; set; }
            public DateTime? FechaPerdida { get; set; }
            public string NumControlador { get; set; }
            public string NumModem { get; set; }
            public string DamageReportTxt { get; set; }

            //MOV LOGISTICO
            public string Nota { get; set; }
            public DateTime? Eta { get; set; }
            public string NumeroEnvio { get; set; }
            public string EmpresaTransporte { get; set; }
            public DateTime? FechaArribo { get; set; }
            public int DiasEnNodo { get; set; }
            public string ContinenteOrigen { get; set; }
            public string PaisOrigen { get; set; }
            public string CiudadOrigen { get; set; }
            public string TipoNodoOrigen { get; set; }
            public string NodoOrigen { get; set; }
            public string ContinenteDestino { get; set; }
            public string PaisDestino { get; set; }
            public string CiudadDestino { get; set; }
            public string TipoNodoDestino { get; set; }
            public string NodoDestino { get; set; }
            public string EtaVencido { get; set; }
            public int DiasEtaVencido { get; set; }
            public DateTime? FechaEnvio { get; set; }
            public DateTime? FechaUltimaAccionLogistica { get; set; }
            public string UsuarioMovimiento { get; set; }
            //MOV LOGISTICO

            //RECUPERACION
            public string RetrievalProvider { get; set; }
            public string NombreRPNotificado { get; set; }
            public int NumNotificaciones { get; set; }
            public string RetrievalProviderNoti { get; set; }
            public DateTime? FechaPrimeraNotificacion { get; set; }
            public DateTime? FechaNotificacionReal { get; set; }
            public int ReintentoRecupera { get; set; }
            public DateTime? FechaRecuperacion { get; set; }
            public DateTime? Etr { get; set; }
            public string EtrVencido { get; set; }
            public int DiasEtrVencido { get; set; }
            public string NotaRecuperacion { get; set; }
            public string LugarRecuperacion { get; set; }
            public string ContinenteRecuperacion { get; set; }
            public int Requerimiento { get; set; }
            public DateTime? FechaPriorizacion { get; set; }
            //RECUPERACION

            //SERVICIO
            public int IdServicio { get; set; }
            public string EstadoServicio { get; set; }
            public string Booking { get; set; }
            public string Contenedor { get; set; }
            public string Naviera { get; set; }
            public string Nave { get; set; }
            public string TratamientoCO2 { get; set; }
            public string Usuario { get; set; }
            public DateTime? FechaControlador { get; set; }
            //SERVICIO

            public string EstadoRetrieval { get; set; }
            public string AnoServicio { get; set; }
            public string TipoMovimiento { get; set; }
            //DEPOSITO
            public int IdTipoCommodity { get; set; }
            public string TipoCommodity { get; set; }
            public DateTime? FechaProgramacion { get; set; }
            public string DetalleAlerta { get; set; }
            public string UltimaUbicacion { get; set; }
            public DateTime? FechaUltUbicacion { get; set; }
        }

        public class ESN
        {
            public int Id { get; set; }
            public int IdEstadoRecuperacion { get; set; }
            public string EstadoRecuperacion { get; set; }
            public int IdUltMovLogistico { get; set; }
            public int TransitoNodo { get; set; }
            public string TransitoNodoTxt { get; set; }
            public int Prioritario { get; set; }
            public string PrioritarioTxt { get; set; }
            public int Recuperado { get; set; }
            public string RecuperadoTxt { get; set; }
            public int Perdido { get; set; }
            public string PerdidoTxt { get; set; }
            public DateTime? FechaPerdida { get; set; }
            public string EstadoTecnico { get; set; }
            public string NumESN { get; set; }
            public int DamageReport { get; set; }
            public string DamageReportTxt { get; set; }
            public string TipoESN { get; set; }
        }

        public class Viaje
        {
            public int Id { get; set; }
            public string NumeroViaje { get; set; }
            public string PuertoOrigen { get; set; }
            public string PuertoDestino { get; set; }
            public string Nave { get; set; }
            public DateTime? EtaNave { get; set; }
            public DateTime? InicioStacking { get; set; }
            public DateTime? FinStacking { get; set; }
            public DateTime? Etd { get; set; }
            public DateTime? EtaPod { get; set; }
            public int Estado { get; set; }
            public string Usuario { get; set; }
            public int Validado { get; set; }
            public int IdPuertoOrigen { get; set; }
            public int IdPuertoDestino { get; set; }
            public int IdNave { get; set; }
        }

        public class EstadoContenedor
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
        }

        public class Coordinador
        {
            public int IdUsuario { get; set; }
            public string NombreUsuario { get; set; }

        }

        public class Facturacion
        {
            public string Checkbox { get; set; }
            public string Continente { get; set; }
            public string ServiceProvider { get; set; }
            public string Pais { get; set; }
            public string PuertoOrigen { get; set; }
            public string PuertoDestino { get; set; }
            public string PaisDeposito { get; set; }
            public string Contenedor { get; set; }
            public string Naviera { get; set; }
            public string Nave { get; set; }
            public string Viaje { get; set; }
            public string Commodity { get; set; }
            public string Tratamiento { get; set; }
            public DateTime? FechaControlador { get; set; }
            public string Booking { get; set; }
            public int Tamano { get; set; }
            public DateTime? Etd { get; set; }
            public string Exportador { get; set; }
            public int EstadoFacturacion { get; set; }
            public string CodigoSoftland { get; set; }
            public string NumeroFactura { get; set; }
            public int Precio { get; set; }
            public decimal TipoCambio { get; set; }
            public DateTime? FechaFacturacion { get; set; }
            public string Gestion { get; set; }
            public DateTime? FechaGestion { get; set; }
            public string CentroCosto { get; set; }
            public int NotaCredito { get; set; }
            public decimal TipoCambioNota { get; set; }
            public int IdServicio { get; set; }
            public int Semana { get; set; }
            public string MesFacturacion { get; set; }
            public int PrecioServicioPesos { get; set; }
            public int PrecioOriginalPesos { get; set; }
        }

        public class CentroCostos
        {
            public int IdCentro { get; set; }
            public string Nombre { get; set; }
        }

        public class Notificacion
        {
            public int Validacion { get; set; }
            public string Email { get; set; }
        }

        public class Nodo
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
        }

        public class Clientes
        {
            public int IdUsuario { get; set; }
            public string NombreUsuario { get; set; }
            public string Email { get; set; }
            public string Tipo { get; set; }
            public string Estado { get; set; }
        }

        public class CambioReserva
        {
            public string Booking { get; set; }
            public string Item { get; set; }
            public string ValorActual { get; set; }
            public string ValorNuevo { get; set; }
            public DateTime? Fecha_Creacion { get; set; }
            public DateTime? Fecha_Edicion { get; set; }
            public int Estado { get; set; }
        }

        public class Sensor
        {
            public int IdSensor { get; set; }
            public string NumeroSerie { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
            public string EstadoLogistico { get; set; }
            public string TipoNodoOrigen { get; set; }
            public string PaisOrigen { get; set; }
            public string CiudadOrigen { get; set; }
            public string NodoOrigen { get; set; }
            public string TipoNodoDestino { get; set; }
            public string PaisDestino { get; set; }
            public string CiudadDestino { get; set; }
            public string NodoDestino { get; set; }
            public string Alerta { get; set; }
            public DateTime? FechaVencimiento { get; set; }
        }

        public class MarcaSensor
        {
            public int IdMarca { get; set; }
            public string Nombre { get; set; }
        }

        public class ModeloSensor
        {
            public int IdModelo { get; set; }
            public string Nombre { get; set; }
        }

        public class Alerta
        {
            public string Checkbox { get; set; }
            public int Id { get; set; }
            public DateTime Fecha { get; set; }
            public string Descripcion { get; set; }
            public int IdServicio { get; set; }
            public string Contenedor { get; set; }
            public string Controlador { get; set; }
            public string Modem { get; set; }
            public int Activo { get; set; }
        }

        public class LogisticModem
        {
            public string NumModem { get; set; }
            public string Contenedor_modem { get; set; }
            public string Contenedor_controlador { get; set; }
            public DateTime FechaUltConexionModemPTL { get; set; }
            public DateTime FechaUltConexionModemController { get; set; }
        }

        public class ResultadoValidacionPrezarpe
        {
            public int IdServicio { get; set; }
            public int Resultado { get; set; }
        }

        public class InfoServicioPTL
        {
            public int id { get; set; }
            public string faultAnalysisResult { get; set; }
            public DateTime lastDataDownload { get; set; }
        }

        public class ResultadoFaultAnalysisPTL
        {
            public int id { get; set; }
            public int idSampleClasification { get; set; }
            public int nrStretch { get; set; }
            public float faultTime { get; set; }
            public float faultTimePercent { get; set; }
            public DateTime analysisDate { get; set; }
            public int faultCode { get; set; }
            public string faultResult { get; set; }
            public int idCodeResult { get; set; }
        }

        public class Muestra
        {
            public int id { get; set; }
            public DateTime? fecha { get; set; }
            public float co2 { get; set; }
            public float o2 { get; set; }
        }
    }
}