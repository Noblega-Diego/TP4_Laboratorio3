using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4_WRITE
{
    class Read: Conexion
    {

        static void Main(string[] args)
        {
            StreamReader sReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(),"temp.txt"));
            String linea;
            MySqlConnection connection = Conectar();
            connection.Open();
            List<Producto> productos;
            try
            {
                sReader.ReadLine();
                linea = sReader.ReadLine();
             
                do {
                    int countLines = 0;
                    productos = new List<Producto>();
                    countLines = 0;
                    while (linea != null) {
                        countLines++;
                        Producto producto = new Producto();
                        string[] columnas = linea.Split("\t");
                        producto.Id = Convert.ToInt64(columnas[0]);
                        producto.FechaDeAlta = Convert.ToDateTime(columnas[1]);
                        producto.Codigo = columnas[2];
                        producto.Denominacion = columnas[3];
                        producto.Precio = Convert.ToDouble(columnas[4]);
                        producto.Publicado = Convert.ToChar(columnas[5]);
                        productos.Add(producto);
                        linea = sReader.ReadLine();
                    
                        if(countLines == 50)
                        {
                            break;
                        }

                    }
                    guardarProductos(productos, connection);

                } while(linea != null);

            }
            catch(Exception e)
            {
                Console.WriteLine("Excepcion producida: " + e.Message);
            }
            finally
            {
                sReader.Close();
                connection.Close();
            }
        }

        private static void guardarProductos(List<Producto> productos, MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            try
            {
                foreach (Producto producto in productos)
                {
                    if (!productoExiste(producto, command)) { 
                        command.CommandText = "INSERT INTO articulo_copy(id, fechaAlta, codigo, denominacion, precio, publicado) " +
                            "values( "+ producto.Id + ", '"
                            + producto.FechaDeAlta.ToString("yyyy/M/dd H/m/s", CultureInfo.InvariantCulture) + "', '"
                            + producto.Codigo + "', '"
                            + producto.Denominacion.Replace("'", @"\'") + "', "
                            + producto.Precio.ToString("0.####", CultureInfo.InvariantCulture) + ", '"
                            + producto.Publicado + "')";
                   
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText = "UPDATE articulo_copy SET " +
                            "fechaAlta = '"+ producto.FechaDeAlta.ToString("yyyy/M/dd H/m/s") + "' , " +
                            "codigo = '"+ producto.Codigo + "' , " +
                            "denominacion = '"+ producto.Denominacion.Replace("'", @"\'") + "' , " +
                            "precio = "+ producto.Precio.ToString("0.####", CultureInfo.InvariantCulture) + " , " +
                            "publicado = '"+ producto.Publicado +"' " +
                            "WHERE id = " + producto.Id;
                        command.ExecuteNonQuery();
                    }
                }
            }catch(Exception e)
            {
                throw;
            }
            
        }

        private static bool productoExiste(Producto producto, MySqlCommand command)
        {
            int exist = 0;
            command.CommandText = "SELECT COUNT(id) AS 'exist' FROM articulo_copy WHERE codigo = '" + producto.Codigo +"'";
            MySqlDataReader reader =  command.ExecuteReader();
            if(reader.Read())
                exist = reader.GetInt32("exist");
            reader.Close();
            return (exist == 1);
        }
    }
}
