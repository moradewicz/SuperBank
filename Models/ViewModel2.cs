using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankApp_V2_MVC.Models
{
    public class ViewModel2
    {

      
            public IEnumerable<RegisterViewModel> RegisterViewModel { get; set; }
            public IEnumerable<BankAppEntities1> BankAppEntities { get; set; }
        }
    }
