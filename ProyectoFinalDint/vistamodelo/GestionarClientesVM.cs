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
        private ObservableCollection<Clientes> clientes;
        public ObservableCollection<Clientes> Clientes
        {
            get => clientes;
            set => SetProperty(ref clientes, value);
        }
        public GestionarClientesVM()
        {
            this.Clientes = new ClientesSQLiteRepository().FindAll();
            //Console.WriteLine(Clientes[0].Nombre);
        }
    }
}
