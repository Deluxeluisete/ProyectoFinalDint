using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint.modelo
{
    class Marca: ObservableObject
    {
        private int id_marca;

        public int Id_marca
        {
            get { return id_marca; }
            set { SetProperty(ref id_marca, value); }
        }

        private string marca;

        public string Marca
        {
            get { return marca; }
            set { SetProperty(ref marca, value); }
        }

        public Marca(int id_marca, string marca)
        {
            Id_marca = id_marca;
            Marca = marca;
        }
    }
}
