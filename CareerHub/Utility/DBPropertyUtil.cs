using System.Configuration;
namespace Utility
{
    public static class DBPropertyUtil
    {
        public static string GetConnectionString(string propertyFileName)
        {
            try
            {
                // Read connection string from app.config or return default
                string connectionString = ConfigurationManager.ConnectionStrings["CareerHubDB"]?.ConnectionString;

                if (string.IsNullOrEmpty(connectionString))
                {
                    // Default connection string
                    connectionString = "Server=VOYAGER\\MSSQLSERVER02;Database=careerhub;Integrated Security=true;";
                }

                return connectionString;
            }
            catch (System.Exception ex)
            {
                throw new Exception.DatabaseConnectionException($"Error reading connection string: {ex.Message}");
            }
        }
    }
}
