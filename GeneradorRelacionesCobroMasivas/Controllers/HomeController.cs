using CREC_MVC.Models.AccesoBD.RelacionesCobro;
using CREC_MVC.Models.Entidades.RelacionesCobro;
using System.Collections.Generic;
using System.Web.Mvc;
using Rotativa;
using Rotativa.Options;
using System.IO;
using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using ZXing.Common;
using ZXing;
using ZXing.Rendering;
using Microsoft.Ajax.Utilities;
using System.Linq;

namespace GeneradorRelacionesCobroMasivas.Controllers
{
    public class HomeController : Controller
    {
        BDRelacionesCobro bd = new BDRelacionesCobro();
        public ActionResult GeneradorRelacionesCobro()
        {
            List<ECoordinaciones> listaCoordinaciones = bd.GetCoordinaciones();
            return View(listaCoordinaciones);
        }

        public string GenerarRelaciones(int ID_SUCURSAL, string COORDINACION, string NOMBRE_SUCURSAL)
        {
            int numAsociadas;
            string rs = "";
            string NomArchivo = "/PDF/RelacionesDeCobro_" + NOMBRE_SUCURSAL + "_" + COORDINACION + ".pdf";
            string FechaCorte = bd.GetFechaCorte();
            DateTime FechaCorte1 = DateTime.Parse(FechaCorte);  // Convertir string a DateTime
            string formattedFechaCorte = FechaCorte1.ToString("dd_MM_yyyy");  // Formato dd_MM_yyyy

            // Definir la ruta de la carpeta
            string folderPath = "E:/RC/" + formattedFechaCorte;
            ERelaciones r = new ERelaciones();

            try
            {
                List<ERelaciones> listAsociadas = bd.GetAsociadas(ID_SUCURSAL, COORDINACION);
                numAsociadas = listAsociadas.Count;
                bd.ActualizaEstatusRegistro(NomArchivo, ID_SUCURSAL, numAsociadas, FechaCorte);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                foreach (var item in listAsociadas)
                {

                    GetPDF(item.ID_ASOCIADA, item.NOMBRE_ASOCIADA, NOMBRE_SUCURSAL, COORDINACION, folderPath);

                }
                rs = bd.ActualizaEstatusRegistro(NomArchivo, ID_SUCURSAL, numAsociadas, FechaCorte);


            }
            catch (Exception ex)
            {
                rs = "Error: " + ex.Message;
            }

            return rs;


        }
        public ActionResult RelacionCobro(int ID_PRESTAMO)
        {
            try
            {
                DateTime FechaCorte = new DateTime();
                string Fecha = bd.GetFechaCorte();
                FechaCorte = Convert.ToDateTime(Fecha);
                //ModelRelacionesCobroDatos obj = new ModelRelacionesCobroDatos();
                ERelacionCobro cliente = bd.ConsultaDatosRelacionCobro(ID_PRESTAMO, FechaCorte);
                //obj.listRelAsociada = cliente;
                List<EClientesFinales> CF = bd.ConsultaDatosCF(ID_PRESTAMO, FechaCorte);
                List<EClientesPP> PP = bd.ConsultaPP(ID_PRESTAMO, FechaCorte);
                List<EBonificacion> Bonificacion = bd.ConsultaBonificacion(ID_PRESTAMO, FechaCorte);
                List<EBonificacion> BonificacionMontos = bd.ConsultaMontosBonificacion(ID_PRESTAMO, FechaCorte);
                string promociones = bd.ObtenerPromociones(ID_PRESTAMO, FechaCorte);
                decimal promo = Convert.ToDecimal(promociones);
                string nivel = bd.ObtenerImagen(ID_PRESTAMO, FechaCorte);
                decimal totalMontoPrestamo = CF.Sum(clienteFinal => decimal.Parse(clienteFinal.SALDO_ACTUAL));
                decimal seguro = CF.Sum(seguroFinal => decimal.Parse(seguroFinal.MONTO_SEGURO));
                decimal pago = CF.Sum(pagoFinal => decimal.Parse(pagoFinal.MONTO_PAGO));
                ViewBag.ClientesFinales = CF;
                ViewBag.ClientesPP = PP;
                ViewBag.Bonificacion = Bonificacion;
                ViewBag.BonificacionMontos = BonificacionMontos;
                ViewBag.TotalMontoPrestamo = totalMontoPrestamo;
                ViewBag.TotalSeguro = seguro;
                ViewBag.TotalPago = pago;
                ViewBag.Promociones = promo;
                ViewBag.Nivel = nivel;
                //ViewBag.BytesCodigoBarrasOXXO = ObtenerCodigoBarras(Regex.Replace(cliente.CodBar, @"\s", ""), 120, 70);
                ViewBag.BytesCodigoBarrasPAYCASH = ObtenerCodigoBarras(cliente.PayCash, 120, 80);
                return PartialView(cliente);
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult RelacionCobroIndividual(int ID_PRESTAMO)
        {
            try
            {
                DateTime FechaCorte = new DateTime();
                string Fecha = bd.GetFechaCorte();
                FechaCorte = Convert.ToDateTime(Fecha);
                //ModelRelacionesCobroDatos obj = new ModelRelacionesCobroDatos();
                ERelacionCobro cliente = bd.ConsultaDatosRelacionCobro(ID_PRESTAMO, FechaCorte);
                //obj.listRelAsociada = cliente;
                List<EClientesFinales> CF = bd.ConsultaDatosCF(ID_PRESTAMO, FechaCorte);
                List<EClientesPP> PP = bd.ConsultaPP(ID_PRESTAMO, FechaCorte);
                List<EBonificacion> Bonificacion = bd.ConsultaBonificacion(ID_PRESTAMO, FechaCorte);
                List<EBonificacion> BonificacionMontos = bd.ConsultaMontosBonificacion(ID_PRESTAMO, FechaCorte);
                string promociones = bd.ObtenerPromociones(ID_PRESTAMO, FechaCorte);
                decimal promo = Convert.ToDecimal(promociones);
                string nivel = bd.ObtenerImagen(ID_PRESTAMO, FechaCorte);
                decimal totalMontoPrestamo = CF.Sum(clienteFinal => decimal.Parse(clienteFinal.SALDO_ACTUAL));
                decimal seguro = CF.Sum(seguroFinal => decimal.Parse(seguroFinal.MONTO_SEGURO));
                decimal pago = CF.Sum(pagoFinal => decimal.Parse(pagoFinal.MONTO_PAGO));
                //decimal monto = 
                ViewBag.ClientesFinales = CF;
                ViewBag.ClientesPP = PP;
                ViewBag.Bonificacion = Bonificacion;
                ViewBag.BonificacionMontos = BonificacionMontos;
                ViewBag.TotalMontoPrestamo = totalMontoPrestamo;
                ViewBag.TotalSeguro = seguro;
                ViewBag.TotalPago = pago;
                ViewBag.Promociones = promo;
                ViewBag.Nivel = nivel;
                //ViewBag.BytesCodigoBarrasOXXO = ObtenerCodigoBarras(Regex.Replace(cliente.CodBar, @"\s", ""), 120, 80);
                ViewBag.BytesCodigoBarrasPAYCASH = ObtenerCodigoBarras(cliente.PayCash, 120, 80);
                return new ViewAsPdf(cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public byte[] ObtenerCodigoBarras(string texto, int width, int height)
        {
            var stream = new MemoryStream();
            var margin = 0;
            var codeWriter = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = margin,
                    PureBarcode = false,

                }
            };

            var bitmap = codeWriter.Write(texto);
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            return stream.ToArray();
        }
        //public byte[] ObtenerCodigoBarras(string texto, int width, int height)
        //{
        //    Byte[] byteArray;
        //    var stream = new MemoryStream();
        //    try
        //    {

        //        var margin = 0;
        //        var codeWriter = new ZXing.BarcodeWriter
                //var codeWriter = new ZXing.BarcodeWriter
                //{
                //    Format = ZXing.BarcodeFormat.EAN_13,
                //    Options = new EncodingOptions
                //    {
                //        Height = height,
                //        Width = width,
                //        Margin = margin,
                //        PureBarcode = false,

                //    }
                //}

                //        BarcodeWriterPixelData writer = new BarcodeWriterPixelData()
                //        {
                //            Format = BarcodeFormat.CODE_128, 
                //            Options = new EncodingOptions
                //            {
                //                Height = 150,
                //                Width = 300,
                //                PureBarcode = false, // this should indicate that the text should be displayed, in theory. Makes no difference, though.
                //                Margin = 10

                //            }
                //        };


                //        //var pixelData = writer.Write(texto);
                //        PixelData pixelData = writer.Write(texto);

                //        using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                //        {
                //            using (var ms = new MemoryStream())
                //            {
                //                var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                //                try
                //                {
                //                    // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image
                //                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                //                }
                //                finally
                //                {
                //                    bitmap.UnlockBits(bitmapData);
                //                }
                //                // save to stream as PNG

                //                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                //                byteArray = ms.ToArray();
                //            }
                //        }

                //    }
                //    catch (Exception ex)
                //    {

                //        throw;
                //    }

                //    return byteArray;
                //} 
            public ActionResult GetPDF(int ID_ASOCIADA,string NOMBRE_ASOCIADA, string NOMBRE_SUCURSAL, string COORDINACION, string PATH)
            {
            string fileName = "RelacionesDeCobro_" +  ID_ASOCIADA + ".pdf";
            string fullPath = Path.Combine(PATH, fileName);

            //var strNJson = new
            //{
            //    ID_ASOCIADA = ID_ASOCIADA,
            //    NOMBRE_ASOCIADA = NOMBRE_ASOCIADA
            //};

            List<ERelaciones> lista = new List<ERelaciones>();
            ERelaciones va = new ERelaciones();
            va.NOMBRE_ASOCIADA = NOMBRE_ASOCIADA;
            va.ID_ASOCIADA = ID_ASOCIADA;
            lista.Add(va);

            var report = new ViewAsPdf(lista)
            {
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                PageSize = Rotativa.Options.Size.A4,
                PageMargins = new Margins(10, 10, 10, 10),
                

        };

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath); // Elimina el archivo si existe
            }

            var byteArray = report.BuildPdf(ControllerContext);
            using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(byteArray, 0, byteArray.Length);
            }
            return report;
        }


    }
}