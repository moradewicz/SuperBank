using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankApp_V2_MVC.Models
{
    public class HistoriaMultipleView
    {

        public IEnumerable<BankApp_V2_MVC.Models.Przelew>  Przelew1 { get; set; }
        public BankApp_V2_MVC.Models.HistoriaModel Historia { get; set; }

    }
}