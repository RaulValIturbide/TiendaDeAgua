using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TiendaDeAgua.Tablas;

namespace TiendaDeAgua.Informes
{
    /// <summary>
    /// Lógica de interacción para ProductoReport.xaml
    /// </summary>
    public partial class ProductoReport : Window
    {
        public ProductoReport()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ProductoReport_Loaded);
        }

        private void ProductoReport_Loaded(object sender, RoutedEventArgs e)
        {
            string reportPath = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Informes",
                "ProductoReport.rdlc"
            );

            System.Windows.MessageBox.Show("RDLC existe: " + File.Exists(reportPath));

            var lista = ProductoBD.ListaProducto();

            System.Windows.MessageBox.Show("Productos cargados: " + (lista?.Count ?? -1));

            reportViewer.Reset();
            reportViewer.ProcessingMode = BoldReports.UI.Xaml.ProcessingMode.Local;
            reportViewer.ReportPath = reportPath;

            var dataSource = new BoldReports.Windows.ReportDataSource("ProductoDataSet", lista);

            reportViewer.DataSources.Clear();
            reportViewer.DataSources.Add(dataSource);

            reportViewer.RefreshReport();
        }



    }
}
