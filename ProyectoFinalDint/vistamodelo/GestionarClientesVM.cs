using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
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
        private SQLiteRepositoryClientes ServicioSQLite;

        private ObservableCollection<Clientes> listaClientes;
        public ObservableCollection<Clientes> ListaClientes
        {
            get => listaClientes;
            set => SetProperty(ref listaClientes, value);
        }

        private ObservableCollection<Clientes> listaBinding;

        public ObservableCollection<Clientes> ListaBinding
        {
            get { return listaBinding; }
            set { SetProperty(ref listaBinding, value); }
        }

        public RelayCommand AñadirClienteCommand { get; }
        public RelayCommand FindAllCommand { get; }
        public GestionarClientesVM()
        {
            ServicioSQLite = new SQLiteRepositoryClientes();
            this.ListaBinding = new ObservableCollection<Clientes>();
            rellenarListaBinding();
            this.ListaClientes = ServicioSQLite.FindAll();
            AñadirClienteCommand = new RelayCommand(AñadirCliente);
            FindAllCommand = new RelayCommand(FindAll);
        }

        private void FindAll()
        {
            ListaClientes = ServicioSQLite.FindAll();
        }

        private void AñadirCliente()
        {
            ServicioSQLite.Inserta(new Clientes(1, "ssss", "aaaa", "entrada", "salida", "ddddd", 6));
        }

        public void rellenarListaBinding()
        {
            listaBinding.Add(new Clientes(1, "ssss", "aaaa", "entrada", "salida", "ddddd", 6));
            listaBinding.Add(new Clientes(1, "ssss", "aaaa", "entrada", "salida", "ddddd", 6));
            listaBinding.Add(new Clientes(1, "ssss", "aaaa", "entrada", "salida", "ddddd", 6));
            listaBinding.Add(new Clientes(1, "ssss", "aaaa", "entrada", "salida", "ddddd", 6));
          
        }


    }
}
