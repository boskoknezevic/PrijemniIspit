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
    public class clsIspitServis
    {
        private IIspitRepo _repo;
        private clsPoslovnaPravila _poslovnaPravila;

        //konstruktor
        public clsIspitServis(IIspitRepo repo, clsPoslovnaPravila poslovnaPravila)
        {
            _repo = repo;
            _poslovnaPravila = poslovnaPravila;
        }

        public DataSet Prikazi()
        {
            return _repo.DajSveIspite();
        }

        public DataSet PrikaziSaID()
        {
            return _repo.DajSveIspiteSaID();
        }

        public DataSet DajIspite(string jmbgKorisnika)
        {
            return _repo.DajIspitePoStudentu(jmbgKorisnika);
        }
        public bool NoviIspit(int idSmera, DateTime datum, TimeSpan vreme, int sala)
        {
            if (_poslovnaPravila.ProveraDanaIspita(datum))
            { return _repo.NoviIspit(idSmera, datum, vreme, sala); }
            else { return false; }
        }

        public bool Obrisi(int idIspita)
        {
           return _repo.ObrisiIspit(idIspita);
        }

        public DataSet SviSmerovi()
        {
            return _repo.DajSveSmerove();
        }
    }
}
