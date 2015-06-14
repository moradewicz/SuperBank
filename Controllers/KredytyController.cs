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
    public class KredytyController : Controller
    {
        // GET: Kredyty
        public ActionResult Index()
        {
            List<Kredyty> kredyty;
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
                kredyty = context.Kredyty.SqlQuery("SELECT * FROM dbo.Kredyty WHERE UserId = '" + userIdValue + "' ").ToList();

            }

            return View(kredyty);
        }
        [Authorize]
        [HttpGet]
        public ActionResult DodajKredyt()
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


            DodajKredytMultipleVievs mymodel = new DodajKredytMultipleVievs();
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

            okres.Add(new SelectListItem { Text = "1 miesiąc", Value = 1 + "" });
            okres.Add(new SelectListItem { Text = "6 miesięcy", Value = 6 + "" });
            okres.Add(new SelectListItem { Text = "12 miesięcy", Value = 12 + "" });


            SelectList selectListOkres = new SelectList(okres, "Value", "Text");
            ViewBag.Okres = selectListOkres;
            return View(mymodel);
        }
        [Authorize]
        [HttpPost]
        public ActionResult DodajKredyt(DodajKredytMultipleVievs model) {
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


            decimal kwota = model.DodajKredyt.Kwota;
            int rachunek = Convert.ToInt32(model.DodajKredyt.NumerKonta);
            string okres = model.DodajKredyt.Okres;

            Debug.WriteLine(kwota + "------" + rachunek + "-pro" + okres);



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


        
                okresINT = Convert.ToInt32(okres);
                if (okresINT == 1)
                {
                    oprocentowanie = 3;

                }

                if (okresINT == 6)
                {
                    oprocentowanie = 5;

                }
                if (okresINT == 12)
                {
                    oprocentowanie = 8;

                }

                dataOtwarcia = System.DateTime.Now;
                dataWyga = System.DateTime.Now;
                dataWyga = dataWyga.AddMonths(okresINT);
                decimal odsetki = (oprocentowanie * 0.01M) * kwota;
                Debug.WriteLine("---ODSETKI---" + odsetki);
                decimal rata = 6.43M;
                using (var context2 = new BankAppEntities1())
                {
                    var dane = new Kredyty { DataZawarcia = dataOtwarcia, Saldo = kwota, Rata = rata, Okres = 7,UserID=userIdValue,zostalorat=8 };
                    context2.Kredyty.Add(dane);

                    context2.SaveChanges();
                }
                



            return RedirectToAction("Index", "Kredyty");
        }
    }
}