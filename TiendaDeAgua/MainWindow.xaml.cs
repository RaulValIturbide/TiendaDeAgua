using Microsoft.Data.Sqlite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TiendaDeAgua
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ComunServicioAiron.Conectar.ActivarConexion();


            string sql = " INSERT INTO usuarios(nombre,contrasenya) " +
                         " VALUES(@pNombre,@pContrasenya) ";

            var parametros = new SqliteParameter[]
                {
                    new SqliteParameter("@pNombre","Raul"),
                    new SqliteParameter("@pContrasenya","1234")
                };
            int filasAfectadas = ComunServicioAiron.Conectar.EjecutarNonQuery(sql, parametros);
            if(filasAfectadas > 0)
            {
                MessageBox.Show("USUARIO AGREGADO");
            }
            else
            {
                MessageBox.Show("ERROR");
            }

            
            
        }



    }
}