using Contabilidad.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Contabilidad.Models
{
    public class PagosModel
    {
        private static readonly string Mantenciones = Environment.GetEnvironmentVariable("MANTENCIONES", EnvironmentVariableTarget.Machine);
        private static readonly string LECAROS_CORE = Environment.GetEnvironmentVariable("CORE", EnvironmentVariableTarget.Machine);


        public static List<Pago> getPagosProveedores(int idUsuario, string inicio, string termino, byte idEstado, int idProveedor, int tipo_proveedor)
        {
            List<Pago> oListaPagos = new List<Pago>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Mantenciones))
                {
                    using (SqlCommand cmd = new SqlCommand("Mnt_Pagos_Proveedores_Consultar_NEW", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usu", SqlDbType.Int).Value = idUsuario;
                        cmd.Parameters.Add("@inicio", SqlDbType.VarChar).Value = inicio;
                        cmd.Parameters.Add("@termino", SqlDbType.VarChar).Value = termino;
                        cmd.Parameters.Add("@id_estado", SqlDbType.TinyInt).Value = idEstado;
                        cmd.Parameters.Add("@id_proveedor", SqlDbType.TinyInt).Value = idProveedor;
                        cmd.Parameters.Add("@tipo_proveedor", SqlDbType.TinyInt).Value = tipo_proveedor; 
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Pago oPago = new Pago();
                            oPago.idAsignacion = (int)reader["id_asignacion"];
                            oPago.idSol = (int)reader["id_sol"];
                            oPago.fechaTermino = reader["f_termino_trabajo"].ToString();
                            oPago.rut = reader["rut"].ToString();
                            oPago.nomProveedor = reader["nombre_proveedor"].ToString();
                            oPago.idCotizacion = (int)reader["id_cotizacion"];
                            oPago.idProp = (int)reader["id_prop"];
                            oPago.idContrato = (int)reader["id_contrato"];
                            oPago.propiedad = reader["propiedad"].ToString();
                            oPago.servicio = reader["nombre_servicio"].ToString();
                            oPago.descripcion = reader["descripcion"].ToString();
                            oPago.total = (int)reader["monto_servicio"];
                            oPago.idEstado = (byte)reader["id_estado_pago"];
                            oPago.estado = reader["estado"].ToString();
                            oPago.fecEstado = reader["f_estado_pago"].ToString();
                            oPago.idProveedor = (int)reader["id_proveedor"];
                            oPago.rutCta = reader["rut_cta"].ToString();
                            oPago.nombreCta = reader["nombre_cta"].ToString();
                            oPago.idBanco = (int)reader["id_banco"];
                            oPago.idTipoCta = (byte)reader["id_tipo_cta"];
                            oPago.numCta = reader["numero_cuenta"].ToString();
                            oPago.nomArchFactura = reader["nom_arch_factura"].ToString();
                            oPago.numFactura = reader["num_factura"].ToString();
                            oPago.idFactura = (int)reader["id_factura"];
                            oPago.para = reader["para"].ToString();
                            oPago.id_tipo_proveedor = (int)reader["id_tipo_proveedor"];
                            oPago.tipo_proveedor = reader["tipo_proveedor"].ToString();
                            oListaPagos.Add(oPago);
                        }

                    }
                    conn.Close();
                }
            }
            catch (Exception Ex)
            {
                var x = Ex.Message;

            }
            return oListaPagos;
        }

        public static string CambiarEstado(int idUsuario, string IdsActualizar, int idNuevoEstado, string fechaNomina)
        {
            string mensaje = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(Mantenciones))
                {
                    using (SqlCommand cmd = new SqlCommand("Mnt_Pago_Cambia_Estado ", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usu", SqlDbType.Int).Value = idUsuario;
                        cmd.Parameters.Add("@ids_actualizar", SqlDbType.VarChar).Value = IdsActualizar;
                        cmd.Parameters.Add("@id_estado", SqlDbType.Int).Value = idNuevoEstado;
                        cmd.Parameters.Add("@fec_nomina", SqlDbType.VarChar).Value = fechaNomina;
                        conn.Open();

                        cmd.ExecuteNonQuery();

                    }
                    conn.Close();
                }
            }
            catch (Exception Ex)
            {
                mensaje = Ex.Message;

            }
            return mensaje;
        }

        public static List<listascombobox> Llama_core_cargar_lista(int id_usu, int lista, string orden) //Trae una propiedad
        {
            List<listascombobox> lst = new List<listascombobox>();
            string Llama_PA = "EXEC core_cargar_lista @id_usu, @lista, @orden";
            using (SqlConnection conn = new SqlConnection(LECAROS_CORE))
            {
                conn.Open();
                SqlCommand comando = new SqlCommand(Llama_PA, conn);
                comando.Parameters.AddWithValue("@id_usu", id_usu);
                comando.Parameters.AddWithValue("@lista", lista);
                comando.Parameters.AddWithValue("@orden", orden);
                SqlDataReader Resultados = comando.ExecuteReader();
                if (Resultados.HasRows)
                {
                    while (Resultados.Read())
                    {
                        lst.Add(new listascombobox
                        {
                            id = int.Parse(Resultados["id"].ToString()),
                            descripcion = Resultados["descripcion"].ToString()
                        });
                    }
                }
                conn.Close();
            }
            return lst;
        }
        public static PagoObservaciones getPagosObservaciones(int idUsuario, int idTipo, int idAsignacion)
        {
            PagoObservaciones oPagoObs = new PagoObservaciones();
            oPagoObs.observaciones = new List<Observacion>();
            oPagoObs.idAsignacion = idAsignacion;
            try
            {
                using (SqlConnection conn = new SqlConnection(Mantenciones))
                {
                    using (SqlCommand cmd = new SqlCommand("Mnt_observaciones_Cargar_Lista ", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usu", SqlDbType.Int).Value = idUsuario;
                        cmd.Parameters.Add("@id_tipo", SqlDbType.Int).Value = idTipo;    
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = idAsignacion;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Observacion oObservacion = new Observacion();
                            oObservacion.fecha = reader["fecha"].ToString();
                            oObservacion.nomUsuario = reader["nombre"].ToString();
                            oObservacion.observaciones = reader["observaciones"].ToString();

                            oPagoObs.observaciones.Add(oObservacion);
                        }


                    }
                    conn.Close();
                }
            }
            catch (Exception Ex)
            {
                var x = Ex.Message;

            }
            return oPagoObs;
        }


        public static string addObservacion(int idUsuario, int idTipo, int idAsignacion, string obs)
        {
            string mensaje = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(Mantenciones))
                {
                    using (SqlCommand cmd = new SqlCommand("Mnt_observaciones_ingresa", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usu", SqlDbType.Int).Value = idUsuario;
                        cmd.Parameters.Add("@id_tipo", SqlDbType.TinyInt).Value = idTipo;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = idAsignacion;
                        cmd.Parameters.Add("@g_obs", SqlDbType.VarChar).Value = obs;
                        conn.Open();

                        cmd.ExecuteNonQuery();

                    }
                    conn.Close();
                }
            }
            catch (Exception Ex)
            {
                mensaje = Ex.Message;

            }
            return mensaje;
        }


        public static List<Proveedor> getListaProveedores(int idUsuario)
        {
            List<Proveedor> oListaProveedores = new List<Proveedor>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Mantenciones))
                {
                    using (SqlCommand cmd = new SqlCommand("Mnt_Proveedor_Cargar ", conn))
                    {
                        conn.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usu", SqlDbType.Int).Value = idUsuario;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Proveedor oProveedor = new Proveedor();
                            oProveedor.idProveedor = (int)reader["id_proveedor"];
                            oProveedor.nombreProveedor = reader["nombre_proveedor"].ToString();

                            oListaProveedores.Add(oProveedor);
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception Ex)
            {
                var x = Ex.Message;

            }
            return oListaProveedores;
        }

    }

}