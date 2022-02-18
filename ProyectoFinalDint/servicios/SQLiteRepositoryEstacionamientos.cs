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
    class SQLiteRepositoryEstacionamientos
    {
        private String src = "";
        private String nombreDB = "parking.db";
        private SqliteConnection conexion;
        private SqliteCommand comando;
        public SQLiteRepositoryEstacionamientos()
        {
            this.conexion = new SqliteConnection("Data Source=" + this.nombreDB);
        }
        public SQLiteRepositoryEstacionamientos(String nombredb, String source)
        {
            this.nombreDB = nombredb;
            this.src = source;
            this.conexion = new SqliteConnection("Data Source=" + this.src + this.nombreDB);
        }
        /*
         Con este metodo se pueden introducir nuevas plazas en la tabla estacionamientos. Pero solo si el objeto está "lleno", 
        lo  cual no es muy práctico. No voy a depurar este método, ya que solo lo usaremos para pruebas. En la versión final,
        deberiamos incluir un número fijo de plazas o hacer que se genere un número concreto de ellas.
         */
        /// <summary>
        /// Añade un estacionamiento a la base de datos
        /// </summary>
        /// <param name="estacionamientos">Un objeto estacionamiento del cual se extraen los datos para insertalos en la base de datos</param>
        public void Inserta(Estacionamientos estacionamientos)
        {
            this.conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT COUNT(*) FROM estacionamientos";
            int resultado = int.Parse(comando.ExecuteScalar().ToString());

            //fh = fecha y hora
            comando.CommandText = "INSERT INTO estacionamientos VALUES (@id,@idV,@mat,@fhentrada,@fhsalida,@imp,@tipo)";
            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters.Add("@idV", SqliteType.Integer);
            comando.Parameters.Add("@mat", SqliteType.Text);
            comando.Parameters.Add("@fhentrada", SqliteType.Text);
            comando.Parameters.Add("@fhsalida", SqliteType.Text);
            comando.Parameters.Add("@imp", SqliteType.Real);
            comando.Parameters.Add("@tipo", SqliteType.Text);

            comando.Parameters["@id"].Value = resultado;
            comando.Parameters["@idV"].Value = estacionamientos.Id_vehiculo != null ? estacionamientos.Id_vehiculo : null;
            comando.Parameters["@mat"].Value = estacionamientos.Matricula;
            comando.Parameters["@fhentrada"].Value = estacionamientos.Entrada;
            comando.Parameters["@fhsalida"].Value = estacionamientos.Salida;
            comando.Parameters["@imp"].Value = estacionamientos.Importe;
            comando.Parameters["@tipo"].Value = estacionamientos.Tipo;

            comando.ExecuteNonQuery();
            this.conexion.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lector"></param>
        /// <returns></returns>
        private Estacionamientos EstacionamientosFactory(SqliteDataReader lector) => new Estacionamientos(
            lector.GetInt32(0),
            lector.IsDBNull(1) ? -1 : lector.GetInt32(1),
            lector[2].ToString(),
            lector[3].ToString(),
            lector[4].ToString(),
            lector.IsDBNull(5) ? 0.0 : lector.GetDouble(5),
            lector[6].ToString()
        );

        /// <summary>
        /// Encuentra un estacionamiento por id
        /// </summary>
        /// <param name="id">Identificador del estacionamiento</param>
        /// <returns>Devuelve un objeto estacionamiento con el id del parametro</returns>
        public Estacionamientos FindById(int id)
        {
            this.conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM estacionamientos WHERE id_estacionamiento = " + id;

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                if (lector.Read())
                {
                    Estacionamientos e = EstacionamientosFactory(lector);
                    this.conexion.Close();
                    lector.Close();
                    return e;
                }
            }
            this.conexion.Close();
            lector.Close();
            return null;
        }
        /// <summary>
        /// Devuelve los estacionamientos que estan Ocupados, es decir que estan asociados a un vehiculo
        /// </summary>
        /// <returns>Devuelve una lista de estacionamientos ocupados</returns>
        public ObservableCollection<Estacionamientos> FindEstacionamientosOcupados()
        {
            this.conexion.Open();

            ObservableCollection<Estacionamientos> estacionamientos = new ObservableCollection<Estacionamientos>();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM estacionamientos WHERE matricula IS NOT NULL";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    estacionamientos.Add(EstacionamientosFactory(lector));
                }
            }
            this.conexion.Close();
            lector.Close();
            return estacionamientos;
        }
        /// <summary>
        /// Busca un estacionamiento por la matricula del coche que lo ocupa
        /// </summary>
        /// <param name="mat">La matricula del coche</param>
        /// <returns>Devuelve el estacionamiento ocupado o no</returns>
        public Estacionamientos FindByMatricula(String mat)
        {
            this.conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM estacionamientos WHERE matricula = " + mat;

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                if (lector.Read())
                {
                    return EstacionamientosFactory(lector);
                }
            }
            this.conexion.Close();
            lector.Close();
            return null;
        }
        /// <summary>
        /// Busca todos los estacionamientos
        /// </summary>
        /// <returns>Devuelve una lista de todos los estacionamientos</returns>
        public ObservableCollection<Estacionamientos> FindAll()
        {
            this.conexion.Open();

            ObservableCollection<Estacionamientos> estacionamientos = new ObservableCollection<Estacionamientos>();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM estacionamientos";

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    estacionamientos.Add(EstacionamientosFactory(lector));
                }
                //return estacionamientos;
            }
            this.conexion.Close();
            lector.Close();
            return estacionamientos;
        }
        /// <summary>
        /// Añade la hora de entrada de un vehiculo a un estacionamiento
        /// </summary>
        /// <param name="vehiculo">El vehiculo que entra al estacionamiento</param>
        /// <param name="fhEntrada">Fecha y hora de entrada del vehiculo</param>
        /// <param name="idPlaza">id del estacionamiento</param>
        /// <returns>Devuelve si se ha añadido con exito la entrada (True) o no (False)</returns>
        public bool UpdateEstacionamientoMatriculaHoraEntrada(Vehiculos vehiculo, DateTime fhEntrada, int idPlaza)
        {
            String idV = "";
            if (FindById(idPlaza) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                if (vehiculo.Id_vehiculo != null)
                {
                    idV = ", id_vehiculo = @idV ";
                }
                comando.CommandText = "UPDATE estacionamientos " +
                    "SET matricula = @mat, entrada = @fhEnt " + idV +
                    "Where id_estacionamiento = @id";

                comando.Parameters.Add("@mat", SqliteType.Text);
                comando.Parameters.Add("@fhEnt", SqliteType.Integer);
                comando.Parameters.Add("@id", SqliteType.Integer);

                if (vehiculo.Id_vehiculo != null)
                {
                    comando.Parameters.Add("@idV", SqliteType.Integer);
                    comando.Parameters["@idV"].Value = vehiculo.Id_vehiculo;
                }

                comando.Parameters["@mat"].Value = vehiculo.Matricula;
                comando.Parameters["@fhEnt"].Value = fhEntrada.ToString("dd/MM/yyyy HH:mm:ss");
                comando.Parameters["@id"].Value = idPlaza;

                comando.ExecuteReader();
                this.conexion.Close();
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Añade la hora de salida de un vehiculo a un estacionamiento
        /// </summary>
        /// <param name="idPlaza">id del estacionamiento</param>
        /// <param name="fhSalida">Fecha y hora de entrada del vehiculo</param>
        /// <returns>Devuelve si se ha añadido con exito la salida (True) o no (False)</returns>
        public bool UpdateEstacionamientoHoraSalida(int idPlaza, DateTime fhSalida)
        {
            if (FindById(idPlaza) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();
                comando.CommandText = "UPDATE estacionamientos " +
                    "SET salida = @fhSal " +
                    "Where id_estacionamiento = @id";

                comando.Parameters.Add("@sal", SqliteType.Text);
                comando.Parameters.Add("@id", SqliteType.Integer);

                comando.Parameters["@fhSal"].Value = fhSalida.ToString("dd/MM/yyyy HH:mm:ss");
                comando.Parameters["@id"].Value = idPlaza;

                comando.ExecuteReader();
                this.conexion.Close();
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Vacia un estacionamiento por su plaza
        /// </summary>
        /// <param name="idPlaza">Id de la plaza</param>
        /// <returns>Devuelve si se ha vaciado el estacionamiento con exito (True) o no (False)</returns>
        public bool UpdateEstacionamientoVacio(int idPlaza)
        {
            if (FindById(idPlaza) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();
                comando.CommandText = "UPDATE estacionamientos " +
                    "SET id_vehiculo = null, matricula = '', entrada = '', salida = '', importe = null, tipo = '' " +
                    "Where id_estacionamiento = @id";

                comando.Parameters.Add("@id", SqliteType.Integer);

                comando.Parameters["@id"].Value = idPlaza;

                comando.ExecuteReader();
                this.conexion.Close();
                return true;
            }
            else return false;
        }

        /*
         En principio, este método se debe borrar más tarde.
         */
        public bool DeleteEstacionamientoById(int id)
        {
            if (FindById(id) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "DELETE FROM estacionamientos WHERE id_estacionamiento = @id";

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
    }
}
