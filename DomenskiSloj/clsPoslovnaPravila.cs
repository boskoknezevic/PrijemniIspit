
using SlojPodataka;
using SlojPodataka.Interfejsi;
using System.Data;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;

namespace DomenskiSloj
{
    public class clsPoslovnaPravila
    {
        private IPrijavaRepo _repoPrijava;
        private IIspitRepo _repoIspit;

        //konstruktor
        //dobija se string konekcije pri pozivanju
        public clsPoslovnaPravila(IPrijavaRepo repoPrijava, IIspitRepo repoIspit)
        {
            _repoPrijava = repoPrijava;
            _repoIspit = repoIspit;

        }

        //ukoliko ne postoji prijava vraca true u suprotnom false
        public bool ProveraPrijave(string jmbgKorisnika, int idIspita)
        {
            bool proveraUspesnosti = false;

            DataSet dsPodaci = _repoPrijava.DajSvePrijave();

            if (dsPodaci == null || dsPodaci.Tables.Count == 0 || dsPodaci.Tables[0].Rows.Count == 0)
            {
                proveraUspesnosti = true;
                return proveraUspesnosti;
            }
            var rezultat = from DataRow row in dsPodaci.Tables[0].AsEnumerable()
                           where row.Field<string>("JMBG") == jmbgKorisnika
                           && row.Field<int>("IDIspita") == idIspita
                           select row;

            if (!rezultat.Any())
            {
                proveraUspesnosti = true;
                return proveraUspesnosti;
            }

            return proveraUspesnosti;
        }

        public bool BiranjeSale(int idIspita)
        {
            string putanjaDoXml = "C:\\Users\\Ryzen\\Desktop\\PrijemniIspit\\PrijemniIspit\\XMLOgranicenja\\BrojMestaUSali.xml";

            XDocument xml = XDocument.Load(putanjaDoXml);

            int brojMesta = int.Parse(xml.Element("poslovnaPravila").Element("brojmesta").Value);

            DataSet prijave = _repoPrijava.DajSvePrijavePoIspitu(idIspita);

            int brojPrijava = prijave.Tables.Count;

            if (brojPrijava >= brojMesta)
            {
                return false;
            }
            return true;
        }

        public bool ProveraDanaIspita(DateTime datum)
        {
            bool proveraUspesnosti = false;

            DataSet dsPodaci = _repoIspit.DajSveIspite();

            if (dsPodaci == null || dsPodaci.Tables.Count == 0 || dsPodaci.Tables[0].Rows.Count == 0)
            {
                proveraUspesnosti = true;
                return proveraUspesnosti;
            }

            var rezultat = from DataRow row in dsPodaci.Tables[0].AsEnumerable()
                           where row.Field<DateTime>("Datum") == datum
                           select row;

            if (!rezultat.Any())
            {
                proveraUspesnosti = true;
                return proveraUspesnosti;
            }

            return proveraUspesnosti;
        }

    }
}