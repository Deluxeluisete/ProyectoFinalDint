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
    class SQLiteRepositoryMarca
    {
        private String src = "";
        private String nombreDB = "parking.db";
        private SqliteConnection conexion;
        private SqliteCommand comando;
        public SQLiteRepositoryMarca()
        {
            this.conexion = new SqliteConnection("Data Source=" + this.nombreDB);
        }
        public SQLiteRepositoryMarca(String nombredb, String source)
        {
            this.nombreDB = nombredb;
            this.src = source;
            this.conexion = new SqliteConnection("Data Source=" + this.src + this.nombreDB);
        }
        /// <summary>
        /// 
        ///
        /// </summary>
        /// <param name="marca"></param>
        public void Inserta(Marcas marca)
        {
            this.conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT COUNT(*) FROM marcas";
            int resultado = int.Parse(comando.ExecuteScalar().ToString());

            comando.CommandText = "INSERT INTO marcas VALUES (@id,@marca)";
            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters.Add("@marca", SqliteType.Text);

            comando.Parameters["@id"].Value = resultado + 1;
            comando.Parameters["@marca"].Value = marca.Marca;

            comando.ExecuteNonQuery();
            this.conexion.Close();
        }
        private Marcas MarcasFactory(SqliteDataReader lector) => new Marcas(
            lector.GetInt32(0),
            lector[1].ToString()
        );
        /// <summary>
        /// Encontramos una marca por su id 
        /// </summary>
        /// <param name="id"></param> id de la marca
        /// <returns></returns>
        public Marcas FindById(int id)
        {
            this.conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM marcas WHERE id_marca = " + id;

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                if (lector.Read())
                {
                    Marcas m = MarcasFactory(lector);
                    this.conexion.Close();
                    lector.Close();
                    return m;
                }
            }
            this.conexion.Close();
            lector.Close();
            return null;
        }
        /// <summary>
        /// Encontramos una marca por su marca 
        /// </summary>
        /// <param name="marca"></param> 
        /// <returns></returns>
        public Marcas FindByMarca(String marca)
        {
            this.conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM marcas WHERE marca = " + marca;

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                if (lector.Read()) return MarcasFactory(lector);
            }
            this.conexion.Close();
            lector.Close();
            return null;
        }
        /// <summary>
        /// Enseñamos todas las marcas almacenadas en nuestra coleccion
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Marcas> FindAll()
        {
            this.conexion.Open();

            ObservableCollection<Marcas> marcas = new ObservableCollection<Marcas>();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM marcas";

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    marcas.Add(MarcasFactory(lector));
                }
                return marcas;
            }
            this.conexion.Close();
            lector.Close();
            return null;
        }
        /// <summary>
        /// Actualizamos la marca por su campo id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="marca"></param>
        /// <returns></returns>
        public bool UpdateMarcaById(int id, String marca)
        {
            if (FindById(id) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "UPDATE marcas " +
                    "SET marca = @param1 " +
                    "Where id_marca = @id";

                comando.Parameters.Add("@param1", SqliteType.Text);
                comando.Parameters.Add("@id", SqliteType.Integer);

                comando.Parameters["@param1"].Value = marca;
                comando.Parameters["@id"].Value = id;

                comando.ExecuteReader();
                this.conexion.Close();
                return true;
            }
            else return false;
        }
        public bool UpdateMarca(Marcas marca) => UpdateMarcaById(marca.Id_marca, marca.Marca);
        /// <summary>
        /// Eliminamos una marca por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteMarcaById(int id)
        {
            if (FindById(id) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "DELETE FROM marcas WHERE id_marca = @id";

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
        public bool DeleteMarca(Marcas marca) => DeleteMarcaById(marca.Id_marca);
    }
}
