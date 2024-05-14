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
    public class FacturacionModel
    {
        private static readonly string Mantenciones = Environment.GetEnvironmentVariable("MANTENCIONES", EnvironmentVariableTarget.Machine);
        public static string CambiarEstado(int idUsuario, string IdsActualizar, int idNuevoEstado)
        {
            string mensaje = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(Mantenciones))
                {
                    using (SqlCommand cmd = new SqlCommand("Mnt_Facturacion_Cambia_Estado ", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usu", SqlDbType.Int).Value = idUsuario;
                        cmd.Parameters.Add("@ids_actualizar", SqlDbType.VarChar).Value = IdsActualizar;
                        cmd.Parameters.Add("@id_estado", SqlDbType.Int).Value = idNuevoEstado;
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

        public static List<Factura> getFacturacion(int idUsuario, string inicio, string termino, byte idEstado)
        {
            List<Factura> oListaFacturas = new List<Factura>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Mantenciones))
                {
                    using (SqlCommand cmd = new SqlCommand("Mnt_Facturacion_Cliente_Consultar", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usu", SqlDbType.Int).Value = idUsuario;
                        cmd.Parameters.Add("@inicio", SqlDbType.VarChar).Value = inicio;
                        cmd.Parameters.Add("@termino", SqlDbType.VarChar).Value = termino;
                        cmd.Parameters.Add("@id_estado_facturacion", SqlDbType.TinyInt).Value = idEstado;
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Factura oFactura = new Factura();
                            oFactura.rut = reader["rut"].ToString();
                            oFactura.cliente = reader["cliente"].ToString();
                            oFactura.idProp = (int)reader["id_prop_arriendo"];
                            oFactura.idContrato = (int)reader["id_contrato"];
                            oFactura.propiedad = reader["propiedad"].ToString();
                            oFactura.idCotizacion = (int)reader["id_cotizacion"];
                            oFactura.fecAprobacion = reader["fecha_aprobacion"].ToString();
                            oFactura.glosaCotizacion = reader["cot_glosa"].ToString();
                            oFactura.neto = (int)reader["neto"];
                            oFactura.iva = (int)reader["iva"];
                            oFactura.total = (int)reader["cot_total"];
                            oFactura.idEstado = (byte)reader["id_estado_facturacion"];
                            oFactura.estado = reader["estado"].ToString();
                            oFactura.fecEstado = reader["f_estado_facturacion"].ToString();
                            oFactura.nombreArchivo = reader["nombre_archivo"].ToString();

                            oListaFacturas.Add(oFactura);
                        }

                    }
                    conn.Close();
                }
            }
            catch (Exception Ex)
            {
                var x = Ex.Message;

            }
            return oListaFacturas;
        }

        public static FacturaCotizacion updateFacturaCotizacion(int idUsuario,int idCotizacion, int folioFactura, string nomArchivo)
        {
            FacturaCotizacion factura = new FacturaCotizacion();

            try
            {
                using (SqlConnection conn = new SqlConnection(Mantenciones))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("Mnt_Factura_Cliente_Crear_Nubox ", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usu", SqlDbType.Int).Value = idUsuario;
                        cmd.Parameters.Add("@id_cotizacion", SqlDbType.Int).Value = idCotizacion;
                        cmd.Parameters.Add("@cot_num_fac_cliente", SqlDbType.Int).Value = folioFactura;
                        cmd.Parameters.Add("@cot_nom_archivo_fac", SqlDbType.VarChar).Value = nomArchivo;

                        SqlDataReader Resultados = cmd.ExecuteReader();
                        if (Resultados.Read())
                        {
                            factura.nombreCliente = Resultados["cliente"].ToString();
                        }
                        conn.Close();

                    }
                    conn.Close();
                }
            }
            catch (Exception Ex)
            {
                var mensaje = Ex.Message;
            }


            return factura;
        }

        public static List<HeaderExcelFormato> Llama_GeneraExcelHeader(int idUsuario, string grilla)
        {
            List<HeaderExcelFormato> lst = new List<HeaderExcelFormato>();
            string Llama_PA = "EXEC Mnt_Genera_EXCEL @id_usu, 0, @grilla";
            using (SqlConnection conn = new SqlConnection(Mantenciones))
            {
                conn.Open();
                SqlCommand comando = new SqlCommand(Llama_PA, conn);
                comando.Parameters.AddWithValue("@id_usu", idUsuario);
                comando.Parameters.AddWithValue("@grilla", grilla);
                SqlDataReader Resultados = comando.ExecuteReader();
                while (Resultados.Read())
                {
                    lst.Add(new HeaderExcelFormato
                    {
                        id = (int)Resultados["ID"],
                        nombre = Resultados["Nombre"].ToString()
                    });
                }
                conn.Close();
            }
            return lst;
        }
        public static DataSet Llama_GeneraExcel(int idUsuario, int idFormato, string grilla, string inicio, string termino, int id_estado_facturacion, string fechaContable, string idsSeleccion, int idProveedor)
        {
            DataSet ds = new DataSet();
            List<HeaderExcelFormato> lst = new List<HeaderExcelFormato>();
            using (SqlConnection conn = new SqlConnection(Mantenciones))
            {
                using (SqlCommand cmd = new SqlCommand("Mnt_Genera_EXCEL", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_usu", idUsuario);
                    cmd.Parameters.AddWithValue("@id_formato", idFormato);
                    cmd.Parameters.AddWithValue("@grilla", grilla);
                    cmd.Parameters.AddWithValue("@inicio", inicio);
                    cmd.Parameters.AddWithValue("@termino", termino);
                    cmd.Parameters.AddWithValue("@fecha_contable", fechaContable);
                    cmd.Parameters.AddWithValue("@id_estado", id_estado_facturacion);
                    cmd.Parameters.AddWithValue("@ids_seleccion", idsSeleccion);
                    cmd.Parameters.AddWithValue("@id_proveedor", idProveedor);

                    conn.Open();
                    SqlDataAdapter adp = new SqlDataAdapter();
                    adp.SelectCommand = cmd;
                    adp.Fill(ds);
                }
            }
            return ds;
        }



    }
}