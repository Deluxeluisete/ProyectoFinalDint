using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using ProyectoFinalDint.modelo;
using ProyectoFinalDint.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProyectoFinalDint.servicios.MessageService;

namespace ProyectoFinalDint.vistamodelo
{
    class VentanaCobrarVM : ObservableRecipient
    {
        private SQLiteRepositoryVehiculos ServicioRepositorioVehiculos;
        private double importe;

        public double Importe
        {
            get
            {
                Importe = esCliente(EstacionamientoSeleccionado, ServicioRepositorioVehiculos) ? EstacionamientoSeleccionado.Importe * 0.9 : EstacionamientoSeleccionado.Importe;
                return importe;
            }
            set { SetProperty(ref importe, value); }
        }

        private Estacionamientos estacionamientoSeleccionado;

        public Estacionamientos EstacionamientoSeleccionado
        {
            get { return estacionamientoSeleccionado; }
            set { SetProperty(ref estacionamientoSeleccionado, value); }
        }

        public VentanaCobrarVM()
        {
            EstacionamientoSeleccionado = WeakReferenceMessenger.Default.Send<EstacionamientoSeleccionadoMessage>();
            ServicioRepositorioVehiculos = new SQLiteRepositoryVehiculos();
        }

        public static Boolean esCliente(Estacionamientos EstacionamientoSeleccionado, SQLiteRepositoryVehiculos ServicioRepositorioVehiculos)
        {
            Vehiculos vehiculoComprobar = new Vehiculos();
            vehiculoComprobar = ServicioRepositorioVehiculos.FindById((int)EstacionamientoSeleccionado.Id_vehiculo);
            if (vehiculoComprobar.Id_vehiculo == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
