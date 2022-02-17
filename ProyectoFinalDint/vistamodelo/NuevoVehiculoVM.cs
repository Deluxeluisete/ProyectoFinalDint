using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using ProyectoFinalDint.modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProyectoFinalDint.mensajes.Mensajes;
using static ProyectoFinalDint.servicios.MessageService;

namespace ProyectoFinalDint.vistamodelo
{
    class NuevoVehiculoVM : ObservableObject
    {

        private Vehiculos vehiculo;

        public Vehiculos Vehiculo
        {
            get { return vehiculo; }
            set { SetProperty(ref vehiculo, value); }
        }

        public NuevoVehiculoVM()
        {
            WeakReferenceMessenger.Default.Send(new VehiculoMessage(Vehiculo));
        }
               

        internal void Aceptar()
        {
            throw new NotImplementedException();
        }
    }
}
