using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.Clases
{
    public class Factura
    {
        public string rut { get; set; }
        public string cliente { get; set; }
        public int idProp { get; set; }
        public int idContrato { get; set; }
        public string propiedad { get; set; }
        public int idCotizacion { get; set; }
        public string fecAprobacion { get; set; }
        public string glosaCotizacion { get; set; }
        public int neto { get; set; }
        public int iva { get; set; }
        public int total { get; set; }
        public byte idEstado { get; set; }
        public string estado { get; set; }
        public string fecEstado { get; set; }
        public string nombreArchivo { get; set; }
        
    }
}