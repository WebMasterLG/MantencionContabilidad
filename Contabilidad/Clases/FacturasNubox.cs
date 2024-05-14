using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.Clases
{
    public class ListadoCargaFacturas
    {
        public int id_factura { get; set; }
        public string nombre_cliente { get; set; }
        public string rut_cliente_formateado { get; set; }
        public string rut_cliente { get; set; }
        public string dv_cliente { get; set; }
        public string periodo { get; set; }
        public string g_periodo { get; set; }
        public string folio_factura { get; set; }
        public string g_archivo_factura { get; set; }
        public string estado_id { get; set; }
        public string g_estado { get; set; }
        public string neto { get; set; }
        public string iva { get; set; }
        public string total { get; set; }
        public string icono_mostrar { get; set; }
        public int idCotizacion { get; set; }
    }
}