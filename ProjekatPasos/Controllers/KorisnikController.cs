using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System;
using AplikacioniSloj;
using SlojPodataka;
using System.Data;

namespace PrezentacioniSloj.Controllers
{
    public class KorisnikController : Controller
    {

        private readonly clsKorisnikServis _korisnikServis;
        private readonly clsRezultatServis _rezultatServis;
        private readonly clsIspitServis _ispitServis;
        private readonly clsPrijavaServis _prijavaServis;

        public KorisnikController(clsKorisnikServis korisnikServis, clsRezultatServis rezultatServis, clsIspitServis ispitServis, clsPrijavaServis prijavaServis)
        {
            _korisnikServis = korisnikServis;
            _rezultatServis = rezultatServis;
            _ispitServis = ispitServis;
            _prijavaServis = prijavaServis;
        }
        public IActionResult KorisnikPocetna()
        {
            return View();
        }

        public IActionResult KorisnikProfil()
        {
            // Dobijanje podataka iz sesije
            var jmbg = HttpContext.Session.GetString("JMBG");
            var ime = HttpContext.Session.GetString("Ime");
            var prezime = HttpContext.Session.GetString("Prezime");
            var lozinka = HttpContext.Session.GetString("Lozinka");
            var email = HttpContext.Session.GetString("Email");
            var tipKorisnika = HttpContext.Session.GetString("TipKorisnika");

            // Kreiranje modela sa podacima iz sesije
            var model = new RegistracijaModel
            {
                JMBG = jmbg,
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Lozinka = lozinka,
                TipKorisnika = tipKorisnika

            };

            return View(model);
        }

        public IActionResult KorisnikPrijava()
        {
            // Pretpostavimo da dobijamo JMBG korisnika iz sesije
            string jmbgKorisnika = HttpContext.Session.GetString("JMBG");

            // Dobijamo sve ispite
            DataSet sviIspiti = _ispitServis.PrikaziSaID();

            // Dobijamo ispite prijavljene od strane korisnika
            DataSet prijavljeniIspiti = _ispitServis.DajIspite(jmbgKorisnika);

            // Kreiramo novi DataSet koji ćemo proslediti view-u
            DataSet ispiti = new DataSet();

            // Kopiramo i preimenujemo tabele pre nego što ih dodamo u novi DataSet
            if (sviIspiti.Tables.Count > 0)
            {
                DataTable sviIspitiTable = sviIspiti.Tables[0].Copy();
                sviIspitiTable.TableName = "SviIspiti";
                ispiti.Tables.Add(sviIspitiTable);
            }

            if (prijavljeniIspiti.Tables.Count > 0)
            {
                DataTable prijavljeniIspitiTable = prijavljeniIspiti.Tables[0].Copy();
                prijavljeniIspitiTable.TableName = "PrijavljeniIspiti";
                ispiti.Tables.Add(prijavljeniIspitiTable);
            }

            return View(ispiti);
        }

        [HttpPost]
        public IActionResult PrijaviIspit(int idIspita)
        {
            
            string jmbgKorisnika = HttpContext.Session.GetString("JMBG");

            if (!string.IsNullOrEmpty(jmbgKorisnika))
            {
                _prijavaServis.Dodaj(jmbgKorisnika, idIspita);
                return RedirectToAction("KorisnikPocetna");
            }
            return RedirectToAction("KorisnikPrijava");
        }

        [HttpPost]
        public IActionResult IzmeniPodatke(RegistracijaModel model, string action)
        {

            if (action == "izmeni")
            {
                // Dobavi JMBG korisnika iz sesije
                var jmbgIzSesije = HttpContext.Session.GetString("JMBG");

                if (!string.IsNullOrEmpty(jmbgIzSesije))
                {
                    clsKorisnik korisnik = new clsKorisnik();
                    korisnik.Jmbg = model.JMBG;
                    korisnik.Ime = model.Ime;
                    korisnik.Prezime = model.Prezime;
                    korisnik.Email = model.Email;
                    korisnik.Lozinka = model.Lozinka;
                    korisnik.TipKorisnika = model.TipKorisnika;

                    if (_korisnikServis.Izmeni(jmbgIzSesije, korisnik))
                        return RedirectToAction("KorisnikPocetna");
                    return View();
                }
                return View();

            }

            else if (action == "obrisi")
            {   
                var jmbg = HttpContext.Session.GetString("JMBG");

                if (!string.IsNullOrEmpty(jmbg))
                {
                    if (_korisnikServis.Obrisi(jmbg))
                        return RedirectToAction("Pocetna", "Home");
                    return View();
                }
                return View();
            }
            return View();
        }


        public IActionResult KorisnikStampa()
        {
            var jmbg = HttpContext.Session.GetString("JMBG");
            if (!string.IsNullOrEmpty(jmbg))
            {
                DataSet dataSet = _rezultatServis.PrikaziPoJMBG(jmbg);
                if (dataSet != null)
                    return View("KorisnikStampa", dataSet);
                else return RedirectToAction("KorisnikPocetna");
            }
            return RedirectToAction("KorisnikPocetna");
        }
    }
}
