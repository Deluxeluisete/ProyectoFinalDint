using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProyectoFinalDint.servicios.MessageService;

namespace ProyectoFinalDint.vistamodelo
{
    class NuevaMarcaVM : ObservableObject
    {
        private string marca;

        public string Marca
        {
            get { return marca; }
            set { SetProperty(ref marca, value); }
        }

        public NuevaMarcaVM()
        {
        }
        public void Aceptar()
        {
            
            WeakReferenceMessenger.Default.Send(new NuevaMarcaMessage(Marca));
        }
    }
}
