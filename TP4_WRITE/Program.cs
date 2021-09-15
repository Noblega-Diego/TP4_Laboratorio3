using MySql.Data.MySqlClient;
using System;
using System.Security.Permissions;
using System.Text;
using System.IO;

namespace TP4_WRITE
{
    class Program: Conexion
    {
        static void Main(string[] args)
        {
            MySqlConnection connectMySql = Conectar();
            MySqlCommand command = new MySqlCommand();
            StreamWriter streamWriter = null;
            command.Connection = connectMySql;
            int maxitems = 50;
            int itemCount = 0;
            int pag = 0;
            int ID = 0, FECHAALTA = 1, CODIGO = 2, DENOMINACION = 3, PRECIO = 4, PUBLICADO = 5;
            try
            {
                connectMySql.Open();
                streamWriter = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(),"temp.txt"));
                do {
                    itemCount = 0;
                    command.CommandText = "SELECT * FROM articulo LIMIT " + pag * maxitems + "," + (pag * maxitems) + maxitems;
                    MySqlDataReader readProducto = command.ExecuteReader();
                    StringBuilder buffer = new StringBuilder();
                     
                    if (pag == 0)
                    {
                        buffer.Append("id\tfechaAlta\tcodigo\tdenominacion\tprecio\tpublicado\n");
                    }
                    while (readProducto.Read())
                    {
                        long id = readProducto.GetInt64(ID);
                        DateTime fechaAlta = readProducto.GetDateTime(FECHAALTA);
                        string codigo = readProducto.GetString(CODIGO);
                        string denominacion = readProducto.GetString(DENOMINACION);
                        double precio = readProducto.GetDouble(PRECIO);
                        char publicado = readProducto.GetChar(PUBLICADO);
                        itemCount ++;
                        buffer.Append("" + id + "\t" + fechaAlta.ToString() + "\t" + codigo + "\t" + denominacion + "\t" + precio + "\t" + publicado + "\n");
                    }

                    readProducto.Close();
                    streamWriter.Write(buffer.ToString());
                    pag ++;
                } while (itemCount == maxitems);
                Console.WriteLine("terminado con exito");
                streamWriter.Close();
            }catch( Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (streamWriter != null)
                    streamWriter.Close();
                connectMySql.Close();
            }

        }

        
    }
}
