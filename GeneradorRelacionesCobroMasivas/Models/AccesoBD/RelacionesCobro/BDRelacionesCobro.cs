using CREC_MVC.Models.Entidades;
using CREC_MVC.Models.Entidades.RelacionesCobro;
using CREC_MVC.Models.Querys.RelacionesCobro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CREC_MVC.Models.AccesoBD.RelacionesCobro
{
    public class BDRelacionesCobro 
    {
        public List<ECoordinaciones> GetCoordinaciones()
        {
            var lista = new List<ECoordinaciones>();
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            try
            {
                con.Open();
                var cmd = new SqlCommand(qRelacionesCobroController.obtenerCoordinaciones, con);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        lista.Add(new ECoordinaciones
                        {
                            ID_SUCURSAL = rd.IsDBNull(rd.GetOrdinal("ID_SUCURSAL")) ? 0 : Convert.ToInt16(rd["ID_SUCURSAL"]),
                            NOMBRE_SUCURSAL = rd.IsDBNull(rd.GetOrdinal("NOMBRE_SUCURSAL")) ? "" : Convert.ToString(rd["NOMBRE_SUCURSAL"]),
                            A = rd.IsDBNull(rd.GetOrdinal("A")) ? 0 : Convert.ToInt16(rd["A"]),
                            B = rd.IsDBNull(rd.GetOrdinal("B")) ? 0 : Convert.ToInt16(rd["B"]),
                            C = rd.IsDBNull(rd.GetOrdinal("C")) ? 0 : Convert.ToInt16(rd["C"]),
                            D = rd.IsDBNull(rd.GetOrdinal("D")) ? 0 : Convert.ToInt16(rd["D"]),
                            CORD_VACANTE = rd.IsDBNull(rd.GetOrdinal("CORD_VACANTE")) ? 0 : Convert.ToInt16(rd["CORD_VACANTE"])
                        });

                }



            }
            catch (Exception ex)
            {
                lista = new List<ECoordinaciones>();
            }
            finally
            {
                con.Close();
            }

            return lista;
        }
        public List<ERelaciones> GetAsociadas(int ID_SUCURSAL, string COORDINACION)
        {
            var lista = new List<ERelaciones>();
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            try
            {
                con.Open();
                var cmd = new SqlCommand(qRelacionesCobroController.obtenerAsociadas, con);
                cmd.Parameters.AddWithValue("@ID_SUCURSAL", ID_SUCURSAL);
                cmd.Parameters.AddWithValue("@COORDINACION", COORDINACION);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        lista.Add(new ERelaciones
                        {
                            ID_ASOCIADA = rd.IsDBNull(rd.GetOrdinal("ID_ASOCIADA")) ? 0 : Convert.ToInt32(rd["ID_ASOCIADA"])
                        });

                }
            }
            catch (Exception ex)
            {
                lista = new List<ERelaciones>();
            }
            finally
            {
                con.Close();
            }

            return lista;
        }
        public ERelacionCobro ConsultaDatosRelacionCobro(int idPrestamo, DateTime Fecha_Corte)
        {
            CultureInfo ci = CultureInfo.InvariantCulture;
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            ERelacionCobro resp = new ERelacionCobro();
            try
            {
                con.Open();
                var cmd = new SqlCommand(qRelacionesCobroController.ConsultaDatosRelacionC, con);
                cmd.Parameters.AddWithValue("@ID_PRESTAMO", idPrestamo);
                cmd.Parameters.AddWithValue("@FECHA_CORTE", Fecha_Corte);

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        resp = new ERelacionCobro
                        {
                            ID_PRESTAMO = rd.IsDBNull(rd.GetOrdinal("ID_PRESTAMO")) ? 0 : Convert.ToInt32(rd["ID_PRESTAMO"])
                          ,
                            HOY_LETRAS = rd.IsDBNull(rd.GetOrdinal("HOY_LETRAS")) ? "" : Convert.ToString(rd["HOY_LETRAS"])
                          ,
                            NOMBRE_COMPLETO = rd.IsDBNull(rd.GetOrdinal("NOMBRE_COMPLETO")) ? "" : Convert.ToString(rd["NOMBRE_COMPLETO"])
                          ,
                            ULTIMO_PAGO = rd.IsDBNull(rd.GetOrdinal("ULTIMO_PAGO")) ? "" : Convert.ToString(rd["ULTIMO_PAGO"])
                          ,
                            PROMEDIO = rd.IsDBNull(rd.GetOrdinal("PROMEDIO")) ? "" : Convert.ToString(rd["PROMEDIO"])
                          ,
                            FECHA_ACTIVADO = rd.IsDBNull(rd.GetOrdinal("FECHA_ACTIVADO")) ? "" : Convert.ToString(rd["FECHA_ACTIVADO"])
                          ,
                            FECHA_LIMIT = rd.IsDBNull(rd.GetOrdinal("FECHA_LIMIT")) ? "" : Convert.ToString(rd["FECHA_LIMIT"])
                          ,
                            NOMBRE_SUCURSAL = rd.IsDBNull(rd.GetOrdinal("NOMBRE_SUCURSAL")) ? "" : Convert.ToString(rd["NOMBRE_SUCURSAL"])
                          ,
                            FECHACORTE = rd.IsDBNull(rd.GetOrdinal("FECHACORTE")) ? "" : Convert.ToString(rd["FECHACORTE"])
                          ,
                            MONTO_LINEA = rd.IsDBNull(rd.GetOrdinal("MONTO_LINEA")) ? "" : Convert.ToString(rd["MONTO_LINEA"])
                          ,
                            MONTO_ACTIVO = rd.IsDBNull(rd.GetOrdinal("MONTO_ACTIVO")) ? "" : Convert.ToString(rd["MONTO_ACTIVO"])
                          ,
                            SALDO_DEUDOR = rd.IsDBNull(rd.GetOrdinal("SALDO_DEUDOR")) ? "" : Convert.ToString(rd["SALDO_DEUDOR"])
                          ,
                            MONTO_DISPONIBLE = rd.IsDBNull(rd.GetOrdinal("MONTO_DISPONIBLE")) ? "" : Convert.ToString(rd["MONTO_DISPONIBLE"])
                          ,
                            MONTO_CONTINGENCIA = rd.IsDBNull(rd.GetOrdinal("MONTO_CONTINGENCIA")) ? "" : Convert.ToString(rd["MONTO_CONTINGENCIA"])
                          ,
                            NOMBRE_SEGURO = rd.IsDBNull(rd.GetOrdinal("NOMBRE_SEGURO")) ? "" : Convert.ToString(rd["NOMBRE_SEGURO"])
                          ,
                            POLIZA = rd.IsDBNull(rd.GetOrdinal("POLIZA")) ? "" : Convert.ToString(rd["POLIZA"])
                          ,
                            TOTAL_SEG = rd.IsDBNull(rd.GetOrdinal("TOTAL_SEG")) ? "" : Convert.ToString(rd["TOTAL_SEG"])
                          ,
                            SALDO_VENCIDO = rd.IsDBNull(rd.GetOrdinal("SALDO_VENCIDO")) ? 0 : Convert.ToDecimal(rd["SALDO_VENCIDO"])
                          ,
                            CARGO_TARDIO = rd.IsDBNull(rd.GetOrdinal("CARGO_TARDIO")) ? 0 : Convert.ToDecimal(rd["CARGO_TARDIO"])
                          ,
                            TOTAL_COBRAR = rd.IsDBNull(rd.GetOrdinal("TOTAL_COBRAR")) ? 0 : Convert.ToDecimal(rd["TOTAL_COBRAR"])
                          ,
                            SALDO_FAVOR = rd.IsDBNull(rd.GetOrdinal("SALDO_FAVOR")) ? 0 : Convert.ToDecimal(rd["SALDO_FAVOR"])
                          ,
                            TOTALSEG = rd.IsDBNull(rd.GetOrdinal("TOTALSEG")) ? 0 : Convert.ToDecimal(rd["TOTALSEG"])
                          ,
                            SUBTIPOMONTOPAGO = rd.IsDBNull(rd.GetOrdinal("SUBTIPOMONTOPAGO")) ? 0 : Convert.ToDecimal(rd["SUBTIPOMONTOPAGO"])
                          ,
                            SUBTIPOMONTOBONIFICACION = rd.IsDBNull(rd.GetOrdinal("SUBTIPOMONTOBONIFICACION")) ? 0 : Convert.ToDecimal(rd["SUBTIPOMONTOBONIFICACION"])
                          ,
                            REFERENCIA = rd.IsDBNull(rd.GetOrdinal("REFERENCIA")) ? "" : Convert.ToString(rd["REFERENCIA"])
                          ,
                            STP = rd.IsDBNull(rd.GetOrdinal("STP")) ? "" : Convert.ToString(rd["STP"])
                          ,
                            CodBar = rd.IsDBNull(rd.GetOrdinal("CodBar")) ? "" : Convert.ToString(rd["CodBar"])
                          ,
                            PayCash = rd.IsDBNull(rd.GetOrdinal("PayCash")) ? "" : Convert.ToString(rd["PayCash"])

                        };
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return resp;
        }
        public List<EClientesFinales> ConsultaDatosCF(int idPrestamo, DateTime Fecha_Corte)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            List<EClientesFinales> resp = new List<EClientesFinales>();
            try
            {
                con.Open();
                var cmd = new SqlCommand(qRelacionesCobroController.ConsultaDatosCF, con);
                cmd.Parameters.AddWithValue("@ID_PRESTAMO", idPrestamo);
                cmd.Parameters.AddWithValue("@FECHA_CORTE", Fecha_Corte);

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        resp.Add(new EClientesFinales
                        {
                            ID_CLIENTE = rd.IsDBNull(rd.GetOrdinal("ID_CLIENTE")) ? 0 : Convert.ToInt32(rd["ID_CLIENTE"])
                          ,
                            ID_PRESTAMO = rd.IsDBNull(rd.GetOrdinal("ID_PRESTAMO")) ? "" : Convert.ToString(rd["ID_PRESTAMO"])
                          ,
                            NOMBRE = rd.IsDBNull(rd.GetOrdinal("NOMBRE")) ? "" : Convert.ToString(rd["NOMBRE"])
                          ,
                            FOLIO = rd.IsDBNull(rd.GetOrdinal("FOLIO")) ? "" : Convert.ToString(rd["FOLIO"])
                          ,
                            FECHA_ACTIVADO = rd.IsDBNull(rd.GetOrdinal("FECHA_ACTIVADO")) ? "" : Convert.ToString(rd["FECHA_ACTIVADO"])
                          ,
                            NUM_PAGOS = rd.IsDBNull(rd.GetOrdinal("NUM_PAGOS")) ? "" : Convert.ToString(rd["NUM_PAGOS"])
                          ,
                            MONTO_PRESTAMO = rd.IsDBNull(rd.GetOrdinal("MONTO_PRESTAMO")) ? "" : Convert.ToString(rd["MONTO_PRESTAMO"])
                          ,
                            SALDO_ANTERIOR = rd.IsDBNull(rd.GetOrdinal("SALDO_ANTERIOR")) ? "" : Convert.ToString(rd["SALDO_ANTERIOR"])
                          ,
                            SALDO_ACTUAL = rd.IsDBNull(rd.GetOrdinal("SALDO_ACTUAL")) ? "" : Convert.ToString(rd["SALDO_ACTUAL"])
                          ,
                            MONTO_PAGO = rd.IsDBNull(rd.GetOrdinal("MONTO_PAGO")) ? "" : Convert.ToString(rd["MONTO_PAGO"])
                          ,
                            PRESTAMO = rd.IsDBNull(rd.GetOrdinal("ID_PRESTAMO")) ? "" : Convert.ToString(rd["ID_PRESTAMO"])
                          ,
                            NUM_PAGO = rd.IsDBNull(rd.GetOrdinal("NUM_PAGO")) ? "" : Convert.ToString(rd["NUM_PAGO"])
                          ,
                            MONTO_SEGURO = rd.IsDBNull(rd.GetOrdinal("MONTO_SEGURO")) ? "" : Convert.ToString(rd["MONTO_SEGURO"])

                        }
                    );
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return resp;
        }
        public List<EClientesPP> ConsultaPP(int idPrestamo, DateTime Fecha_Corte)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            List<EClientesPP> resp = new List<EClientesPP>();
            try
            {
                con.Open();
                var cmd = new SqlCommand(qRelacionesCobroController.ConsultaDatosPP, con);
                cmd.Parameters.AddWithValue("@ID_PRESTAMO", idPrestamo);
                cmd.Parameters.AddWithValue("@FECHA_CORTE", Fecha_Corte);

                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        resp.Add(new EClientesPP
                        {
                            ID_PRESTAMO = rd.IsDBNull(rd.GetOrdinal("ID_PRESTAMO")) ? "" : Convert.ToString(rd["ID_PRESTAMO"])
                            ,
                            ID_CLIENTE = rd.IsDBNull(rd.GetOrdinal("ID_CLIENTE")) ? "" : Convert.ToString(rd["ID_CLIENTE"])
                            ,
                            TIPO = rd.IsDBNull(rd.GetOrdinal("TIPO")) ? "" : Convert.ToString(rd["TIPO"])
                            ,
                            TIPOS = rd.IsDBNull(rd.GetOrdinal("TIPOS")) ? "" : Convert.ToString(rd["TIPOS"])
                            ,
                            NUM_PAGOS = rd.IsDBNull(rd.GetOrdinal("NUM_PAGOS")) ? "" : Convert.ToString(rd["NUM_PAGOS"])
                            ,
                            FECHA_ACTIVADO = rd.IsDBNull(rd.GetOrdinal("FECHA_ACTIVADO")) ? "" : Convert.ToString(rd["FECHA_ACTIVADO"])
                            ,
                            NUM_PAGO = rd.IsDBNull(rd.GetOrdinal("NUM_PAGO")) ? "" : Convert.ToString(rd["NUM_PAGO"])
                            ,
                            IMPORTE = rd.IsDBNull(rd.GetOrdinal("IMPORTE")) ? "" : Convert.ToString(rd["IMPORTE"])
                            ,
                            BONIFICACION = rd.IsDBNull(rd.GetOrdinal("BONIFICACION")) ? "" : Convert.ToString(rd["BONIFICACION"])
                            ,
                            FECHA_PAGO = rd.IsDBNull(rd.GetOrdinal("FECHA_PAGO")) ? "" : Convert.ToString(rd["FECHA_PAGO"])
                            ,
                            SALDO_ACTUAL = rd.IsDBNull(rd.GetOrdinal("SALDO_ACTUAL")) ? "" : Convert.ToString(rd["SALDO_ACTUAL"])
                            ,
                            MONTO_PAGO = rd.IsDBNull(rd.GetOrdinal("MONTO_PAGO")) ? "" : Convert.ToString(rd["MONTO_PAGO"])
                            ,
                            IS_PP = rd.IsDBNull(rd.GetOrdinal("IS_PP")) ? "" : Convert.ToString(rd["IS_PP"])
                            ,
                            IS_REESTRUCTURA = rd.IsDBNull(rd.GetOrdinal("IS_REESTRUCTURA")) ? "" : Convert.ToString(rd["IS_REESTRUCTURA"])

                        }
                    );
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return resp;
        }
        public List<EBonificacion> ConsultaBonificacion(int idPrestamo, DateTime Fecha_Corte)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            List<EBonificacion> resp = new List<EBonificacion>();
            try
            {
                con.Open();
                var cmd = new SqlCommand(qRelacionesCobroController.ConsultaBonificacion, con);
                cmd.Parameters.AddWithValue("@ID_PRESTAMO", idPrestamo);
                cmd.Parameters.AddWithValue("@FECHA_CORTE", Fecha_Corte);

                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                            resp.Add(new EBonificacion
                            {
                                ROWQ = rd.IsDBNull(rd.GetOrdinal("ROWQ")) ? "" : Convert.ToString(rd["ROWQ"])
                                ,
                                FEC = rd.IsDBNull(rd.GetOrdinal("FEC")) ? "" : Convert.ToString(rd["FEC"])
                                ,
                                DIAS_H = rd.IsDBNull(rd.GetOrdinal("DIAS_H")) ? "" : Convert.ToString(rd["DIAS_H"])
                                ,
                                ID_BONIFICACION_PRESTAGANA = rd.IsDBNull(rd.GetOrdinal("ID_BONIFICACION_PRESTAGANA")) ? "" : Convert.ToString(rd["ID_BONIFICACION_PRESTAGANA"])
                                ,
                                ESQUEMA = rd.IsDBNull(rd.GetOrdinal("ESQUEMA")) ? "" : Convert.ToString(rd["ESQUEMA"])
                                ,
                                PORCENTAJE = rd.IsDBNull(rd.GetOrdinal("PORCENTAJE")) ? "" : Convert.ToString(rd["PORCENTAJE"])
                                ,
                                COMISION = rd.IsDBNull(rd.GetOrdinal("COMISION")) ? "" : Convert.ToString(rd["COMISION"])
                                ,
                                NIVEL = rd.IsDBNull(rd.GetOrdinal("NIVEL")) ? "" : Convert.ToString(rd["NIVEL"])
                                ,
                                QUITA = rd.IsDBNull(rd.GetOrdinal("QUITA")) ? "" : Convert.ToString(rd["QUITA"])
                                ,
                                ID_ESQUEMA = rd.IsDBNull(rd.GetOrdinal("ID_ESQUEMA")) ? "" : Convert.ToString(rd["ID_ESQUEMA"])


                            }
                        );
                    }
                    else {
                        resp.Add(
                            
                           new EBonificacion
                                 {
                            ROWQ =""

                               ,
                            FEC = "01/01/1900"
                               ,
                            DIAS_H = "01/01/1900"
                               ,
                            ID_BONIFICACION_PRESTAGANA = ""
                               ,
                            ESQUEMA = ""
                               ,
                            PORCENTAJE = ""
                               ,
                            COMISION = ""
                               ,
                            NIVEL = ""
                               ,
                            QUITA = "",
                            ID_ESQUEMA = ""


                           });
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return resp;
        }
        public List<EBonificacion> ConsultaMontosBonificacion(int idPrestamo, DateTime Fecha_Corte)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            List<EBonificacion> resp = new List<EBonificacion>();
            try
            {
                con.Open();
                var cmd = new SqlCommand(qRelacionesCobroController.ConsultaBonificacionMontos, con);
                cmd.Parameters.AddWithValue("@ID_PRESTAMO", idPrestamo);
                cmd.Parameters.AddWithValue("@FECHA_CORTE", Fecha_Corte);

                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.HasRows)
                    {

                        while (rd.Read())
                            resp.Add(new EBonificacion
                            {

                                ESQUEMA = rd.IsDBNull(rd.GetOrdinal("ESQUEMA")) ? "" : Convert.ToString(rd["ESQUEMA"])
                               ,
                                MONTO_PAGO = rd.IsDBNull(rd.GetOrdinal("MONTO_PAGO")) ? "" : Convert.ToString(rd["MONTO_PAGO"])
                                ,
                                MONTO_BONIFICACION = rd.IsDBNull(rd.GetOrdinal("MONTO_BONIFICACION")) ? 0 : Convert.ToDecimal(rd["MONTO_BONIFICACION"])

                            }

                        );
                    }
                    else
                    {
                        resp.Add(
                            new EBonificacion
                            {

                                ESQUEMA = ""
                               ,
                                MONTO_PAGO = "0"
                                ,
                                MONTO_BONIFICACION = 0

                            });


                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return resp;
        }
        public string GetFechaCorte()
        {
           var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            string fecha = "";
            try
            {
                con.Open();
                var cmd = new SqlCommand(qRelacionesCobroController.GetFechaCorte, con);
                fecha = Convert.ToString(cmd.ExecuteScalar());



            }
            catch (Exception ex)
            {
                fecha = "Error"+ex.Message;
            }
            finally
            {
                con.Close();
            }

            return fecha;
        }
        public string ActualizaEstatusRegistro(string NomArchivo, int ID_SUCURSAL, int NunPDF, string FECHACORTE)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            string rs = "";
            try
            {
                con.Open();
                var cmd = new SqlCommand(qRelacionesCobroController.ActualizaRegistro, con);
                cmd.Parameters.AddWithValue("@ID_SUCURSAL", ID_SUCURSAL);
                cmd.Parameters.AddWithValue("@NOMBRE_ARCHIVO", NomArchivo);
                cmd.Parameters.AddWithValue("@NUM_PDF", NunPDF);
                cmd.Parameters.AddWithValue("@FECHA_CORTE", Convert.ToDateTime(FECHACORTE));
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())

                        rs = rd.IsDBNull(rd.GetOrdinal("RESPUESTA")) ? "" : Convert.ToString(rd["RESPUESTA"]);
                }

            }
            catch (Exception ex)
            {
                rs = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return rs;
        }
        public string ObtenerPromociones(int idPrestamo, DateTime Fecha_Corte)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            string rs = "";
            try
            {
                con.Open();
                var cmd = new SqlCommand(qRelacionesCobroController.promociones, con);
                cmd.Parameters.AddWithValue("@ID_PRESTAMO", idPrestamo);
                cmd.Parameters.AddWithValue("@FECHA_CORTE", Fecha_Corte);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())

                        rs = rd.IsDBNull(rd.GetOrdinal("PROMOCIONES")) ? "" : Convert.ToString(rd["PROMOCIONES"]);
                }

            }
            catch (Exception ex)
            {
                rs = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return rs;
        }
        public string ObtenerImagen(int idPrestamo, DateTime Fecha_Corte)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            string rs = "";
            try
            {
                con.Open();
                var cmd = new SqlCommand(qRelacionesCobroController.imagen, con);
                cmd.Parameters.AddWithValue("@ID_PRESTAMO", idPrestamo);
                cmd.Parameters.AddWithValue("@FECHA_CORTE", Fecha_Corte);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())

                        rs = rd.IsDBNull(rd.GetOrdinal("IMAGEN")) ? "" : Convert.ToString(rd["IMAGEN"]);
                }

            }
            catch (Exception ex)
            {
                rs = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return rs;
        }
    }
}