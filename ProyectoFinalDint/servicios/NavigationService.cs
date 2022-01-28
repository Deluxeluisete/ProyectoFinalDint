using ProyectoFinalDint.vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint.servicios
{
    class NavigationService
    {
        public bool? AbrirDialogoNuevaMarca()
        {
            NuevaMarca dialogo = new NuevaMarca();
            return dialogo.ShowDialog();
        }
    }
}
