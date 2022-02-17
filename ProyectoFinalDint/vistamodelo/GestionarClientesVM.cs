using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using ProyectoFinalDint.modelo;
using ProyectoFinalDint.servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProyectoFinalDint.servicios.MessageService;

namespace ProyectoFinalDint.vistamodelo
{
    class GestionarClientesVM : ObservableObject
    {

        private Clientes clienteSeleccionado;

        public Clientes ClienteSeleccionado
        {
            get { return clienteSeleccionado; }
            set { SetProperty(ref clienteSeleccionado, value); }
        }

        private SQLiteRepositoryClientes ServicioSQLite;

        private ObservableCollection<Clientes> listaClientes;
        public ObservableCollection<Clientes> ListaClientes
        {
            get => listaClientes;
            set => SetProperty(ref listaClientes, value);
        }

        internal void Aceptar()
        {
            throw new NotImplementedException();
        }

        private ObservableCollection<Clientes> listaBinding;

        public ObservableCollection<Clientes> ListaBinding
        {
            get { return listaBinding; }
            set { SetProperty(ref listaBinding, value); }
        }

        public RelayCommand AñadirClienteCommand { get; }
        public RelayCommand FindAllCommand { get; }
        public RelayCommand EliminarClienteCommand { get; }

        private NavigationService navigation;
        public GestionarClientesVM()
        {
            navigation = new NavigationService();
            ServicioSQLite = new SQLiteRepositoryClientes();
            this.ListaBinding = new ObservableCollection<Clientes>();
            rellenarListaBinding();
            this.ListaClientes = ServicioSQLite.FindAll();
            EliminarClienteCommand = new RelayCommand(EliminarCliente);
            AñadirClienteCommand = new RelayCommand(AñadirCliente);
            FindAllCommand = new RelayCommand(FindAll);
            WeakReferenceMessenger.Default.Register<NuevoClienteMessage>(this, (r, m) => { ClienteSeleccionado = m.Value; });

        }

        private void EliminarCliente()
        {
            ServicioSQLite.DeleteCliente(ClienteSeleccionado);
        }

        private void FindAll() 
        {
            ListaClientes = ServicioSQLite.FindAll();
        }

        private void AñadirCliente()
        {
            bool? dialog = navigation.AbrirDialogoNuevoCliente();
            if(dialog == true)
            {
                ServicioSQLite.Inserta(ClienteSeleccionado);
            }
            
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
