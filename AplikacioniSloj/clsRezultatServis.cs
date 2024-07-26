using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;
using DomenskiSloj;
using SlojPodataka;

namespace AplikacioniSloj
{
    public class clsRezultatServis
    {
        private IRezultatRepo _repoRezultat;

        //konstruktor
        public clsRezultatServis(IRezultatRepo repo)
        {
            _repoRezultat = repo;
        }

        public DataSet Prikazi()
        {
            return _repoRezultat.DajSveRezultate();
        }

        public DataSet PrikaziPoJMBG(string jmbgKorisnika)
        {
            return _repoRezultat.DajRezultatePoStudentu(jmbgKorisnika);
        }

        public bool Obrisi(int idRezultata)
        {
            return _repoRezultat.ObrisiRezultat(idRezultata);
        }

        public bool Izmeni(int idRezultata, int bodovi)
        {
            return _repoRezultat.IzmeniRezultat(idRezultata, bodovi);
        }
    }
}
