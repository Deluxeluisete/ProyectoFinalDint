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
        public GestionarEstacionamientosVM()
        {
            this.ListaEstacionamientos = new SQLiteRepositoryEstacionamientos().FindAll();
        }
    }
}
