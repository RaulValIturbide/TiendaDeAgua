using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using TiendaDeAgua.DTOs;
using TiendaDeAgua.Tablas;

namespace TiendaDeAgua.interfaz
{
    public partial class InformePedido : Window
    {
        public InformePedido(string filtro = "id", int estadoID = 0)
        {
            InitializeComponent();

            List<PedidoDTO> pedidos = PedidoBD.ListaPedidoTodo(filtro, estadoID);

            // Creamos el FlowDocument
            lectorDocumento.Document = CrearFlowDocument(pedidos);
        }

        private FlowDocument CrearFlowDocument(List<PedidoDTO> pedidos)
        {
            FlowDocument doc = new FlowDocument();
            doc.PagePadding = new Thickness(20);
            doc.ColumnWidth = 800;

            // Título
            Paragraph titulo = new Paragraph(new Run("Informe de Pedidos"));
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
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Cliente"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Fecha"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Total"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Estado"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerGroup.Rows.Add(headerRow);
            tabla.RowGroups.Add(headerGroup);

            // Datos
            TableRowGroup bodyGroup = new TableRowGroup();
            bool filaPar = false;
            foreach (var p in pedidos)
            {
                TableRow row = new TableRow();
                filaPar = !filaPar;
                row.Background = filaPar ? System.Windows.Media.Brushes.LightGray : System.Windows.Media.Brushes.White;

                row.Cells.Add(new TableCell(new Paragraph(new Run(p.ID.ToString()))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(p.ClienteNombre))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(p.Fecha))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(p.Total.ToString("C2")))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(p.EstadoNombre))) { Padding = new Thickness(5) });

                bodyGroup.Rows.Add(row);
            }
            tabla.RowGroups.Add(bodyGroup);

            doc.Blocks.Add(tabla);
            return doc;
        }
    }
}