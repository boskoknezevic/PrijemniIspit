using AplikacioniSloj;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlojPodataka;
using System.Data;

namespace PrezentacioniSloj.Controllers
{
    public class AdminController : Controller
    {
        private readonly clsKorisnikServis _korisnikServis;
        private readonly clsIspitServis _ispitServis;
        private readonly clsPrijavaServis _prijavaServis;
        private readonly clsRezultatServis _rezultatServis;

        public AdminController(clsKorisnikServis korisnikServis, clsIspitServis ispitServis, clsPrijavaServis prijavaServis, clsRezultatServis rezultatServis)
        {
            _korisnikServis = korisnikServis;
            _ispitServis = ispitServis;
            _prijavaServis = prijavaServis;
            _rezultatServis = rezultatServis;
        }

        public IActionResult AdminPocetna()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminPregledKorisnika(string prezime)
        {
            DataSet rezultat;

            if (!string.IsNullOrEmpty(prezime))
            {
                rezultat = _korisnikServis.PrikaziPoPrezimenu(prezime);
            }
            else
            {
                rezultat = _korisnikServis.Prikazi();
            }

            return View(rezultat);
        }

        public IActionResult AdminPregledIspita()
        {
            DataSet smerovi = _ispitServis.SviSmerovi();

            ViewBag.Smerovi = smerovi.Tables[0].AsEnumerable()
                                .Select(row => new {
                                    IDSmera = row.Field<int>("IDSmera"),
                                    Opis = row.Field<string>("Opis")
                                }).ToList();

            var ispiti = _ispitServis.Prikazi();
            return View(ispiti);
        }

        [HttpPost]
        public IActionResult AdminZakaziIspit(DateTime datum, TimeSpan vreme, int smer)
        {
            try
            {
                _ispitServis.NoviIspit(smer, datum, vreme, 1);

                return RedirectToAction("AdminPocetna");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Došlo je do greške prilikom zakazivanja ispita.");
                return View();
            }
        }

        public IActionResult AdminPregledKorisnikaDetalji()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IzmeniKorisnika(string? email, string? jmbg, string? action)
        {
            if (!string.IsNullOrEmpty(email))
            {
                if (action == "izmeni")
                {
                    clsKorisnik korisnik = _korisnikServis.PronadjiPoEmail(email);
                    return View("AdminPregledKorisnikaDetalji", korisnik);
                }
                if (action == "obrisi")
                {
                    _korisnikServis.Obrisi(jmbg);
                }
            }

            return RedirectToAction("AdminPregledKorisnika");
        }

        [HttpPost]
        public IActionResult IzmeniPodatke(string action, clsKorisnik model, string StariJMBG)
        {
            if (action == "izmeni")
            {
                string stariJMBG = StariJMBG;

                _korisnikServis.Izmeni(stariJMBG, model);

                return RedirectToAction("AdminPocetna"); 
            }

            return View(); 
        }


        public IActionResult AdminRezultatStampa()
        {
            DataSet dataSet = _rezultatServis.Prikazi();
                if (dataSet != null)
                    return View("AdminRezultatStampa", dataSet);
                else return RedirectToAction("AdminPocetna");
        }

        public IActionResult AdminUnosRezultata()
        {
            DataSet dataSet = _rezultatServis.Prikazi();

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                // Filtriranje redova sa bodovima == 0
                DataTable filteredTable = dataSet.Tables[0].Clone(); // Clone structure

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    if ((int)row["Bodovi"] == 0)
                    {
                        filteredTable.ImportRow(row);
                    }
                }

                // Kreiranje novog DataSet-a sa filtriranim podacima
                DataSet filteredDataSet = new DataSet();
                filteredDataSet.Tables.Add(filteredTable);

                return View("AdminUnosRezultata", filteredDataSet);
            }
            else
            {
                return RedirectToAction("AdminPocetna");
            }
        }

        [HttpPost]
        public IActionResult UnesiRezultate(int IDRezultata, int NoviRezultat)
        {
            // Implementacija logike za izmenu rezultata
            bool uspesno = _rezultatServis.Izmeni(IDRezultata, NoviRezultat);

            if (uspesno)
            {
                return RedirectToAction("AdminPocetna"); // Uspesno izmenjeno, preusmeri na AdminPocetna
            }
            else
            {
                return RedirectToAction("AdminUnosRezultata"); // Neuspesno, preusmeri nazad na AdminUnosRezultata
            }
        }
    }
}
