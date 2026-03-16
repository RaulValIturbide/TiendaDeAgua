using Microsoft.Reporting.WinForms;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Reportes
{
    public partial class Form1 : Form
    {
        private static readonly string rutaBD = Path.GetFullPath("..\\..\\..\\data\\database\\airon.db");
        public Form1()
        {
            InitializeComponent();
            CargarInforme();   // ← AÑADIDO
        }

        private void CargarInforme()
        {
            // Nombre EXACTO del RDLC incrustado
            reportViewer1.LocalReport.ReportEmbeddedResource =
                "Reportes.InformeProducto.rdlc";

            // Cargar datos desde SQLite
            DataTable tabla = ObtenerProductos();
           

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(
                new ReportDataSource("DataSet1", tabla)
            );

            reportViewer1.RefreshReport();
        }

        private DataTable ObtenerProductos()
        {
            DataTable tabla = new DataTable();

            using (var con = new SqliteConnection($"Data Source={rutaBD}"))
            {
                con.Open();

                using (var cmd = new SqliteCommand(
                    " SELECT ID, Nombre, Precio, Stock, CategoriaID " +
                    " FROM Producto", con))
                using (var reader = cmd.ExecuteReader())
                {
                    tabla.Load(reader);
                }
            }

            return tabla;
        }
    }
}
