using ProyectoFinalDint.vistamodelo;
using System;
using System.Collections.Generic;
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

namespace ProyectoFinalDint.vistas
{
    /// <summary>
    /// Lógica de interacción para NuevoCliente.xaml
    /// </summary>
    public partial class NuevoCliente : Window
    {
        NuevoClienteVM vm;
        public NuevoCliente()
        {
            InitializeComponent();
            vm = new NuevoClienteVM();
            this.DataContext = vm;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            vm.Aceptar();
            this.DialogResult = true;
        }
    }
}
