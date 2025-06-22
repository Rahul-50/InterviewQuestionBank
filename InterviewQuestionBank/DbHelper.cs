using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class DbHelper
{
    private readonly string _connectionString;

    public DbHelper(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public DataTable GetData(string query)
    {
        using var con = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(query, con);
        using var da = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public void Execute(string query, SqlParameter[] parameters)
    {
        using var con = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(query, con);
        cmd.Parameters.AddRange(parameters);
        con.Open();
        cmd.ExecuteNonQuery();
    }
}
