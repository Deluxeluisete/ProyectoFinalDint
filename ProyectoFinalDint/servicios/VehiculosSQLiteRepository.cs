using Microsoft.Data.Sqlite;
using ProyectoFinalDint.modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint.servicios
{
    class VehiculosSQLiteRepository
    {
        private String src = "";
        private String nombreDB = "parking.db";
        private SqliteConnection conexion;
        private SqliteCommand comando;
        public VehiculosSQLiteRepository()
        {
            this.conexion = new SqliteConnection("Data Source=" + this.nombreDB);
        }
        public VehiculosSQLiteRepository(String nombredb, String source)
        {
            this.nombreDB = nombredb;
            this.src = source;
            this.conexion = new SqliteConnection("Data Source=" + this.src + this.nombreDB);
        }
        public void Inserta(Vehiculos Vehiculo)
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

            comando.Parameters["@id"].Value = resultado;
            comando.Parameters["@idCl"].Value = Vehiculo.Id_cliente;
            comando.Parameters["@mat"].Value = Vehiculo.Matricula;
            comando.Parameters["@idM"].Value = Vehiculo.Id_marca;
            comando.Parameters["@mod"].Value = Vehiculo.Modelo;
            comando.Parameters["@tipo"].Value = Vehiculo.Tipo;

            comando.ExecuteNonQuery();
            this.conexion.Close();
        }

        private Vehiculos VehiculosFactory(SqliteDataReader lector)
        {
            int id = lector.GetInt32(0);
            int idCl = lector.GetInt32(1);
            String mat = lector[2].ToString();
            int idM = lector.GetInt32(3);
            String mod = lector[4].ToString();
            String tipo = lector[5].ToString();
            return new Vehiculos(id, idCl, mat, idM, mod, tipo);
        }
        public Vehiculos FindById(int id)
        {
            this.conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM vehiculos WHERE id_vehiculo = " + id;

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                if (lector.Read())
                {
                    Vehiculos v = VehiculosFactory(lector);
                    this.conexion.Close();
                    lector.Close();
                    return v;
                }
            }
            this.conexion.Close();
            lector.Close();
            return null;
        }
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
        public List<Vehiculos> FindAll()
        {
            this.conexion.Open();

            List<Vehiculos> vehiculos = new List<Vehiculos>();

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
