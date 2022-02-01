using Microsoft.Toolkit.Mvvm.ComponentModel;
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
        private ObservableCollection<Vehiculos> listaVehiculos;
        public ObservableCollection<Vehiculos> ListaVehiculos
        {
            get => listaVehiculos;
            set => SetProperty(ref listaVehiculos, value);
        }
        public GestionarVehiculosVM()
        {
            this.ListaVehiculos = new VehiculosSQLiteRepository().FindAll();
        }
    }
}