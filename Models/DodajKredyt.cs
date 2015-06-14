using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankApp_V2_MVC.Models
{
    public class DodajKredyt
    {
        [Required(ErrorMessage = "Pole Kwota  jest wymagane")]
        [DataType(DataType.Currency)]
        public decimal Kwota { get; set; }

         [Display(Name = " Rachunk")]
        public string Rachunek { get; set; }
        public string Okres { get; set; }
        public string Oprocentowanie { get; set; }

        public string iloscrat { get; set; }
        public string NumerKonta { get; set; }
    }
}