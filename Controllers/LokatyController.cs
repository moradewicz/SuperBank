using BankApp_V2_MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BankApp_V2_MVC.Controllers
{
    public class LokatyController : Controller
    {


        public RedirectToRouteResult Zamknij(int IDlokaty)
        {
            decimal kasazlokaty =0;
            int numerkonta = 0;
            using (var db = new BankAppEntities1())
            {
                var result = db.Lokaty.SingleOrDefault(b => b.LokataID == IDlokaty);
                if (result != null)
                {
                    kasazlokaty = (decimal) result.Saldo;
                    numerkonta = (int)result.KontoID;
                    result.Status = "Zamknięta przez klienta";

                    db.SaveChanges();
                }
            }
            using (var db = new BankAppEntities1())
            {
                var result = db.Konto.SingleOrDefault(b => b.KontoId == numerkonta);
                if (result != null)
                {
                    result.Saldo = result.Saldo + kasazlokaty;
                    result.Status = "Zamknięta przez klienta";

                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Lokaty");
        }
        // GET: Lokaty
        public ActionResult Index()
        {
            //11111111111111111111111111111111111111111111
            List<Lokaty> lokaty;
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
                lokaty = context.Lokaty.SqlQuery("SELECT * FROM dbo.Lokaty WHERE UserId = '" + userIdValue + "' ").ToList();

            }

            return View(lokaty);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DodajLokate() {

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


            DodajLokateMultipleView mymodel = new DodajLokateMultipleView();
            mymodel.Konta = konto;

            List<SelectListItem> items = new List<SelectListItem>();
            


            foreach (var item in konto)
            {

                items.Add(new SelectListItem { Text = item.Nazwa + "  Saldo :" + item.Saldo, Value = item.KontoId + "" });

            }

            ViewBag.DaneKont = items;

            SelectList selectList = new SelectList(items, "Value", "Text");
            ViewBag.DaneKontList = selectList;



            List<SelectListItem> okres = new List<SelectListItem>();

            okres.Add(new SelectListItem { Text = "1 miesiąc" ,Value = 1+""});
            okres.Add(new SelectListItem { Text = "6 miesięcy", Value = 6 + "" });
            okres.Add(new SelectListItem { Text = "12 miesięcy", Value = 12 + "" });


            SelectList selectListOkres = new SelectList(okres, "Value", "Text");
            ViewBag.Okres = selectListOkres;
            return View(mymodel);
        }




        [Authorize]
        [HttpPost]
        public ActionResult DodajLokate(DodajLokateMultipleView model) {
            decimal saldo = 0;
            int okresINT = 0;
            string userIdValue = "FAIL";
            DateTime dataOtwarcia = new DateTime();
            DateTime dataWyga = new DateTime();
            decimal oprocentowanie = 0;

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


            decimal kwota = model.DodajLokate.Kwota;
            int rachunek = Convert.ToInt32(model.DodajLokate.NumerKonta);
            string okres = model.DodajLokate.Okres;

            Debug.WriteLine(kwota +"------"+ rachunek + "-pro"+okres);



            List<Konto> konto;
            using (var context = new BankAppEntities1())
            {
                konto = context.Konto.SqlQuery("SELECT * FROM dbo.Konto WHERE UserId = '" + userIdValue + "'AND KontoId='" + rachunek + "'").ToList();
            }
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in konto)
            {
                saldo = item.Saldo;
            }


            if (saldo - kwota >= 0 & kwota > 0)
            {
                 okresINT = Convert.ToInt32(okres);
                 if (okresINT == 1)
                 { 
                oprocentowanie= 3; 
                    
                }
                  
                      if (okresINT == 6)
                 { 
                oprocentowanie= 5; 
                   
                }
                           if (okresINT == 12)
                 { 
                oprocentowanie= 8; 
                     
                }

                 dataOtwarcia = System.DateTime.Now;
                 dataWyga = System.DateTime.Now;
                 dataWyga = dataWyga.AddMonths(okresINT);
                 decimal odsetki = (oprocentowanie * 0.01M) * kwota;
                 Debug.WriteLine( "---ODSETKI---" + odsetki);
                using (var context2 = new BankAppEntities1())
                {
                    var dane = new Lokaty { UserID=userIdValue,KontoID=rachunek,Saldo=kwota,OkresTrwania=okresINT,OprocentowanieLokaty=oprocentowanie,DataOtwarcia=dataOtwarcia,DataWyga=dataWyga,NaliczoneOdsetki=odsetki ,Status = "Otwarta"};
                    context2.Lokaty.Add(dane);

                    context2.SaveChanges();
                }

                decimal nowe = saldo - kwota;
                using (var db = new BankAppEntities1())
                {
                    var result = db.Konto.SingleOrDefault(b => b.KontoId == rachunek);
                    if (result != null)
                    {
                        result.Saldo = nowe;
                        
                        db.SaveChanges();
                    }
                }

            }
            else {
                TempData["MessageLokata"] = (int)2;
                return RedirectToAction("DodajLokate", "Lokaty");
            }


            return RedirectToAction("Index", "Lokaty");
        }
    }
}