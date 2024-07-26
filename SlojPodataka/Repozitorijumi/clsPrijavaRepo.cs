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
    public class clsPrijavaRepo : IPrijavaRepo
    {
        //polje konekcije
        private string _stringKonekcije;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsPrijavaRepo(string stringKonekcije)
        {
            _stringKonekcije = stringKonekcije;
        }

        public DataSet DajSvePrijave()
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSvePrijave", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public DataSet DajSvePrijavePoIspitu(int idIspita)
        {
            DataSet dsPodaci = new DataSet();

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("DajSvePrijavePoIspitu", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDIspita", SqlDbType.Int).Value = idIspita;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Komanda;
            da.Fill(dsPodaci);
            Veza.Close();
            Veza.Dispose();

            return dsPodaci;
        }

        public int DajPrijavuPoIspituIKorisniku(string jmbgKorisnika, int idIspita)
        {
            int idPrijave = -1;

            using (SqlConnection veza = new SqlConnection(_stringKonekcije))
            {
                veza.Open();
                using (SqlCommand komanda = new SqlCommand("DajPrijavuPoIspituIKorisniku", veza))
                {
                    komanda.CommandType = CommandType.StoredProcedure;
                    komanda.Parameters.Add("@IDIspita", SqlDbType.Int).Value = idIspita;
                    komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.NVarChar).Value = jmbgKorisnika;

                    using (SqlDataReader reader = komanda.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idPrijave = reader.GetInt32(0);
                        }
                    }
                }
            }

            return idPrijave;
        }


        public bool NovaPrijava(string jmbgKorisnika, int idIspita)
        {
            //promenljiva za proveru uspesnosti unosa 
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("NovaPrijava", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@JMBGKorisnika", SqlDbType.NVarChar).Value = jmbgKorisnika;
            Komanda.Parameters.Add("@IDIspita", SqlDbType.Int).Value = idIspita;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }

        

        public bool OdobriPrijavu(int idPrijave)
        {
            int proveraUnosa = 0;

            SqlConnection Veza = new SqlConnection(_stringKonekcije);
            Veza.Open();
            SqlCommand Komanda = new SqlCommand("OdobriPrijavuIKreirajRezultat", Veza);
            Komanda.CommandType = CommandType.StoredProcedure;
            Komanda.Parameters.Add("@IDPrijave", SqlDbType.Int).Value = idPrijave;

            proveraUnosa = Komanda.ExecuteNonQuery();
            Veza.Close();
            Veza.Dispose();

            //vraca true ako je uspesno
            return (proveraUnosa > 0);
        }
    }
}
