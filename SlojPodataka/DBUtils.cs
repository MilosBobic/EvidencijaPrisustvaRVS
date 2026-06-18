using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace SlojPodataka
{
    public class DBUtils
    {
        protected readonly string connectionString =
            "Server=localhost\\SQLEXPRESS;Database=SkolaDB;Trusted_Connection=True;TrustServerCertificate=True;";

        protected void IzvrsiUpit(
            string upit,
            Dictionary<string, object>? parametri = null)
        {
            using var con = new SqlConnection(connectionString);

            con.Open();

            using var cmd = new SqlCommand(upit, con);

            if (parametri != null)
            {
                foreach (var p in parametri)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            cmd.ExecuteNonQuery();
        }

        protected SqlDataReader IzvrsiCitanje(
            string upit,
            Dictionary<string, object>? parametri = null)
        {
            var con = new SqlConnection(connectionString);

            con.Open();

            var cmd = new SqlCommand(upit, con);

            if (parametri != null)
            {
                foreach (var p in parametri)
                {
                    cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }

            return cmd.ExecuteReader(
                CommandBehavior.CloseConnection);
        }

        protected DataTable DajTabelu(string upit)
        {
            using var con = new SqlConnection(connectionString);

            using var cmd = new SqlCommand(upit, con);

            using var adapter = new SqlDataAdapter(cmd);

            DataTable tabela = new DataTable();

            adapter.Fill(tabela);

            return tabela;
        }
    }
}