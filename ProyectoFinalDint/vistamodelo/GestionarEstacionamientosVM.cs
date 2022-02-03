using Microsoft.Toolkit.Mvvm.ComponentModel;
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
    class GestionarEstacionamientosVM : ObservableObject
    {
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

        public GestionarEstacionamientosVM()
        {
            this.ListaBinding = new ObservableCollection<Estacionamientos>();
            rellenarListaBinding();
            this.ListaEstacionamientos = new SQLiteRepositoryEstacionamientos().FindAll();
        }


        public void rellenarListaBinding()
        {
            listaBinding.Add(new Estacionamientos(1,1,"aaaa", "entrada", "salida", 3.5, "coche"));
            listaBinding.Add(new Estacionamientos(2, 12, "bbbbb", "entrada", "salida", 3.5, "coche"));
            listaBinding.Add(new Estacionamientos(3, 15, "cccc", "entrada", "salida", 3.5, "moto"));
        }


    }
}
