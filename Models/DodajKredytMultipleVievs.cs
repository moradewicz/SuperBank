using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankApp_V2_MVC.Models
{
    public class DodajKredytMultipleVievs
    {
        public IEnumerable<BankApp_V2_MVC.Models.Konto> Konta { get; set; }
        public BankApp_V2_MVC.Models.DodajKredyt DodajKredyt { get; set; }

    }
}