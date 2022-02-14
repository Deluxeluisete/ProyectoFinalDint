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

        private Vehiculos vehiculoSeleccionado;

        public Vehiculos VehiculoSeleccionado
        {
            get { return vehiculoSeleccionado; }
            set { SetProperty(ref vehiculoSeleccionado, value); }
        }

        public RelayCommand AñadirVehiculoCommand { get; }

        public RelayCommand EditarVehiculoCommand { get; }

        public RelayCommand BorrarVehiculoCommand { get; }

        SQLiteRepositoryVehiculos ServicioSQLVehiculos;
        public GestionarVehiculosVM()
        {
            ServicioSQLVehiculos = new SQLiteRepositoryVehiculos();
            this.ListaVehiculos = ServicioSQLVehiculos.FindAll();
            AñadirVehiculoCommand = new RelayCommand(AñadirVehiculo);
            EditarVehiculoCommand = new RelayCommand(EditarVehiculo);
            BorrarVehiculoCommand = new RelayCommand(BorrarVehiculo);
        }

        private void BorrarVehiculo()
        {
            if (new SQLiteRepositoryEstacionamientos().FindByMatricula(this.VehiculoActual.Matricula) == null)
            {
                new SQLiteRepositoryVehiculos().DeleteVehiculo(this.VehiculoActual);
            }
            ListaVehiculos = ServicioSQLVehiculos.FindAll();
        }

        private void EditarVehiculo()
        {
            if (new SQLiteRepositoryEstacionamientos().FindByMatricula(this.VehiculoActual.Matricula) == null)
            {
                new SQLiteRepositoryVehiculos().UpdateVehiculo(this.VehiculoActual);
            }
            ListaVehiculos = ServicioSQLVehiculos.FindAll();
        }

        private void AñadirVehiculo()
        {
            //Traer de una ventana hija el objeto vehiculo con los datos insertados por el usuario.

            if (new SQLiteRepositoryEstacionamientos().FindByMatricula(this.VehiculoActual.Matricula) == null)
            {
                new SQLiteRepositoryVehiculos().InsertaVehiculo(this.VehiculoActual);
            }
            ListaVehiculos = ServicioSQLVehiculos.FindAll();
        }
    }
}