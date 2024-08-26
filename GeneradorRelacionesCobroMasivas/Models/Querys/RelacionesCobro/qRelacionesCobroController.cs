namespace CREC_MVC.Models.Querys.RelacionesCobro
{
    public class qRelacionesCobroController { 
      

        public const string obtenerCoordinaciones = @"SELECT * FROM FN_GET_COORDINACIONES_PG()";
        public const string obtenerAsociadas = @"SELECT * FROM FN_GET_ASOCIADAS_RELACIONES_COBRO  (@ID_SUCURSAL,@COORDINACION)  ORDER BY NOMBRE_ASOCIADA";
        public const string ConsultaDatosRelacionC = @"EXEC SP_CONSULTA_DATOS_RELACION_COBRO_PG @FECHA_CORTE,@ID_PRESTAMO";
        public const string ConsultaDatosCF = @"    Select ID_CLIENTE, ID_PRESTAMO, NOMBRE
                                                         , FOLIO, FECHA_ACTIVADO, NUM_PAGOS, MONTO_PRESTAMO, SALDO_ANTERIOR
                                                         , SALDO_ACTUAL, MONTO_PAGO , ID_PRESTAMO,NUM_PAGO
                                                         , MONTO_SEGURO 
                                                    from dbo.FN_NP_DETALLE_CLIENTES_PG(@ID_PRESTAMO, @FECHA_CORTE)";


        public const string ConsultaDatosPP = @" 
                    SELECT ID_PRESTAMO
                    , ID_CLIENTE
                    , TIPO
                    , ISNULL(TIPO, 'PRESTAMO PERSONAL') TIPOS
                    ,FECHA_ACTIVADO
                    ,NUM_PAGOS
                    ,NUM_PAGO
                    ,IMPORTE
                    ,BONIFICACION
                    ,FECHA_PAGO
                    ,SALDO_ACTUAL
                    ,MONTO_PAGO
                    ,ISNULL(IS_PP, 0) IS_PP
                    ,ISNULL(IS_REESTRUCTURA, 0) IS_REESTRUCTURA
                    FROM[FUNC_DETALLE_PRESTAMOS_SUBTIPOS_CLICKBOX_PG] (@ID_PRESTAMO, @FECHA_CORTE)
                    ORDER BY FECHA_ACTIVADO";
        public const string ConsultaBonificacion = @"EXEC SP_BONIFICACION_PRESTAGANA_FORMATO_PRUEBA @ID_PRESTAMO, @FECHA_CORTE";

        public const string ConsultaBonificacionMontos = @"EXEC SP_GET_MONTOS_BONIFICACION_PG_PRUEBA @ID_PRESTAMO, @FECHA_CORTE";

        public const string GetFechaCorte = @"SELECT TOP 1 FECHA_CORTE FROM C_FECHA_CORTE_PRESTAGANA WHERE FECHA_CORTE<=GETDATE() ORDER BY FECHA_CORTE DESC";

        public const string ActualizaRegistro = @"EXEC SP_ACTUALIZA_REGISTRO_RELACIONES_COBRO @ID_SUCURSAL,@NOMBRE_ARCHIVO,@NUM_PDF,@FECHA_CORTE";
        public const string promociones = @"SELECT ISNULL(PROMOCIONES,0) AS PROMOCIONES FROM BD_SALDO_DISTRIBUIDORA_PG  WHERE ID_PRESTAMO = @ID_PRESTAMO AND FECHA_CORTE = @FECHA_CORTE and id_status=1";

        public const string imagen = @"SELECT IMAGEN FROM C_ESQUEMA_BONIFICACION WHERE C_ESQUEMA_BONIFICACION =  (SELECT ID_ESQUEMA FROM BD_SALDO_DISTRIBUIDORA_PG WHERE ID_PRESTAMO = @ID_PRESTAMO AND FECHA_CORTE = @FECHA_CORTE and id_status=1)";


    }

}