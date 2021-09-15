using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4_WRITE
{
    class Conexion
    {
        public static MySqlConnection Conectar()
        {
            MySqlConnection connectMySql;
            MySqlConnectionStringBuilder connectConfig = new MySqlConnectionStringBuilder();
            connectConfig.Server = "localhost";
            connectConfig.Port = 3306;
            connectConfig.UserID = "root";
            connectConfig.Password = "mysql";
            connectConfig.Database = "utn";
            connectConfig.SslMode = MySqlSslMode.None;
            connectMySql = new MySqlConnection(connectConfig.ConnectionString);
            return connectMySql;
        }
    }
}
