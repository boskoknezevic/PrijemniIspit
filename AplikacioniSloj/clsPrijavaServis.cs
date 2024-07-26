using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlojPodataka.Interfejsi;
using DomenskiSloj;

namespace AplikacioniSloj
{
    public class clsPrijavaServis
    {
        private IPrijavaRepo _repo;
        private clsPoslovnaPravila _poslovnaPravila;

        //konstruktor
        public clsPrijavaServis(IPrijavaRepo repo, clsPoslovnaPravila poslovnaPravila)
        {
            _repo = repo;
            _poslovnaPravila = poslovnaPravila;
        }

        public DataSet Prikazi()
        {
            return _repo.DajSvePrijave();
        }

        public DataSet PrikaziPoIspitu(int idIspita)
        {
            return _repo.DajSvePrijavePoIspitu(idIspita);
        }

        public bool Dodaj(string jmbgKorisnika, int idIspita)
        {   
            if(!_poslovnaPravila.ProveraPrijave(jmbgKorisnika, idIspita))
            { return false; }
            if(!_poslovnaPravila.BiranjeSale(idIspita))
            { return false; }
            if(_repo.NovaPrijava(jmbgKorisnika, idIspita))
            {
                int idPrijave = _repo.DajPrijavuPoIspituIKorisniku(jmbgKorisnika, idIspita);
                return _repo.OdobriPrijavu(idPrijave);
            }
            return false;
        }
    }
}
