using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ProyectoFinalDint.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProyectoFinalDint
{
    class MainWindowVM : ObservableObject
    {
        NavigationService servicioNavegacion;
        private UserControl userControl;
        public UserControl UserControl
        {
            get => userControl;
            set => SetProperty(ref userControl, value);
        }


        public RelayCommand NuevaMarcaButtonCommand { get; }
        public RelayCommand GestionaClientesCommand { get; }
        public RelayCommand GestionaVehiculosCommand { get; }

        public MainWindowVM()
        {
            servicioNavegacion = new NavigationService();
            NuevaMarcaButtonCommand = new RelayCommand(AbrirDialogoNuevaMarca);
            GestionaClientesCommand = new RelayCommand(MuestraGestionaClientesCommand);
            GestionaVehiculosCommand = new RelayCommand(MuestraGestionaVehiculosCommand);
        }

        private void MuestraGestionaVehiculosCommand()
        {
            this.UserControl = servicioNavegacion.GestionarVehiculosUC();
        }

        private void MuestraGestionaClientesCommand()
        {
            this.UserControl = servicioNavegacion.GestionarClientesUC();
        }

        private void AbrirDialogoNuevaMarca()
        {
            servicioNavegacion.AbrirDialogoNuevaMarca();
        }
    }
}
