using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using TiendaDeAgua.DTOs;
using TiendaDeAgua.Tablas;

namespace TiendaDeAgua.interfaz
{
    public partial class InformeProducto : Window
    {
        public InformeProducto()
        {
            InitializeComponent();

            // Obtenemos la lista de productos
            List<ProductoDTO> productos = ProductoBD.ListaProducto();

            // Generamos el FlowDocument
            lectorDocumento.Document = CrearFlowDocument(productos);
        }

        private FlowDocument CrearFlowDocument(List<ProductoDTO> productos)
        {
            FlowDocument doc = new FlowDocument();            
            doc.PagePadding = new Thickness(20);
            doc.ColumnWidth = 800;

            // Título
            Paragraph titulo = new Paragraph(new Run("Informe de Productos"));
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

            // Fila de encabezado
            TableRowGroup headerGroup = new TableRowGroup();
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("ID"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Nombre"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Precio"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Stock"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Categoria"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerGroup.Rows.Add(headerRow);
            tabla.RowGroups.Add(headerGroup);

            // Filas de datos
            TableRowGroup bodyGroup = new TableRowGroup();
            bool filaPar = false;
            foreach (var p in productos)
            {
                TableRow row = new TableRow();
                filaPar = !filaPar;
                row.Background = filaPar ? System.Windows.Media.Brushes.LightGray : System.Windows.Media.Brushes.White;

                row.Cells.Add(new TableCell(new Paragraph(new Run(p.ID.ToString()))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(p.Nombre))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(p.Precio.ToString("C2")))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(p.Stock.ToString()))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(p.CategoriaNombre))) { Padding = new Thickness(5) });

                bodyGroup.Rows.Add(row);
            }
            tabla.RowGroups.Add(bodyGroup);

            doc.Blocks.Add(tabla);
            return doc;
        }
    }
}