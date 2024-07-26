using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlojPodataka
{
    // Class: Ispit - Prati podatke o ispitima i omogućava pregled i odabir dostupnih termina za prijemni ispit.

    // Responsibility:
    // - Prati podatke o ispitima (ID, datum, vreme i sala).
    // - Omogućava pregled i odabir dostupnih termina za prijemni ispit.

    // Collaboration:
    // - Sa klasom Smer (povezivanje sa ID smera).


    [Table("ISPIT")]
    public class clsIspit
    {

        //polja
        [Key]
        private int _idIspita;

        [Required]
        private int _idSmera;

        [Required]
        private DateOnly _datum;

        [Required]
        private TimeOnly _vreme;

        [Required]
        private int _sala;

        //property
        public int IdIspita { get => _idIspita; set => _idIspita = value; }
        public int IdSmera { get => _idSmera; set => _idSmera = value; }
        public DateOnly Datum { get => _datum; set => _datum = value; }
        public TimeOnly Vreme { get => _vreme; set => _vreme = value; }
        public int Sala { get => _sala; set => _sala = value; }
    }
}
