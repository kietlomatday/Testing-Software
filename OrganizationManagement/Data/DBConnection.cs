using MySql.Data.MySqlClient;
using System.Data.SqlClient;

public class DBConnection
{
    public static MySqlConnection GetConnection()
    {
        return new MySqlConnection(
            "Server=localhost;Database=organization_db;Uid=root;Pwd=_@_Kiettran1709;"
        );
    }
}
