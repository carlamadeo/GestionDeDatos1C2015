using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using PagoElectronico.DB;

namespace PagoElectronico.Facturacion
{
    class FacturacionHelper
    {
        public static List<Facturacion> getListFacturacionesPendientes(Int16 idCliente)
        {
            List<Facturacion> lista = new List<Facturacion>();

            SqlConnection conn = Connection.getConnection();

            string query = @"SELECT fp.Id_Facturacion_Pendiente, fp.Id_Cuenta, fp.Fecha,
            fp.Importe, ti.Descripcion, fp.Id_Referencia FROM SQL_SERVANT.Facturacion_Pendiente fp 
            INNER JOIN SQL_SERVANT.Tipo_Item ti ON fp.Id_Tipo_Item = ti.Id_Tipo_Item
            INNER JOIN SQL_SERVANT.Cliente_Cuenta cc ON cc.Id_Cuenta = fp.Id_Cuenta
            WHERE cc.Id_Cliente = " + idCliente;

            SqlCommand command = new SqlCommand(query, conn);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(getFacturacion(reader));
            }

            for (int i = 0; i < lista.Count; i++)
            {
                lista[i].suscripciones = 1;
            }

            return lista;
        }

        private static Facturacion getFacturacion(IDataReader reader)
        {
            Facturacion facturacion = new Facturacion();

            facturacion.id = Convert.ToInt32(reader["Id_Facturacion_Pendiente"]);
            facturacion.idCuenta = Convert.ToDecimal(reader["Id_Cuenta"]);
            facturacion.fecha = Convert.ToDateTime(reader["Fecha"]);
            facturacion.importe = Convert.ToDecimal(reader["Importe"]);
            facturacion.descripcionGasto = Convert.ToString(reader["Descripcion"]);
            int? idReferencia = reader["Id_Referencia"] as int?;
            if (idReferencia == null)
                facturacion.idReferencia = "--";
            else
                facturacion.idReferencia = Convert.ToString(idReferencia);

            return facturacion;
        }

        public static void fillDGVFacturaciones(DataGridView dgv, List<Facturacion> list)
        {
            if (list.Count != 0)
            {
                dgv.DataSource = list;
                dgv.RowHeadersVisible = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.AllowUserToAddRows = false;
                dgv.Columns[0].Width = 25;
                dgv.Columns[1].Width = 120;
                dgv.Columns[2].Width = 120;
                dgv.Columns[3].Width = 200;
                dgv.Columns[0].HeaderCell.Value = "Id";
                dgv.Columns[1].HeaderCell.Value = "N° Referencia";
                dgv.Columns[2].HeaderCell.Value = "Cuenta";
                dgv.Columns[3].HeaderCell.Value = "Detalle";
                dgv.Columns[4].HeaderCell.Value = "Fecha";
                dgv.Columns[5].HeaderCell.Value = "Importe";
                dgv.Columns[6].HeaderCell.Value = "Suscripciones";
            }
        }

        public static Decimal getImporteAPagar(List<Facturacion> facturacionesAPagar)
        {
            Decimal importeTotal = 0;
            int indice = 0;

            foreach (Facturacion item in facturacionesAPagar)
            {
                importeTotal = importeTotal + facturacionesAPagar[indice].importe *
                    facturacionesAPagar[indice].suscripciones;
                indice++;
            }

            return importeTotal;
        }

        public static void saveFacturacion(List<Facturacion> facturacionesAPagar, Int16 idCliente, string tarjeta)
        {
            Decimal importeTotal =  getImporteAPagar(facturacionesAPagar);

            foreach (Facturacion item in facturacionesAPagar)
            {
                eliminarFacturacionesPendientes(item);
            }

            Int32 idFactura = crearFacturacion(idCliente, tarjeta, importeTotal);

            foreach (Facturacion item in facturacionesAPagar)
            {
                saveFacturacionesItem(item, idFactura);
            }

            facturacionesAPagar.Clear();
        }

        private static void eliminarFacturacionesPendientes(Facturacion item)
        {
            SqlCommand sp_remove_pending_facturacion = new SqlCommand();
            sp_remove_pending_facturacion.CommandType = CommandType.StoredProcedure;
            sp_remove_pending_facturacion.CommandText = "SQL_SERVANT.sp_remove_pending_facturacion";

            sp_remove_pending_facturacion.Parameters.AddWithValue("@p_facturacion_pendiente_id", item.id);

            ProcedureHelper.execute(sp_remove_pending_facturacion, "remove pending facturas", false);
        }

        private static Int32 crearFacturacion(Int16 idCliente, string tarjeta, Decimal importe)
        {
            SqlCommand sp_create_factura = new SqlCommand();
            sp_create_factura.CommandText = "SQL_SERVANT.sp_create_factura";
            sp_create_factura.Parameters.Add(new SqlParameter("@p_id_cliente", SqlDbType.Int));
            sp_create_factura.Parameters["@p_id_cliente"].Value = idCliente;
            sp_create_factura.Parameters.Add(new SqlParameter("@p_fecha", SqlDbType.DateTime));
            sp_create_factura.Parameters["@p_fecha"].Value = DateHelper.getToday();
            sp_create_factura.Parameters.Add(new SqlParameter("@p_id_tarjeta", SqlDbType.Decimal));
            sp_create_factura.Parameters["@p_id_tarjeta"].Value = Convert.ToDecimal(tarjeta);
            sp_create_factura.Parameters.Add(new SqlParameter("@p_importe", SqlDbType.Decimal));
            sp_create_factura.Parameters["@p_importe"].Value = importe;

            SqlParameter returnParametersIdFactura = new SqlParameter("@p_id_factura", SqlDbType.BigInt);
            sp_create_factura.Parameters.Add(returnParametersIdFactura);
            returnParametersIdFactura.Direction = ParameterDirection.InputOutput;

            ProcedureHelper.execute(sp_create_factura, "crear factura", false);

            return Convert.ToInt32(returnParametersIdFactura.Value);
        }

        private static void saveFacturacionesItem(Facturacion item, Int32 idFactura)
        {
            SqlCommand sp_create_facturacion_item = new SqlCommand();
            sp_create_facturacion_item.CommandType = CommandType.StoredProcedure;
            sp_create_facturacion_item.CommandText = "SQL_SERVANT.sp_create_facturacion_item";

            sp_create_facturacion_item.Parameters.AddWithValue("@p_id_factura", idFactura);
            sp_create_facturacion_item.Parameters.AddWithValue("@p_id_cuenta", item.idCuenta);
            sp_create_facturacion_item.Parameters.AddWithValue("@p_descripcion_gasto", item.descripcionGasto);
            sp_create_facturacion_item.Parameters.AddWithValue("@p_importe", item.importe);
            sp_create_facturacion_item.Parameters.AddWithValue("@p_cantidad_suscripciones", item.suscripciones);
            sp_create_facturacion_item.Parameters.AddWithValue("@p_id_referencia", item.idReferencia);

            ProcedureHelper.execute(sp_create_facturacion_item, "create facturacion item", false);
        }
    }
}
