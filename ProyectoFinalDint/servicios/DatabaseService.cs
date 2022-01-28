using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint.servicios
{
    class DatabaseService
    {

        public DatabaseService()
        {

        }

        public void ObtenerClientesAll() //Tendra que devolver un array o algo
        {
            SqliteConnection conexion = new SqliteConnection("Data Source=parking.db");
            conexion.Open();

            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = @"SELECT * FROM clientes";

            SqliteDataReader lector = comando.ExecuteReader();
            if(lector.HasRows)
            {
                while(lector.Read())
                {
                    int idCliente = (int)lector["id_cliente"];
                    string nombre = (string)lector["nombre"];
                }
            }
        }

    }
}
