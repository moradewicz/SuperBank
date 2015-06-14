using BankApp_V2_MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BankApp_V2_MVC.Controllers
{
    public class HomeController : Controller
    {
     
        public ActionResult Index()
        {

     



            Debug.WriteLine(" spr lokaty HOMElolLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL");
            DateTime wygas = new DateTime();
            string userIdValue = "FAIL";
            string status = "fail";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userIdValue = userIdClaim.Value;
                }
            }


            List<Lokaty> lokaty;
            using (var context = new BankAppEntities1())
            {
                lokaty = context.Lokaty.SqlQuery("SELECT * FROM dbo.Lokaty WHERE UserId = '" + userIdValue + "'").ToList();
            }
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in lokaty)
            {
                wygas = (DateTime)item.DataWyga;
                DateTime now = new DateTime();
                now = System.DateTime.Now;
                status = (string)item.Status;
                if (status != null)
                {
                    if (status.StartsWith("Otwarta") & now != null & DateTime.Compare(now, wygas) > 0)
                    {

                        int rachunek = 0;
                        rachunek = (int)item.KontoID;
                        decimal kasazlokaty = (decimal)item.Saldo + (decimal)item.NaliczoneOdsetki;



                        using (var db = new BankAppEntities1())
                        {
                            var result = db.Konto.SingleOrDefault(b => b.KontoId == rachunek);
                            if (result != null)
                            {
                                result.Saldo = result.Saldo + kasazlokaty;
                               
                                db.SaveChanges();
                            }
                        }
                        using (var db = new BankAppEntities1())
                        {
                            var result = db.Lokaty.SingleOrDefault(b => b.KontoID == rachunek);
                            if (result != null)
                            {

                                result.Status = "Zamknięta";
                                db.SaveChanges();
                            }
                        }
                    }
                }
                //
            }

            if (TempData["Imie"] != null)
            {
                string imie = (string)TempData["Imie"];
                string naz = (string)TempData["Nazwisko"];
                string pesel = (string)TempData["Pesel"];

                string kod = (string)TempData["Kod_pocztowy"];
                string miasto = (string)TempData["Miasto"];
                string nrdomu = (string)TempData["Nr_domu"];
                string ulica = (string)TempData["Ulica"];



                TempData["DodanoNowego"] = "Y";


                using (var context2 = new BankAppEntities1())
                {
                    var dane = new DaneKlientow { Nr_konta= "WTF", UserId = userIdValue, Imie = imie, Nazwisko = naz, Pesel = pesel, Kod_pocztowy = kod, Miasto = miasto, Nr_domu = nrdomu, Ulica = ulica };
                    context2.DaneKlientow.Add(dane);

                    context2.SaveChanges();
                }
            }


            return View();
        }


        [Authorize]
        public int ZapisInfoUsera()
        {
            string imie = "2";
            string naz = "2";
            string pesel = "2";
            string userIdValue;
            string kod = "5";
            string misato = "5";
            string nrdomu = "5";
            string ulica = "5";

            imie = (String)TempData["Imie"];
            naz = (String)TempData["Nazwisko"];
            pesel = (String)TempData["Pesel"];

            if (TempData["Kod_pocztowy"] != null)
            {
                kod = (String)TempData["Kod_pocztowy"];
                misato = (String)TempData["Miasto"];
                nrdomu = (String)TempData["Nr_domu"];
                ulica = (String)TempData["Ulica"];
            }


            userIdValue = "FAIL";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userIdValue = userIdClaim.Value;
                }
            }
            using (var context2 = new BankAppEntities1())
            {
                var dane = new DaneKlientow { UserId = userIdValue, Imie = imie, Nazwisko = naz, Pesel = pesel, Nr_konta = "55", Miasto = misato, Ulica = ulica, Nr_domu = nrdomu, Kod_pocztowy = kod };
                context2.DaneKlientow.Add(dane);

                context2.SaveChanges();
            }

            TempData["DodanoNowego"] = "N";

            return 4;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Może nie najtaniej, ale jako tako";



            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Pytaj o co chcesz...";

            return View();
        }


        public ActionResult Pomoc()
        {
            ViewBag.Message = "Wszystko co chciałbyś wiedzieć w jednym miejscu ";

            return View();
        }

        public ActionResult LokatyHome()
        {
            ViewBag.Message = "Wszystko co chciałbyś wiedzieć w jednym miejscu ";

            return View();
        }
        public ActionResult KredytyHome()
        {

            ViewBag.Message = "Wszystko co chciałbyś wiedzieć w jednym miejscu ";
            return View();
        }
    }
}