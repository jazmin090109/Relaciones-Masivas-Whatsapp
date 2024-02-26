using CREC_MVC.Models.AccesoBD.RelacionesCobro;
using CREC_MVC.Models.Entidades.RelacionesCobro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

using ZXing.Common;

namespace GeneradorRelacionesCobroMasivas.Controllers.RelacionCobro
{
    public class RelacionCobroController : Controller
    {
        // GET: RelacionCobro
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RelacionCobro()
        {
            DateTime FechaCorte = new DateTime();
            int ID_PRESTAMO = 933169;
            FechaCorte = Convert.ToDateTime("2022/12/07");
            BDRelacionesCobro bd = new BDRelacionesCobro();
            //ModelRelacionesCobroDatos obj = new ModelRelacionesCobroDatos();
            ERelacionCobro cliente = bd.ConsultaDatosRelacionCobro(ID_PRESTAMO, FechaCorte);
            //obj.listRelAsociada = cliente;
            List<EClientesFinales> CF = bd.ConsultaDatosCF(ID_PRESTAMO, FechaCorte);
            List<EClientesPP> PP = bd.ConsultaPP(ID_PRESTAMO, FechaCorte);
            List<EBonificacion> Bonificacion = bd.ConsultaBonificacion(ID_PRESTAMO, FechaCorte);
            List<EBonificacion> BonificacionMontos = bd.ConsultaMontosBonificacion(ID_PRESTAMO, FechaCorte);
            ViewBag.ClientesFinales = CF;
            ViewBag.ClientesPP = PP;
            ViewBag.Bonificacion = Bonificacion;
            ViewBag.BonificacionMontos = BonificacionMontos;
            ViewBag.BytesCodigoBarrasOXXO = ObtenerCodigoBarras(Regex.Replace(cliente.CodBar, @"\s", ""), 70, 50);
            ViewBag.BytesCodigoBarrasPAYCASH= ObtenerCodigoBarras(Regex.Replace(cliente.PayCash, @"\s", ""), 70, 50); ;
            return View(cliente);
        }
        public byte[] ObtenerCodigoBarras(string texto, int width, int height)
        {
            var stream = new MemoryStream();
            var margin = 0;
            var codeWriter = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.ITF,
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

    }
}