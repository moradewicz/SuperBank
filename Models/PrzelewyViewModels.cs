using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankApp_V2_MVC.Models
{
    public class Zamkmnij {

        public string Rachunek { get; set; }

        public int NumerKonta { get; set; }
    }
    public class DodajRachunekViewModel
    {

        [Required(ErrorMessage = "Pole Nazwa jest wymagane")]
        [Display(Name = "Nazwa Rachunku")]
       
        public string NazwaKonta { get; set; }
    }

    public class WykonajPrzelewViewModel
    {

        [Required(ErrorMessage = "Pole Numer Rachunku Odbiorcy jest wymagane")]
        [Display(Name = "Numer Rachunku Odbiorcy")]
        [DataType(DataType.CreditCard, ErrorMessage = "Zły numer Rachunku odbiorcy")]
        public string NumerRachunkuOdbiorcy { get; set; }



        [Required(ErrorMessage = "Pole Imie Odbiorcy jest wymagane")]
        [Display(Name = "Imie Odbiorcy")]
        public string ImieOdbiorcy { get; set; }


        [Required(ErrorMessage = "Pole Nazwisko Odbiorcy jest wymagane")]
        [Display(Name = "Nazwisko Odbiorcy")]
        public string NazwiskoOdbiorcy { get; set; }

        [Required(ErrorMessage = "Pole Ulica Odbiorcy jest wymagane")]
        [Display(Name = "Ulica")]
        public string Ulica { get; set; }

        [Required(ErrorMessage = "Pole nr domu Odbiorcy jest wymagane")]
        [Display(Name = "Nr domu")]
        public string nrdomu { get; set; }

        [Required(ErrorMessage = "Pole kod pocztowy Odbiorcy jest wymagane")]
        [Display(Name = "Kod pocztowy")]
        public string Kodpocztowy { get; set; }

        [Required(ErrorMessage = "Pole Miasto Odbiorcy jest wymagane")]
        [Display(Name = "Miasto")]
        public string Miasto { get; set; }

        [Required(ErrorMessage = "Pole Tytuł jest wymagane")]
        [Display(Name = "Tytuł przelewu")]
        public string Tytuł { get; set; }

        [Required(ErrorMessage = "Pole Kwota jest wymagane")]
        [Display(Name = "Kwota przelewu")]
        public decimal Kwota { get; set; }




        [Required(ErrorMessage = "Pole Kwota jest wymagane")]
        [Display(Name = "Kwota")]

        public string DaneKont { get; set; }
        public bool Rachunek { get; set; }

    }

}