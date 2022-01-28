using Microsoft.Data.Sqlite;
using ProyectoFinalDint.modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalDint.servicios
{
    class ClientesSQLiteRepository
    {
        private String src = "";
        private String nombreDB = "parking.db";
        private SqliteConnection conexion;
        private SqliteCommand comando;
        public ClientesSQLiteRepository()
        {
            this.conexion = new SqliteConnection("Data Source=" + this.nombreDB);
        }
        public ClientesSQLiteRepository(String nombredb, String source)
        {
            this.nombreDB = nombredb;
            this.src = source;
            this.conexion = new SqliteConnection("Data Source=" + this.src + this.nombreDB);
        }

        public void Inserta(Clientes cliente)
        {
            this.conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT COUNT(*) FROM clientes";
            int resultado = int.Parse(comando.ExecuteScalar().ToString());

            comando.CommandText = "INSERT INTO clientes VALUES (@id,@nombre,@documento,@foto,@edad,@genero,@telefono)";
            comando.Parameters.Add("@id", SqliteType.Integer);
            comando.Parameters.Add("@nombre", SqliteType.Text);
            comando.Parameters.Add("@documento", SqliteType.Text);
            comando.Parameters.Add("@foto", SqliteType.Text);
            comando.Parameters.Add("@edad", SqliteType.Integer);
            comando.Parameters.Add("@genero", SqliteType.Text);
            comando.Parameters.Add("@telefono", SqliteType.Text);

            comando.Parameters["@id"].Value = resultado;
            comando.Parameters["@nombre"].Value = cliente.Nombre;
            comando.Parameters["@documento"].Value = cliente.Documento;
            comando.Parameters["@foto"].Value = cliente.Foto;
            comando.Parameters["@edad"].Value = cliente.Edad;
            comando.Parameters["@genero"].Value = cliente.Genero;
            comando.Parameters["@telefono"].Value = cliente.Telefono;

            comando.ExecuteNonQuery();
            this.conexion.Close();
        }
        private Clientes ClientesFactory(SqliteDataReader lector)
        {
            int id = int.Parse(lector[0].ToString());
            String nombre = lector[1].ToString();
            String doc = lector[2].ToString();
            String s = lector[3].ToString();
            int edad = int.Parse(lector[4].ToString());
            String genero = lector[5].ToString();
            String tlf = lector[6].ToString();
            return new Clientes(id, s, nombre, genero, doc, tlf, edad);
        }
        public Clientes FindById(int id)
        {
            this.conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM clientes WHERE id_cliente = " + id;

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                if (lector.Read())
                {
                    Clientes c = ClientesFactory(lector);
                    this.conexion.Close();
                    lector.Close();
                    return c;
                }
            }
            this.conexion.Close();
            lector.Close();
            return null;
        }
        public Clientes FindByDocumento(String doc)
        {
            this.conexion.Open();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM clientes WHERE documento = " + doc;

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                if (lector.Read())
                {
                    return ClientesFactory(lector);
                }
            }
            this.conexion.Close();
            lector.Close();
            return null;
        }
        public List<Clientes> FindAll()
        {
            this.conexion.Open();

            List<Clientes> clientes = new List<Clientes>();

            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM clientes";

            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    clientes.Add(ClientesFactory(lector));
                }
                return clientes;
            }
            this.conexion.Close();
            lector.Close();
            return null;
        }
        public bool UpdateClienteEdadGenero(int edad, String genero, int id)
        {
            if (FindById(id) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "UPDATE clientes " +
                    "SET edad = @ed, genero = @gen " +
                    "Where id_cliente = @id";

                comando.Parameters.Add("@gen", SqliteType.Text);
                comando.Parameters.Add("@ed", SqliteType.Integer);
                comando.Parameters.Add("@id", SqliteType.Integer);

                comando.Parameters["@ed"].Value = edad;
                comando.Parameters["@gen"].Value = genero;
                comando.Parameters["@id"].Value = id;

                comando.ExecuteReader();
                this.conexion.Close();
                return true;
            }
            else return false;
        }
        public bool UpdateCliente(Clientes cliente)
        {
            if (FindById(cliente.Id_cliente) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "UPDATE clientes " +
                    "SET nombre = @nom, documento = @doc, foto = @foto, " +
                    "edad = @ed, genero = @gen, telefono = @tel " +
                    "Where id_cliente = @id";

                comando.Parameters.Add("@nom", SqliteType.Text);
                comando.Parameters.Add("@doc", SqliteType.Text);
                comando.Parameters.Add("@foto", SqliteType.Text);
                comando.Parameters.Add("@ed", SqliteType.Integer);
                comando.Parameters.Add("@gen", SqliteType.Text);
                comando.Parameters.Add("@tel", SqliteType.Text);
                comando.Parameters.Add("@id", SqliteType.Integer);


                comando.Parameters["@nom"].Value = cliente.Nombre;
                comando.Parameters["@doc"].Value = cliente.Documento;
                comando.Parameters["@foto"].Value = cliente.Foto;
                comando.Parameters["@ed"].Value = cliente.Edad;
                comando.Parameters["@gen"].Value = cliente.Genero;
                comando.Parameters["@tel"].Value = cliente.Telefono;
                comando.Parameters["@id"].Value = cliente.Id_cliente;

                comando.ExecuteReader();
                this.conexion.Close();
                return true;
            }
            else return false;
        }

        public bool DeleteClienteById(int id)
        {
            if (FindById(id) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "DELETE FROM clientes WHERE id_cliente = @id";

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
        public bool DeleteCliente(Clientes cliente)
        {
            if (FindById(cliente.Id_cliente) != null)
            {
                this.conexion.Open();
                comando = conexion.CreateCommand();

                comando.CommandText = "DELETE FROM clientes WHERE id_cliente = @id";

                comando.Parameters.Add("@id", SqliteType.Integer);

                comando.Parameters["@id"].Value = cliente.Id_cliente;

                comando.ExecuteNonQuery();
                this.conexion.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
