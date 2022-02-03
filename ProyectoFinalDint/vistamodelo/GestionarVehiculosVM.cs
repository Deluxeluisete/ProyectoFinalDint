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
        private Clientes cliente;
        public Clientes Cliente
        {
            get => cliente;
            set => SetProperty(ref cliente, value);
        }
        private Vehiculos vehiculo;
        public Vehiculos Vehiculo
        {
            get => vehiculo;
            set => SetProperty(ref vehiculo, value);
        }
        private Estacionamientos plaza;
        public Estacionamientos Estacionamiento
        {
            get => plaza;
            set => SetProperty(ref plaza, value);
        }
        private ObservableCollection<Vehiculos> listaVehiculos;
        public ObservableCollection<Vehiculos> ListaVehiculos
        {
            get => listaVehiculos;
            set => SetProperty(ref listaVehiculos, value);
        }

        public RelayCommand EliminaVehiculoCommand { get; }
        
        public GestionarVehiculosVM()
        {
            this.ListaVehiculos = new SQLiteRepositoryVehiculos().FindAll();
            this.EliminaVehiculoCommand = new RelayCommand(EliminaVehiculoUC);
        }

        private void EliminaVehiculoUC()
        {
            if (new SQLiteRepositoryEstacionamientos().FindByMatricula(this.Vehiculo.Matricula) == null)
            {
                new SQLiteRepositoryVehiculos().DeleteVehiculo(this.Vehiculo);
            }
        }

        public void PlazaCliente_VehiculoActual(Vehiculos v)
        {
            this.Vehiculo = v;
            this.Cliente = new SQLiteRepositoryClientes().FindById(this.Vehiculo.Id_cliente);
            this.Estacionamiento = new SQLiteRepositoryEstacionamientos().FindByMatricula(this.Vehiculo.Matricula);
        }
    }
}