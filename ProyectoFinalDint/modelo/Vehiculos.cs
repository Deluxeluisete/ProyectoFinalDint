using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint.modelo
{
    class Vehiculos : ObservableObject
    {
        private int id_vehiculo;

        public int Id_vehiculo
        {
            get { return id_vehiculo; }
            set { SetProperty(ref id_vehiculo,value); }
        }

        private int id_cliente;

        public int Id_cliente
        {
            get { return id_cliente; }
            set { SetProperty(ref id_cliente, value); }
        }

        private string matricula;

        public string Matricula
        {
            get { return matricula; }
            set { SetProperty(ref matricula, value) }
        }

        private int id_marca;

        public int Id_marca
        {
            get { return id_marca; }
            set { SetProperty(ref id_marca, value) }
        }

        private string modelo;

        public string Modelo
        {
            get { return modelo; }
            set { SetProperty(ref modelo, value) }
        }

        private string tipo;

        public string Tipo
        {
            get { return tipo; }
            set { SetProperty(ref tipo, value) }
        }

        public Vehiculos()
        {

        }

        public Vehiculos(int id_vehiculo, int id_cliente, string matricula, int id_marca, string modelo, string tipo)
        {
            Id_vehiculo = id_vehiculo;
            Id_cliente = id_cliente;
            Matricula = matricula;
            Id_marca = id_marca;
            Modelo = modelo;
            Tipo = tipo;
        }


    }
} 
