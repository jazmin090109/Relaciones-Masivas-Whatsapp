using System.Collections.Generic;

namespace CREC_MVC.Models.Entidades.RelacionesCobro
{

    public class ECoordinaciones
    {
        public int ID_SUCURSAL { get; set; }
        public string NOMBRE_SUCURSAL { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }
        public int G { get; set; }
        public int CORD_VACANTE { get; set; }
    }

    public class ERelaciones
    {
        public int ID_ASOCIADA { get; set; }
        public string NOMBRE_ASOCIADA { get; set; }
    }
    public class ERelacionCobro
    {
        public int ID_PRESTAMO { get; set; }
        public string HOY_LETRAS { get; set; }
        public string NOMBRE_COMPLETO { get; set; }
        public string ULTIMO_PAGO { get; set; }
        public string PROMEDIO { get; set; }
        public string FECHA_ACTIVADO { get; set; }
        public string FECHA_LIMIT { get; set; }
        public string NOMBRE_SUCURSAL { get; set; }
        public string FECHACORTE { get; set; }
        public string MONTO_LINEA { get; set; }
        public string MONTO_ACTIVO { get; set; }
        public string SALDO_DEUDOR { get; set; }
        public string MONTO_DISPONIBLE { get; set; }
        public string MONTO_CONTINGENCIA { get; set; }
        public string NOMBRE_SEGURO { get; set; }
        public string POLIZA { get; set; }
        public string TOTAL_SEG { get; set; }
        public decimal SALDO_VENCIDO { get; set; }
        public decimal CARGO_TARDIO { get; set; }
        public decimal TOTAL_COBRAR { get; set; }
        public decimal SALDO_FAVOR { get; set; }
        public decimal TOTALSEG { get; set; }
        public decimal SUBTIPOMONTOPAGO { get; set; }
        public decimal SUBTIPOMONTOBONIFICACION { get; set; }
        public string REFERENCIA { get; set; }
        public string STP { get; set; }
        public string CodBar { get; set; }
        public string PayCash { get; set; }


    }
    public class ModelRelacionesCobroDatos
    {
        public List<EClientesFinales> listRelAsociada { get; set; }
    }
    public class EClientesFinales
    {
        public int ID_CLIENTE { get; set; }
        public string ID_PRESTAMO { get; set; }
        public string NOMBRE { get; set; }
        public string FOLIO { get; set; }
        public string FECHA_ACTIVADO { get; set; }
        public string NUM_PAGOS { get; set; }
        public string MONTO_PRESTAMO { get; set; }
        public string SALDO_ANTERIOR { get; set; }
        public string SALDO_ACTUAL { get; set; }
        public string MONTO_PAGO { get; set; }
        public string PRESTAMO { get; set; }
        public string NUM_PAGO { get; set; }
        public string MONTO_SEGURO { get; set; }


    }
    public class EClientesPP
    {
        public string ID_PRESTAMO { get; set; }
        public string ID_CLIENTE { get; set; }
        public string TIPO { get; set; }
        public string TIPOS { get; set; }
        public string FECHA_ACTIVADO { get; set; }
        public string NUM_PAGOS { get; set; }
        public string NUM_PAGO { get; set; }
        public string IMPORTE { get; set; }
        public string BONIFICACION { get; set; }
        public string FECHA_PAGO { get; set; }
        public string SALDO_ACTUAL { get; set; }
        public string MONTO_PAGO { get; set; }
        public string IS_PP { get; set; }
        public string IS_REESTRUCTURA { get; set; }


    }
    public class EBonificacion
    {
        public string ROWQ { get; set; }
        public string FEC { get; set; }
        public string DIAS_H { get; set; }
        public string ID_BONIFICACION_PRESTAGANA { get; set; }
        public string ESQUEMA { get; set; }
        public string PORCENTAJE { get; set; }
        public string COMISION { get; set; }
        public string NIVEL { get; set; }
        public string QUITA { get; set; }
        public string ID_ESQUEMA { get; set; }
        public string MONTO_PAGO { get; set; }
        public decimal MONTO_BONIFICACION { get; set; }
    }

}
