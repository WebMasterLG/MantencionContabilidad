using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contabilidad.Clases
{
    public class PagoObservaciones
    {
        public int idAsignacion { get; set; }

        public List<Observacion> observaciones { get; set; }
    }
}