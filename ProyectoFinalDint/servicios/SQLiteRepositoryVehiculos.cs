using Microsoft.Data.Sqlite;
using ProyectoFinalDint.modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint.servicios
{
    class SQLiteRepositoryVehiculos
    {
        
        private String src = "";
        private String nombreDB = "parking.db";
        private SqliteConnection conexion;
        private SqliteCommand comando;
        public SQLiteRepositoryVehiculos()
        {
            this.conexion = new SqliteConnection("Data Source=" + this.nombreDB);
        }
        /// <summary>
        /// Constructor que recibe el nombre de la DB y un string source
        /// </summary>
        /// <param name="nombredb"></param> Almaceno el nombre de la BD
        /// <param name="source"></param> 
        public SQLiteRepositoryVehiculos(String nombredb, String source)
        {
            this.nombreDB = nombredb;
            this.src = source;
            this.conexion = new SqliteConnection("Data Source=" + this.src + this.nombreDB);
        }
        /// <summary>
        /// Insertamos un nuevo vehiculo a nuestra base de datos
        /// </summary>
        /// <param name="Vehiculo"></param> recibimos un vehiculo del tipo vehiculo 
        public void InsertaVehiculo(Vehiculos Vehiculo)
        {
            this.conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT COUNT(*) FROM vehiculos";
            int resultado = int.Parse(comando.ExecuteScalar().ToString());

            comando.CommandText = "INSERT INTO vehiculos VALUES (@id,@idCl,@mat,@idM,@mod,@tipo)";
            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters.Add("@idCl", SqliteType.Integer);
            comando.Parameters.Add("@mat", SqliteType.Text);
            comando.Parameters.Add("@idM", SqliteType.Integer);
            comando.Parameters.Add("@mod", SqliteType.Text);
            comando.Parameters.Add("@tipo", SqliteType.Text);

            comando.Parameters["@id"].Value = resultado+1;
            comando.Parameters["@idCl"].Value = Vehiculo.Id_cliente;
            comando.Parameters["@mat"].Value = Vehiculo.Matricula;
            comando.Parameters["@idM"].Value = Vehiculo.Id_marca;
            comando.Parameters["@mod"].Value = Vehiculo.Modelo;
            comando.Parameters["@tipo"].Value = Vehiculo.Tipo;

            comando.ExecuteNonQuery();
            this.conexion.Close();
        }

        private Vehiculos VehiculosFactory(SqliteDataReader lector) => new Vehiculos(
            lector.GetInt32(0),
            lector.GetInt32(1),
            lector[2].ToString(),
            lector.IsDBNull(3) ? -1 : lector.GetInt32(3),
            lector[4].ToString(),
            lector[5].ToString()
        );
        /// <summary>
        /// Encontramos vehiculos gracias al campo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>devolvemos el vehiculo que buscamos por su id</returns>
        public Vehiculos FindById(int id)
        {
            this.conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM vehiculos WHERE id_vehiculo = " + id;

            SqliteDataReader lector = comando.ExecuteReader();
            Vehiculos v = VehiculosFactory(lector);
            if (lector.HasRows)
            {
                if (lector.Read())
                {
                    
                    this.conexion.Close();
                    lector.Close();
                    //return v;
                }
            }
            this.conexion.Close();
            lector.Close();
            return v;
            
        }
        /// <summary>
        /// Encontramos un vehiculo gracias a su matricula
        /// </summary>
        /// <param name="mat"></param> matricula del vehiculo
        /// <returns>devolvemos el vehiculo que buscamos por su matricula</returns>
        public Vehiculos FindByMatricula(String mat)
        {
            this.conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM vehiculos WHERE matricula = " + mat;

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                if (lector.Read())
                {
                    return VehiculosFactory(lector);
                }
            }
            this.conexion.Close();
            lector.Close();
            return null;
        }
        /// <summary>
        /// Cargamos nuestra coleccion de vehiculos 
        /// </summary>
        /// <returns>devuelve los vehiculos que tenemos en nuestra bd</returns>
        public ObservableCollection<Vehiculos> FindAll()
        {
            this.conexion.Open();

            ObservableCollection<Vehiculos> vehiculos = new ObservableCollection<Vehiculos>();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM vehiculos";

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    vehiculos.Add(VehiculosFactory(lector));
                }
                return vehiculos;
            }
            this.conexion.Close();
            lector.Close();
            return null;
        }
        /// <summary>
        /// Actualizamos el modelo del vehiculo gracias a la matricula
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="matricula"></param>
        /// <param name="id"></param>
        /// <returns>devolvemos true o false dependiendo de si encontramos la id</returns>
        public bool UpdateVehiculoModeloMatricula(String modelo, String matricula, int id)
        {
            if (FindById(id) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "UPDATE vehiculos " +
                    "SET modelo = @mod, matricula = @mat " +
                    "Where id_vehiculo = @id";

                comando.Parameters.Add("@mod", SqliteType.Text);
                comando.Parameters.Add("@mat", SqliteType.Text);
                comando.Parameters.Add("@id", SqliteType.Integer);

                comando.Parameters["@mod"].Value = modelo;
                comando.Parameters["@mat"].Value = matricula;
                comando.Parameters["@id"].Value = id;

                comando.ExecuteReader();
                this.conexion.Close();
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Actualizamos un objeto vehiculo
        /// </summary>
        /// <param name="vehiculo"></param>
        /// <returnsdevolvemos true o false ></returns>
        public bool UpdateVehiculo(Vehiculos vehiculo)
        {
            if (FindById(vehiculo.Id_vehiculo.Value) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "UPDATE vehiculos " +
                    "SET id_cliente = @idCl, matricula = @mat, id_marca = @idM, " +
                    "modelo = @mod, tipo = @tip " +
                    "Where id_vehiculo = @id";

                comando.Parameters.Add("@idCl", SqliteType.Integer);
                comando.Parameters.Add("@mat", SqliteType.Text);
                comando.Parameters.Add("@idM", SqliteType.Integer);
                comando.Parameters.Add("@mod", SqliteType.Text);
                comando.Parameters.Add("@tip", SqliteType.Text);
                comando.Parameters.Add("@id", SqliteType.Integer);


                comando.Parameters["@idCl"].Value = vehiculo.Id_cliente;
                comando.Parameters["@mat"].Value = vehiculo.Matricula;
                comando.Parameters["@idM"].Value = vehiculo.Id_marca;
                comando.Parameters["@mod"].Value = vehiculo.Modelo;
                comando.Parameters["@tip"].Value = vehiculo.Tipo;
                comando.Parameters["@id"].Value = vehiculo.Id_vehiculo;

                comando.ExecuteReader();
                this.conexion.Close();
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Borramos un vehiculo por su campo id
        /// </summary>
        /// <param name="id"></param> id del vehiculo
        /// <returns>devuelve un boolean</returns>
        public bool DeleteVehiculoById(int id)
        {
            if (FindById(id) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "DELETE FROM vehiculos WHERE id_vehiculo = @id";

                comando.Parameters.Add("@id", SqliteType.Integer);

                comando.Parameters["@id"].Value = id;
                    
                comando.ExecuteNonQuery();
                this.conexion.Close();
                return true;
            }
            else
            {
                this.conexion.Close();
                return false;
            }
        }
        /// <summary>
        /// Borramos un vehiculo identificado por su objeto vehiculo
        /// </summary>
        /// <param name="vehiculo"></param>
        /// <returns>devolvemos un boolean</returns>
        public bool DeleteVehiculo(Vehiculos vehiculo)
        {
            if (FindById(vehiculo.Id_vehiculo.Value) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "DELETE FROM vehiculos WHERE id_vehiculo = @id";

                comando.Parameters.Add("@id", SqliteType.Integer);

                comando.Parameters["@id"].Value = vehiculo.Id_vehiculo;

                comando.ExecuteNonQuery();
                this.conexion.Close();
                return true;
            }
            else
            {
                this.conexion.Close();
                return false;
            }
        }
    }
}
