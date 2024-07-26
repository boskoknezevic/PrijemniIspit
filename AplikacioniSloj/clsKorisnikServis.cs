using DomenskiSloj;
using SlojPodataka;
using SlojPodataka.Interfejsi;
using System.Data;
using System.Data.SqlClient;

namespace AplikacioniSloj
{
    public class clsKorisnikServis
    {

        private IKorisnikRepo _repo;

        //konstruktor
        public clsKorisnikServis(IKorisnikRepo repo)
        {
            _repo = repo;
        }

        public DataSet Prikazi()
        {
            return _repo.DajSveKorisnike();
        }

        public DataSet PrikaziPoPrezimenu(string prezime)
        {
            return _repo.DajKorisnikaPoPrezimenu(prezime);
        }

        public bool Dodaj(clsKorisnik objKorisnik)
        {
            return _repo.NoviKorisnik(objKorisnik);
        }

        public bool Obrisi(string jmbg)
        {
            return _repo.ObrisiKorisnika(jmbg);
        }

        public bool Izmeni(string StariJMBG, clsKorisnik objNoviKorisnik) 
        {
            return _repo.IzmeniKorisnika(StariJMBG, objNoviKorisnik);
        }

        public clsKorisnik PronadjiPoEmail(string email)
        {
            return _repo.PronadjiPoEmail(email);
        }

    }
}