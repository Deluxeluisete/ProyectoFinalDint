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
        private Estacionamientos estacionamientoSeleccionado;

        public Estacionamientos EstacionamientoSeleccionado
        {
            get { return estacionamientoSeleccionado; }
            set { SetProperty(ref estacionamientoSeleccionado, value); }
        }

        public VentanaCobrarVM()
        {
            EstacionamientoSeleccionado = WeakReferenceMessenger.Default.Send<EstacionamientoSeleccionadoMessage>();
        }
    }
}
