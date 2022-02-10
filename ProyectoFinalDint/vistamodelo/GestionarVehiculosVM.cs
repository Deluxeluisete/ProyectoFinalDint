using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ProyectoFinalDint.modelo;
using ProyectoFinalDint.servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint.vistamodelo
{
    class GestionarVehiculosVM : ObservableObject
    {
        RelayCommand AñadirClienteCommand { get; }

        RelayCommand EditarClienteCommand { get; }

        RelayCommand BorrarClienteCommand { get; }

        SQLiteRepositoryVehiculos ServicioSQLVehiculos;

        private ObservableCollection<Vehiculos> listaVehiculos;
        public ObservableCollection<Vehiculos> ListaVehiculos
        {
            get => listaVehiculos;
            set => SetProperty(ref listaVehiculos, value);
        }
        public GestionarVehiculosVM()
        {
            ServicioSQLVehiculos = new SQLiteRepositoryVehiculos();
            this.ListaVehiculos = ServicioSQLVehiculos.FindAll();
            AñadirClienteCommand = new RelayCommand(AñadirVehiculo);
            EditarClienteCommand = new RelayCommand(EditarVehiculo);
            BorrarClienteCommand = new RelayCommand(BorrarVehiculo);
        }

        private void BorrarVehiculo()
        {
            throw new NotImplementedException();
        }

        private void EditarVehiculo()
        {
            throw new NotImplementedException();
        }

        private void AñadirVehiculo()
        {
            throw new NotImplementedException();
        }
    }
}