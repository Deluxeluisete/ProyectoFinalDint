using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint.modelo
{
    class Clientes : ObservableObject
    {
        public Clientes()
        {

        }

        public Clientes(int id_cliente,  string foto,  string nombre,  string genero,  string documento,  string telefono,  int edad  )
        {
            this.id_cliente = id_cliente;
      
            this.foto = foto;
            
            this.nombre = nombre;
       
            this.genero = genero;
            
            this.documento = documento;
          
            this.telefono = telefono;
  
            this.edad = edad;
       
           
           
        }

        private int id_cliente;

        public int Id_cliente
        {
            get { return id_cliente; }
            set { SetProperty(ref id_cliente, value); }
        }
        private String foto;

        public String Foto
        {
            get { return foto; }
            set { SetProperty(ref foto, value); }
        }
        private String nombre;

        public String Nombre
        {
            get { return nombre; }
            set { SetProperty(ref nombre, value); }
        }
        //hola
      
        private String genero;

        public String Genero
        {
            get { return genero; }
            set { SetProperty(ref genero, value); }
        }
        private String documento;

        public String Documento
        {
            get { return documento; }
            set { SetProperty(ref documento, value); }
        }
        private String telefono;

        public String Telefono
        {
            get { return telefono; }
            set { SetProperty(ref telefono, value); }
        }
        private int edad;

        public int Edad
        {
            get { return edad; }
            set { SetProperty(ref edad, value); }
        }
      

    }
}
