using BankApp_V2_MVC.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.pdf.draw;
using System.Diagnostics;

namespace BankApp_V2_MVC.Controllers
{
    [Authorize]
    public class PrzelewyController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Zamknij(int IDkonto) {
            using (var db = new BankAppEntities1())
            {
                var result = db.Konto.SingleOrDefault(b => b.KontoId == IDkonto);
                if (result != null)
                {

                    result.Status = "Zamknięty przez użytkownika";


                    db.SaveChanges();
                }
            }
            return View();
        }

        // GET: Przelewy
        public ActionResult Index()
        {


            //11111111111111111111111111111111111111111111
            List<Konto> konto;
            string userIdValue = "FAIL";
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



            using (var context = new BankAppEntities1())
            {
                konto = context.Konto.SqlQuery("SELECT * FROM dbo.Konto WHERE UserId = '" + userIdValue + "' ").ToList();

            }

            return View(konto);

        }
        public ActionResult Wykonaj()
        {

            return View();
        }
        [Authorize]
        public ActionResult Rachunki()
        {
            List<Konto> konto;
            string userIdValue = "FAIL";
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



            using (var context = new BankAppEntities1())
            {
                konto = context.Konto.SqlQuery("SELECT * FROM dbo.Konto WHERE UserId = '" + userIdValue + "' ").ToList();

            }





            return View(konto);
        }


        /// <summary>
        /// lel Dodaj R czy cos funakcja wysylajaca do bayz danych
        /// </summary>
        /// <returns></returns>
        ///  
        [Authorize]
        [HttpGet]
        public ActionResult DodajRachunek()
        {


            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult DodajRachunek(DodajRachunekViewModel model)
        {

            string userIdValue = "FAIL";
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


            string nazwa = model.NazwaKonta;

            using (var context2 = new BankAppEntities1())
            {
                var dane = new Konto { UserId = userIdValue, Nazwa = nazwa, Saldo = 100, Status = "OK" };
                context2.Konto.Add(dane);

                context2.SaveChanges();
            }


            return RedirectToAction("Rachunki", "Przelewy");
        }

        [Authorize]
        [HttpPost]
        public ActionResult WykonajPrzelew(WykonajPrzelewMultipleView model, string DaneKont)
        {
            decimal saldo = 0;
            string userIdValue = "FAIL";
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




            string imie = model.Przelew.ImieOdbiorcy;
            string nazwisko = model.Przelew.NazwiskoOdbiorcy;
            int numerOdbiorcy = Convert.ToInt32(model.Przelew.NumerRachunkuOdbiorcy);
            int kontoID = Convert.ToInt32(model.Przelew.DaneKont);

            string tytul = model.Przelew.Tytuł;
            string ulica = model.Przelew.Ulica;
            string numerdomu = model.Przelew.nrdomu;
            string kod = model.Przelew.Kodpocztowy;
            string miasto = model.Przelew.Miasto;
            string tyt = model.Przelew.Tytuł;
            decimal kwota = model.Przelew.Kwota;
            DateTime date = System.DateTime.Now;

            string userIDOdbiorcy = "FAIL";
            decimal SaldoOdbiorcy = 0;
            //1111111111
            List<Konto> konto;
            using (var context = new BankAppEntities1())
            {
                konto = context.Konto.SqlQuery("SELECT * FROM dbo.Konto WHERE UserId = '" + userIdValue + "'AND KontoId='" + kontoID + "'").ToList();
            }
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in konto)
            {
                saldo = item.Saldo;
            }


            //111111111111

            if (saldo - kwota >= 0 & kwota > 0)
            {
                decimal nowe = saldo - kwota;
                //using (var context3 = new BankAppEntities1())
                //{
                //    var konto3 = context3.Database.ExecuteSqlCommand("UPDATE dbo.Konto  SET Saldo =" + nowe + " WHERE UserId = '" + userIdValue + "'AND KontoId=" + kontoID + "");
                //    var konto4 = context3.Database.ExecuteSqlCommand("UPDATE dbo.Konto  SET Saldo =Saldo+" + kwota + " WHERE KontoId=" + numerOdbiorcy + "");


                //    context3.SaveChanges();
                //}
                //update
                using (var db = new BankAppEntities1())
                {
                    var result = db.Konto.SingleOrDefault(b => b.KontoId == kontoID);
                    if (result != null)
                    {
                        result.Saldo = nowe;
                       

                        db.SaveChanges();
                    }
                }
                using (var db = new BankAppEntities1())
                {
                    var result = db.Konto.SingleOrDefault(b => b.KontoId == numerOdbiorcy);
                    if (result != null)
                    {
                        result.Saldo = result.Saldo + kwota;


                        db.SaveChanges();
                    }
                }
                //update


                using (var context3 = new BankAppEntities1())
                {

                    List<Konto> konto5 = context3.Konto.SqlQuery("SELECT * FROM dbo.Konto WHERE  KontoId=" + numerOdbiorcy + "").ToList();
                    foreach (var item in konto5)
                    {
                        userIDOdbiorcy = item.UserId;
                        SaldoOdbiorcy = item.Saldo;
                    }

                    context3.SaveChanges();
                }






                using (var context2 = new BankAppEntities1())
                {
                    var dane = new Przelew { SaldoPoOdbiorcy = SaldoOdbiorcy, KontoIDOdbiorcy = userIDOdbiorcy, SaldoPo = nowe, Imie = imie, Nazwisko = nazwisko, NumerKontaOdbiorcy = numerOdbiorcy, KontoId = userIdValue, NumerKontaNadawcy = kontoID, Ulica = ulica, Nr_domu = numerdomu, Kod_pocztowy = "343", Miasto = miasto, Typ = "Normlany", UserId = userIdValue, Kwota = kwota, Date = date, Tytul = tyt };
                    context2.Przelew.Add(dane);

                    context2.SaveChanges();
                }
                TempData["Message2"] = (int)2;
                return RedirectToAction("Rachunki", "Przelewy");
            }
            else
            {


                TempData["Message"] = (int)2;

                return RedirectToAction("WykonajPrzelew", "Przelewy");
            }
        }


