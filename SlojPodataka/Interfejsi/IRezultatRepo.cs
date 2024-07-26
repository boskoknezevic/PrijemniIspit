using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IRezultatRepo
    {
        DataSet DajSveRezultate();
        DataSet DajRezultatePoStudentu(string jmbgKorisnika);
        bool ObrisiRezultat(int idRezultata);
        bool IzmeniRezultat(int idRezultata, int bodovi);
    }
}
