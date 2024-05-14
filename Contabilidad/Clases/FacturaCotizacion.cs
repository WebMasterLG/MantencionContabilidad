using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.Clases
{
    public class FacturaCotizacion
    {
        public int idCotizacion { get; set; }
        public int folioFactura { get; set; }
        public string nombreCliente { get; set; }
        public string nombreArchivo { get; set; }
        public int neto { get; set; }
        public int iva { get; set; }
        public int total { get; set; }
    }
}