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
    class GestionarClientesUserControlVM : ObservableObject
    {
        private SQLiteRepositoryEstacionamientos ServicioSQLite;

        private ObservableCollection<Estacionamientos> listaEstacionamientos;
        public ObservableCollection<Estacionamientos> ListaEstacionamientos
        {
            get => listaEstacionamientos;
            set => SetProperty(ref listaEstacionamientos, value);
        }

        private ObservableCollection<Estacionamientos> listaBinding;

        public ObservableCollection<Estacionamientos> ListaBinding
        {
            get { return listaBinding; }
            set { SetProperty(ref listaBinding, value); }
        }

        public RelayCommand AñadirEstacionamientoCommand { get; }
        public RelayCommand FindAllCommand { get; }
        public GestionarClientesUserControlVM()
        {
            ServicioSQLite = new SQLiteRepositoryEstacionamientos();
            this.ListaBinding = new ObservableCollection<Estacionamientos>();
            rellenarListaBinding();
            this.ListaEstacionamientos = ServicioSQLite.FindAll();
            AñadirEstacionamientoCommand = new RelayCommand(AñadirEstacionamiento);
            FindAllCommand = new RelayCommand(FindAll);
        }

        private void FindAll()
        {
            ListaEstacionamientos = ServicioSQLite.FindAll();
        }

        private void AñadirEstacionamiento()
        {
            ServicioSQLite.Inserta(new Estacionamientos(5, 8, "1234 GGE", "03/02/2022 - 12:34", "03/02/2022 - 14:23", 5.9, "coche"));
        }

        public void rellenarListaBinding()
        {
            listaBinding.Add(new Estacionamientos(1, 1, "aaaa", "entrada", "salida", 3.5, "coche"));
            listaBinding.Add(new Estacionamientos(2, 12, "bbbbb", "entrada", "salida", 3.5, "coche"));
            listaBinding.Add(new Estacionamientos(3, 15, "cccc", "entrada", "salida", 3.5, "moto"));
        }


    }
}
