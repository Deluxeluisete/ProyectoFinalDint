using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using ProyectoFinalDint.modelo;
using ProyectoFinalDint.servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProyectoFinalDint.servicios.MessageService;

namespace ProyectoFinalDint.vistamodelo
{
    class NuevoClienteVM : ObservableObject
    {
        private Clientes cliente;

        public Clientes Cliente
        {
            get { return cliente; }
            set { SetProperty(ref cliente, value); }
        }

        public RelayCommand ExaminarFotoCommand { get; }
        private NavigationService navigation;


        public NuevoClienteVM()
        {
            ExaminarFotoCommand = new RelayCommand(ExaminarFoto);
            navigation = new NavigationService();
        }

        private void ExaminarFoto()
        {
            throw new NotImplementedException(); //Implementar que coja la foto de azure y saque el genero y la edad para añadirla al objeto Cliente
        }

        internal void Aceptar()
        {
            WeakReferenceMessenger.Default.Send(new NuevoClienteMessage(Cliente));
        }
    }
}
