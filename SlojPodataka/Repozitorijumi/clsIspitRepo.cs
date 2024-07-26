using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;

namespace SlojPodataka.Repozitorijumi
{
    public class clsIspitRepo : IIspitRepo
    {
        //polje konekcije
        private string _stringKonekcije;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsIspitRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        // izlistava sve prijemne ispite i daje njihov datum,
        // ime i prezime korisnika, kao i datum i vreme termina
        public DataSet DajSveIspite()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveIspite", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajSveIspiteSaID()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveIspiteSaID", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajIspitePoStudentu(string jmbgKorisnika)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajIspitPoKorisniku", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.NVarChar).Value = jmbgKorisnika;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public bool NoviIspit(int idSmera, DateTime datum, TimeSpan vreme, int sala)
        {
            //promenljiva za proveru uspesnosti unosa 
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("NoviIspit", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDSmera", SqlDbType.Int).Value = idSmera; 
            Komanda.Parameters.Add("@Datum", SqlDbType.Date).Value = datum;
            Komanda.Parameters.Add("@Vreme", SqlDbType.Time).Value = vreme;
            Komanda.Parameters.Add("@Sala", SqlDbType.Int).Value = sala;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        public bool ObrisiIspit(int idIspita)
        {
            //promenljiva za proveru uspesnosti unosa 
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("ObrisiIspit", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDIspita", SqlDbType.Int).Value = idIspita;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        public DataSet DajSveSmerove()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSveSmerove", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

    }
}
