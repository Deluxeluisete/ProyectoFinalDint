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
    /// Lógica de interacción para NuevaMarca.xaml
    /// </summary>
    public partial class NuevaMarca : Window
    {
        NuevaMarcaVM vm;
        public NuevaMarca()
        {
            InitializeComponent();
            vm = new NuevaMarcaVM();
            this.DataContext = vm;
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            vm.Aceptar();
        }
    }
}
