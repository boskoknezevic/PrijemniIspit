using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka.Interfejsi
{
    public interface IPrijavaRepo
    {
        DataSet DajSvePrijave();
        DataSet DajSvePrijavePoIspitu(int idIspita);
        bool NovaPrijava(string jmbgKorisnika, int idIspita);
        int DajPrijavuPoIspituIKorisniku(string jmbgKorisnika, int idIspita);
        bool OdobriPrijavu(int idPrijave);
    }
}
