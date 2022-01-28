using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint.servicios
{
    class MessageService
    {
        public class NuevaMarcaMessage : ValueChangedMessage<string>
        {
            public NuevaMarcaMessage(string nuevaMarca) : base(nuevaMarca) { }
        }
    }
}
