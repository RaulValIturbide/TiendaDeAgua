using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TiendaDeAgua.DTOs;
using TiendaDeAgua.Tablas;

namespace TiendaDeAgua.interfaz
{
    public partial class InformeCliente : Window
    {
        public InformeCliente()
        {
            InitializeComponent();

            // Obtenemos la lista de clientes, ordenando por ID por defecto
            List<ClienteDTO> clientes = ClienteBD.ListaCliente("id");

            // Generamos el FlowDocument
            lectorDocumento.Document = CrearFlowDocument(clientes);
        }

        private FlowDocument CrearFlowDocument(List<ClienteDTO> clientes)
        {
            FlowDocument doc = new FlowDocument();
            doc.PagePadding = new Thickness(20);
            doc.ColumnWidth = 800;

            // Título
            Paragraph titulo = new Paragraph(new Run("Informe de Clientes"));
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
            for (int i = 0; i < 4; i++)
                tabla.Columns.Add(new TableColumn());

            // Fila de encabezado
            TableRowGroup headerGroup = new TableRowGroup();
            TableRow headerRow = new TableRow();
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("ID"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Nombre"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Contacto"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Dirección"))) { FontWeight = FontWeights.Bold, TextAlignment = TextAlignment.Center });
            headerGroup.Rows.Add(headerRow);
            tabla.RowGroups.Add(headerGroup);

            // Filas de datos
            TableRowGroup bodyGroup = new TableRowGroup();
            bool filaPar = false;
            foreach (var c in clientes)
            {
                TableRow row = new TableRow();
                filaPar = !filaPar;
                // Color alterno
                row.Background = filaPar ? System.Windows.Media.Brushes.LightGray : System.Windows.Media.Brushes.White;

                row.Cells.Add(new TableCell(new Paragraph(new Run(c.ID.ToString()))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(c.Nombre))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(c.Contacto ?? ""))) { Padding = new Thickness(5) });
                row.Cells.Add(new TableCell(new Paragraph(new Run(c.Direccion ?? ""))) { Padding = new Thickness(5) });

                bodyGroup.Rows.Add(row);
            }
            tabla.RowGroups.Add(bodyGroup);

            doc.Blocks.Add(tabla);
            return doc;
        }
    }
}