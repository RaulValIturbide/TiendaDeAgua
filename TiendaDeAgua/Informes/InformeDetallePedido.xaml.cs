using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using TiendaDeAgua.DTOs;
using TiendaDeAgua.Tablas;

namespace TiendaDeAgua.interfaz
{
    public partial class InformeDetallePedido : Window
    {
        public InformeDetallePedido(int pedidoID)
        {
            InitializeComponent();

            List<DetallePedidoDTO> detalles = DetallePedidoBD.ListaDetallePedido(pedidoID);
            
            // Creamos el FlowDocument
            lectorDocumento.Document = CrearFlowDocument(detalles);

        }

        private FlowDocument CrearFlowDocument(List<DetallePedidoDTO> detalles)
        {
            FlowDocument doc = new FlowDocument();
            doc.PagePadding = new Thickness(20);
            doc.PageWidth = 800;
            doc.PageHeight = 1120; 
            doc.ColumnWidth = double.PositiveInfinity;
            doc.ColumnGap = 0;

            // Título
            Paragraph titulo = new Paragraph(new Run("Informe Detalle de Pedido"));
            titulo.FontSize = 24;
            titulo.FontWeight = FontWeights.Bold;
            titulo.TextAlignment = TextAlignment.Center;
            titulo.Margin = new Thickness(0, 0, 0, 20);
            doc.Blocks.Add(titulo);

            // Tabla
            Table tabla = new Table();
            tabla.CellSpacing = 2;
            tabla.BorderBrush = System.Windows.Media.Brushes.Black;
            tabla.BorderThickness = new Thickness(1);

            // Columnas 
            for (int i = 0; i < 5; i++)
                tabla.Columns.Add(new TableColumn());

            // Encabezado
            TableRowGroup headerGroup = new TableRowGroup();
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("ID"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Producto"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Cantidad"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Precio Unitario"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Total"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerGroup.Rows.Add(headerRow);
            tabla.RowGroups.Add(headerGroup);

            // Datos
            TableRowGroup bodyGroup = new TableRowGroup();
            bool filaPar = false;
            foreach (var d in detalles)
            {
                TableRow row = new TableRow();
                filaPar = !filaPar;
                row.Background = filaPar ? System.Windows.Media.Brushes.LightGray : System.Windows.Media.Brushes.White;

                row.Cells.Add(new TableCell(new Paragraph(new Run(d.ID.ToString()))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(d.ProductoNombre ?? ""))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(d.Cantidad.ToString()))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(d.PrecioUnitario.ToString("C2")))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run((d.Cantidad * d.PrecioUnitario).ToString("C2")))) { Padding = new Thickness(5) });

                bodyGroup.Rows.Add(row);
            }
            tabla.RowGroups.Add(bodyGroup);

            doc.Blocks.Add(tabla);
            return doc;
        }
    }
}