using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.Clases
{
    public class Pago
    {
        public int idAsignacion { get; set; }
        public int idSol { get; set; }
        public string fechaTermino { get; set; }
        public string rut { get; set; }
        public int idProveedor { get; set; }
        public string nomProveedor { get; set; }
        public int idCotizacion { get; set; }
        public int idProp { get; set; }
        public int idContrato { get; set; }
        public string propiedad { get; set; }
        public string servicio { get; set; }
        public string descripcion { get; set; }
        public int total { get; set; }
        public byte idEstado { get; set; }
        public string fecEstado { get; set; }
        public string estado { get; set; }
        public string rutCta { get; set; }
        public string nombreCta { get; set; }
        public int idBanco { get; set; }
        public int idTipoCta { get; set; }
        public string numCta { get; set; }
        public string nomArchFactura { get; set; }
        public string numFactura { get; set; }
        public int idFactura { get; set; }
        public string para { get; set; }
        public int id_tipo_proveedor { get; set; }
        public string tipo_proveedor { get; set; }
    }
    public class listascombobox
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public string periodo_dia { get; set; }
        public string periodo_mes { get; set; }
    }
    public class MiViewModel
    {
        public string Test { get; set; }
    }
}