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
        private Estacionamientos plazaActual;
        public Estacionamientos PlazaActual
        {
            get => plazaActual;
            private set => plazaActual = value;
        }
        private Clientes clienteActual;
        public Clientes ClienteActual
        {
            get => clienteActual;
            private set => SetProperty(ref clienteActual, value);
        }
        private Vehiculos vehiculoActual;
        public Vehiculos VehiculoActual
        {
            get => vehiculoActual;
            set
            {
                SetProperty(ref vehiculoActual, value);
                this.ClienteActual = new SQLiteRepositoryClientes().FindById(this.VehiculoActual.Id_cliente);
                this.PlazaActual = new SQLiteRepositoryEstacionamientos().FindByMatricula(this.VehiculoActual.Matricula);
            }
        }
        private ObservableCollection<Vehiculos> listaVehiculos;
        public ObservableCollection<Vehiculos> ListaVehiculos
        {
            get => listaVehiculos;
            set => SetProperty(ref listaVehiculos, value);
        }
        RelayCommand AñadirClienteCommand { get; }

        RelayCommand EditarClienteCommand { get; }

        RelayCommand BorrarClienteCommand { get; }

        SQLiteRepositoryVehiculos ServicioSQLVehiculos;
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
            if (new SQLiteRepositoryEstacionamientos().FindByMatricula(this.VehiculoActual.Matricula) == null)
            {
                new SQLiteRepositoryVehiculos().DeleteVehiculo(this.VehiculoActual);
            }
        }

        private void EditarVehiculo()
        {
            if (new SQLiteRepositoryEstacionamientos().FindByMatricula(this.VehiculoActual.Matricula) == null)
            {
                new SQLiteRepositoryVehiculos().UpdateVehiculo(this.VehiculoActual);
            }
        }

        private void AñadirVehiculo()
        {
            if (new SQLiteRepositoryEstacionamientos().FindByMatricula(this.VehiculoActual.Matricula) == null)
            {
                new SQLiteRepositoryVehiculos().InsertaVehiculo(this.VehiculoActual);
            }
        }
    }
}