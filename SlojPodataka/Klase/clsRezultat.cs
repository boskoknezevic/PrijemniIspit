using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka
{
    // Class: Rezultat - Prati podatke o rezultatu ispita za korisnika.

    // Responsibility:
    // - Prati podatke o rezultatu (ID, bodovi).
    // - Povezuje se sa korisnikom (JMBG korisnika).
    // - Povezuje se sa ispitom (ID ispita).

    // Collaboration:
    // - Sa klasom Korisnik (veza sa JMBG korisnika).
    // - Sa klasom Ispit (veza sa ID ispita).



    [Table("REZULTAT")]
    public class clsRezultat
    {

        //polja
        [Key]
        private int _idRezultata;

        [Required]
        [RegularExpression(@"^[0-9]{13}$")]
        private string _jmbgKorisnika;

        [Required]
        private int _idIspita;

        [Required]
        private int _bodovi;

        //property
        public int IdRezultata { get => _idRezultata; set => _idRezultata = value; }
        public string JmbgKorisnika { get => _jmbgKorisnika; set => _jmbgKorisnika = value; }
        public int IdIspita { get => _idIspita; set => _idIspita = value; }
        public int Bodovi { get => _bodovi; set => _bodovi = value; }
    }
}
