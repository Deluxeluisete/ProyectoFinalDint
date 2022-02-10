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
    class GestionarEstacionamientosVM : ObservableObject
    {
        private SQLiteRepositoryEstacionamientos ServicioSQLite;

        private NavigationService ServicioNavegacion;

        private ObservableCollection<Estacionamientos> listaEstacionamientos;
        public ObservableCollection<Estacionamientos> ListaEstacionamientos
        {
            get => listaEstacionamientos;
            set => SetProperty(ref listaEstacionamientos, value);
        }

        private Estacionamientos estacionamientoSeleccionado;

        public Estacionamientos EstacionamientoSeleccionado
        {
            get { return estacionamientoSeleccionado; }
            set { SetProperty(ref estacionamientoSeleccionado, value); }
        }

        public RelayCommand CobrarEstacionamientoCommand { get; }
        public GestionarEstacionamientosVM()
        {
            ServicioSQLite = new SQLiteRepositoryEstacionamientos();
            ServicioNavegacion = new NavigationService();
            this.ListaEstacionamientos = ServicioSQLite.FindAll();  //Si no sale ninguno es porque no hay ninguno ocupado, es decir, el id_vehiculo del estacionamiento es NULL
            CobrarEstacionamientoCommand = new RelayCommand(Cobrar);
        }

        public void Cobrar()
        {
            WeakReferenceMessenger.Default.Register<GestionarEstacionamientosVM, EstacionamientoSeleccionadoMessage>(this, (r, m) => { m.Reply(r.estacionamientoSeleccionado);});
            ServicioNavegacion.AbrirDialogoCobrar();
        }

        private void EliminarEstacionamiento()
        {
            ServicioSQLite.DeleteEstacionamientoById(estacionamientoSeleccionado.Id_estacionamiento);
            ListaEstacionamientos = ServicioSQLite.FindEstacionamientosOcupados();
        }

    }
}
