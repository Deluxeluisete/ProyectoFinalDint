using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
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
    class GestionarClientesVM : ObservableObject
    {
      
        public GestionarClientesVM()
        {
            this.ListaClientes = new SQLiteRepositoryClientes().FindAll();
            //Console.WriteLine(Clientes[0].Nombre);
        }
    }
}
