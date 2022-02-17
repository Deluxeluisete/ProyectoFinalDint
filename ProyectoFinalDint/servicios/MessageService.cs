using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using ProyectoFinalDint.modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint.servicios
{
    class MessageService
    {
        public class VehiculoMessage : ValueChangedMessage<Vehiculos>
        {
            public VehiculoMessage(Vehiculos vehiculo) : base(vehiculo) { }
        }
        public class NuevaMarcaMessage : ValueChangedMessage<string>
        {
            public NuevaMarcaMessage(string nuevaMarca) : base(nuevaMarca) { }
        }

        public class NuevoClienteMessage : ValueChangedMessage<Clientes>
        {
            public NuevoClienteMessage(Clientes nuevoCliente) : base(nuevoCliente) { }
        }

        public class EstacionamientoSeleccionadoMessage : RequestMessage<Estacionamientos> { }
    }
}
