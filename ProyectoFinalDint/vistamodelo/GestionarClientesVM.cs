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

        private ObservableCollection<Clientes> listaclientes;
        public ObservableCollection<Clientes> ListaClientes
        {
            get { return listaclientes; }
            set { listaclientes = value; }
        }

        public GestionarClientesVM()
        {
            this.ListaClientes = new SQLiteRepositoryClientes().FindAll();
        }
    }
}
