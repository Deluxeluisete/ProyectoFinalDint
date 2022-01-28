using Microsoft.Toolkit.Mvvm.Input;
using ProyectoFinalDint.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint
{
    class MainWindowVM
    {
        NavigationService servicioNavegacion;
        public RelayCommand NuevaMarcaButtonCommand { get; }

        public MainWindowVM()
        {
            servicioNavegacion = new NavigationService();
            NuevaMarcaButtonCommand = new RelayCommand(AbrirDialogoNuevaMarca);
        }

        private void AbrirDialogoNuevaMarca()
        {
            servicioNavegacion.AbrirDialogoNuevaMarca();
        }
    }
}