        [Authorize]
        [HttpGet]
        public ActionResult WykonajPrzelew()
        {


            List<Konto> konto;
            string userIdValue = "FAIL";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {

                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userIdValue = userIdClaim.Value;
                }
            }



            using (var context = new BankAppEntities1())
            {
                konto = context.Konto.SqlQuery("SELECT * FROM dbo.Konto WHERE UserId = '" + userIdValue + "' ").ToList();

            }


            WykonajPrzelewMultipleView mymodel = new WykonajPrzelewMultipleView();
            mymodel.Konta = konto;

            List<SelectListItem> items = new List<SelectListItem>();



            foreach (var item in konto)
            {

                items.Add(new SelectListItem { Text = item.Nazwa + "  Saldo :" + item.Saldo, Value = item.KontoId + "" });

            }

            ViewBag.DaneKont = items;

            SelectList selectList = new SelectList(items, "Value", "Text");
            ViewBag.DaneKontList = selectList;




            return View(mymodel);
        }

        public ActionResult Historia()
        {

            List<Przelew> przelew;
            string userIdValue = "FAIL";
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
                    ViewBag.CurrentUserID = userIdValue;
                }
            }



            using (var context = new BankAppEntities1())
            {
                przelew = context.Przelew.SqlQuery("SELECT * FROM dbo.Przelew WHERE UserId = '" + userIdValue + "' OR KontoIDOdbiorcy='" + userIdValue + "'  ").ToList();

            }



            return View(przelew);
        }

        public FileResult PDF(int przelewID)
        {
            string imieW = "err";
            string nazwiskoW = "err";
            List<DaneKlientow> dane;
            string userIdValue = "FAIL";
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
                    ViewBag.CurrentUserID = userIdValue;
                }
            }


            using (var context = new BankAppEntities1())
            {
                dane = context.DaneKlientow.SqlQuery("SELECT * FROM dbo.DaneKlientow WHERE UserId='" + userIdValue + "' ").ToList();

            }
            foreach (var item in dane)
            {
                imieW = item.Imie;
                nazwiskoW = item.Nazwisko;
            }
            
            if (System.IO.File.Exists(Server.MapPath("~/PDF/"+przelewID+".pdf")))
            {
                return File(Server.MapPath("~/PDF/") + przelewID + ".pdf", "application/pdf");
            }

            string imie = "err";
            string nazwisko = "err";
            int numerOdbiorcy = 11;
            int numerNadawcy = 333;
            string ulica = "err";
            string numerdomu = "err";
            string kod = "err";
            string miasto = "err";
            string tyt = "err";
            decimal kwota = 0; ;
            DateTime date = new DateTime();
            string userID1 = "11";
            List<Przelew> przelew;
            string typ = "err";
            string idobiorcy = "22";
            using (var context = new BankAppEntities1())
            {
                przelew = context.Przelew.SqlQuery("SELECT * FROM dbo.Przelew WHERE Przelew_ID=" + przelewID + " ").ToList();

            }
            foreach (var item in przelew)
            {
                imie = item.Imie;
                nazwisko = item.Nazwisko;
                numerOdbiorcy = item.NumerKontaOdbiorcy;
                tyt = item.Tytul;
                ulica = item.Ulica;
                numerdomu = item.Nr_domu;
                miasto = item.Miasto;
                kod = item.Kod_pocztowy;
                kwota = item.Kwota;
                date = item.Date;
                userID1 = item.UserId;
                idobiorcy = item.KontoIDOdbiorcy;
                numerNadawcy = item.NumerKontaNadawcy;
            }
            if (userID1.Equals(idobiorcy))
            {
                typ = "Przelew z konta własnego na konto własne";
            }
            else
            {
                if (userID1.Equals(userIdValue))
                {
                    typ = "Przelew z konta";
                }
                else
                {
                    typ = "Przelew na konto";
                }
            }
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/PDF/"+przelewID+".pdf"), FileMode.Create));
            var font = FontFactory.GetFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, 11, Font.BOLD);
            var fontNor = FontFactory.GetFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, 11, Font.NORMAL);
            var fontWie = FontFactory.GetFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, 12, Font.BOLD);
            var fontA = FontFactory.GetFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, 9, Font.BOLD);
            var fontE = FontFactory.GetFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, 8, Font.NORMAL);
            Paragraph pr1 = new Paragraph("         Super Bank ", fontA);
            Paragraph pr2 = new Paragraph("         ul. Długie Ogrody 8-14", fontA);
            Paragraph pr3 = new Paragraph("         80-755 Gdańsk ", fontA);
            Paragraph pr4 = new Paragraph("         Telefon : 425.555.0100 ", fontA);
            Paragraph drabli = new Paragraph(" Kapitan i Prezes Banku :", fontE);

            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/img/logoicon.png"));
            pic.ScalePercent(40f);
            pic.SetAbsolutePosition(doc.PageSize.Width - 250f - 20f, doc.PageSize.Height - 10f - 95.6f);

            iTextSharp.text.Image pic2Jana = iTextSharp.text.Image.GetInstance(Server.MapPath("~/img/naz.png"));
            pic2Jana.ScalePercent(70f);
            pic2Jana.SetAbsolutePosition(doc.PageSize.Width - 190f - 20f, doc.PageSize.Height - 680f - 95.6f);

            Chunk pusty = new Chunk("\n", font);

            Chunk wlasciciel = new Chunk("                właściciel:                   ", fontNor);
            Chunk wlasciciel1 = new Chunk(imieW + "  " + nazwiskoW, font);
            Chunk wygenerowano = new Chunk("                wygenerowano:           ", fontNor);
            Chunk wygenerowano1 = new Chunk(System.DateTime.Now.ToString(), font);
            Paragraph typtra = new Paragraph(typ, fontWie);
            typtra.Alignment = Element.ALIGN_CENTER;
            int numerdobry = 00;
            int numerdobry2 = 11;
            if (typ.Equals("Przelew na konto"))
            {
                numerdobry = numerOdbiorcy;
                numerdobry2 = numerNadawcy;
            }
            else {
                numerdobry = numerNadawcy;
                numerdobry2 = numerOdbiorcy;
            
            }
            Chunk numer111 = new Chunk("                Numer rachunku:        ", fontWie);
            Chunk kwotaC11W = new Chunk("lel", fontNor);
            Chunk numer111W = new Chunk(numerdobry.ToString(), fontNor);
            Chunk dataoper = new Chunk("                Data operacji:              ", fontWie);
            Chunk dataoperW = new Chunk(date.ToString(), fontNor);
            Chunk opis1111 = new Chunk("                Opis:                             ", fontWie);
            Chunk opis1111W = new Chunk("Nr rach. przeciwst. :", fontNor);  
            Chunk opis2 = new Chunk("                                                           "+numerdobry2, fontNor);
            Chunk opis3 = new Chunk("                                                           " + "Dane adr. rach. przeciwst. :", fontNor);
            Chunk opis4 = new Chunk("                                                           " + imie + "  "+nazwisko, fontNor);
            Chunk opis5 = new Chunk("                                                           " + "ul. "+ulica + " "+numerdomu, fontNor);
            Chunk opis6 = new Chunk("                                                           " + kod + " "+ miasto, fontNor);
            Chunk opis7 = new Chunk("                                                           " + "Tytuł :"+ tyt, fontNor);



            Chunk typtran1 = new Chunk("                Typ transakcji:            ", fontWie);
            Chunk typtran1W = new Chunk(typ, fontNor);
            Chunk kwotaC11 = new Chunk("                Kwota:                          ", fontWie);

            if (typ.Equals("Przelew na konto"))
            {
                kwotaC11W = new Chunk("+" + kwota.ToString() + " PLN", fontNor);
            }
            else if (typ.Equals("Przelew z konta"))
            {
                 kwotaC11W = new Chunk("-"+kwota.ToString()+" PLN", fontNor);
            }
            else
            {
                kwotaC11W = new Chunk(kwota.ToString() + " PLN", fontNor);
            }
            string ending = "           Dokument elektroniczny sporządzony na podstawie art. 7 Ustawy Prawo Bankowe";
            string ending2 = "          (Dz.U. Nr 140 z 1997 roku, poz. 939 z późniejszymi zmianami).                                                                                        Prezes Banku :";
            string ending3 = "          Nie wymaga pieczątki ani podpisu.";

            Paragraph end = new Paragraph(ending, fontE);
            end.SetLeading(1, 0);
            Paragraph end2 = new Paragraph(ending2, fontE);
            end2.SetLeading(1, 1);
            Paragraph end3 = new Paragraph(ending3, fontE);
            end3.SetLeading(1, 0);


            doc.Open();
            PdfContentByte cb = wri.DirectContent;
            doc.Add(pr1);
            doc.Add(pr2);
            doc.Add(pr3);
            doc.Add(pr4);
            doc.Add(pic);
            cb.MoveTo(30f, 680f);
            cb.LineTo(580f, 680f);
            cb.Stroke();
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(wlasciciel);
            doc.Add(wlasciciel1);
            doc.Add(pusty);
            doc.Add(wygenerowano);
            doc.Add(wygenerowano1);
            cb.MoveTo(30f, 600f);
            cb.LineTo(580f, 600f);
            cb.Stroke();
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(typtra);
            doc.Add(pusty);
            cb.MoveTo(30f, 580f);
            cb.LineTo(580f, 580f);
            cb.Stroke();
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(numer111);
            doc.Add(numer111W);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(dataoper);
            doc.Add(dataoperW);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(opis1111);
            doc.Add(opis1111W);
            doc.Add(pusty);
            doc.Add(opis2);
            doc.Add(pusty);
            doc.Add(opis3);
            doc.Add(pusty);
            doc.Add(opis4);
            doc.Add(pusty);
            doc.Add(opis5);
            doc.Add(pusty);
            doc.Add(opis6);
            doc.Add(pusty);
            doc.Add(opis7);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(typtran1);
            doc.Add(typtran1W);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(kwotaC11);
            doc.Add(kwotaC11W);
            cb.MoveTo(30f, 90f);
            cb.LineTo(580f, 90f);
            cb.Stroke();
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(pusty);
            doc.Add(end);
            doc.Add(pusty);
            doc.Add(end2);
            doc.Add(pusty);
            doc.Add(end3);
            doc.Add(pic2Jana);
            doc.Close();



            return File(Server.MapPath("~/PDF/") + przelewID + ".pdf", "application/pdf");
        }
    }
}