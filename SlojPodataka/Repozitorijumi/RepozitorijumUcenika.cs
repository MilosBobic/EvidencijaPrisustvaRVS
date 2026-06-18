using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SlojPodataka.Modeli;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SlojPodataka.Repozitorijumi
{
    public class RepozitorijumUcenika : DBUtils
    {
        private readonly KontekstBaze db;

        public RepozitorijumUcenika(KontekstBaze db)
        {
            this.db = db;
        }

        public List<Ucenik> DajSve() //EF Core
        {
            return db.Ucenici.ToList();
        }

        public void Izmeni(Ucenik u)
        {
            db.Ucenici.Update(u);
            db.SaveChanges();
        }

        public Ucenik DajPoID(int id) //dbutils
        {
            DataTable tabela =
                DajTabelu($"SELECT * FROM Ucenici WHERE Id = {id}");

            if (tabela.Rows.Count == 0)
                return null;

            DataRow red = tabela.Rows[0];

            return new Ucenik
            {
                Id = (int)red["Id"],
                Ime = red["Ime"].ToString(),
                Prezime = red["Prezime"].ToString()
            };
        }


        public void Dodaj(Ucenik u) //stored
        {
            using var con = new SqlConnection(connectionString);

            con.Open();

            using var cmd =
                new SqlCommand("sp_DodajUcenika", con);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Ime", u.Ime);

            cmd.Parameters.AddWithValue("@Prezime", u.Prezime);

            cmd.ExecuteNonQuery();
        }

        public void Obrisi(int id)
        {
            using var con = new SqlConnection(connectionString);

            con.Open();

            using var cmd =
                new SqlCommand("sp_ObrisiUcenika", con);

            cmd.CommandType =
                CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();
        }
    }
}