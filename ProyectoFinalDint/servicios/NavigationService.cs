using ProyectoFinalDint.vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProyectoFinalDint.servicios
{
    class NavigationService
    {
        public bool? AbrirDialogoNuevaMarca()
        {
            NuevaMarca dialogo = new NuevaMarca();
            return dialogo.ShowDialog();
        }
        internal UserControl GestionarClientesUC() => new GestionarClientesUserControl();
        internal UserControl GestionarVehiculosUC() => new GestionarVehiculosUserControl();
        internal UserControl GestionarEstacionamientosUC() => new GestionarAparcamientosUserControl();

    }
}
