using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IIspitRepo
    {
        DataSet DajSveIspite();
        DataSet DajSveIspiteSaID();
        DataSet DajIspitePoStudentu(string jmbgKorisnika);
        bool NoviIspit(int idSmera, DateTime datum, TimeSpan vreme, int sala);
        bool ObrisiIspit(int idIspita);
        DataSet DajSveSmerove();
    }
}
