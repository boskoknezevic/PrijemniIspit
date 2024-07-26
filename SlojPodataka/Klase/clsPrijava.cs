using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka
{
    // Class: Prijava - Prati podatke o prijavi, korisniku i terminu ispita.

    // Responsibility:
    // - Prati podatke o prijavi (ID, status).
    // - Povezuje se sa korisnikom (JMBG korisnika).
    // - Povezuje se sa terminom ispita (ID ispita).

    // Collaboration:
    // - Sa klasom Korisnik (veza sa JMBG korisnika).
    // - Sa klasom Ispit (veza sa ID ispita).



    [Table("PRIJAVA")]
    public class clsPrijava
    {
        //polja
        [Key]
        private int _idPrijave;

        [Required]
        [RegularExpression(@"^[0 - 9] +$")]
        private string _jmbgKorisnika;

        [Required]
        private int _idIspita;

        [Required]
        private string _status;

        //property
        public int IdPrijave { get => _idPrijave; set => _idPrijave = value; }
        public string JmbgKorisnika { get => _jmbgKorisnika; set => _jmbgKorisnika = value; }
        public int IdIspita { get => _idIspita; set => _idIspita = value; }
        public string Status { get => _status; set => _status = value; }
    }
}
