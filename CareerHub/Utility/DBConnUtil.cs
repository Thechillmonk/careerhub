using Exception;
using System.Data.SqlClient;
namespace Utility;
 public static class DBConnUtility
 {
    public static SqlConnection GetConnection(string connectionString)
    {
        try
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        catch (System.Exception ex)
        {
            throw new DatabaseConnectionException($"Error creating database connection: {ex.Message}", ex);
        }
    }
    public static SqlConnection GetConnection()
    {
        string connectionString = DBPropertyUtil.GetConnectionString("Appsettings.json");
        return GetConnection(connectionString);
    }
}




