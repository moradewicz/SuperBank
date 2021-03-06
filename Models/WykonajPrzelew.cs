﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankApp_V2_MVC.Models
{
    public class WykonajPrzelew
    {


        [Required(ErrorMessage = "Pole Imie  jest wymagane")]
        [DataType(DataType.Text)]
        public string Nr_Rachunku_Odbiorcy { get; set; }
        

        [Required(ErrorMessage = "Pole Imie  jest wymagane")]
        [DataType(DataType.Text)]
        public string Imie { get; set; }
        [Required(ErrorMessage = "Pole Nazwisko  jest wymagane")]
        [DataType(DataType.Text)]
        public string Nazwisko { get; set; }
        [Required(ErrorMessage = "Pole Pesel   jest wymagane")]
        [DataType(DataType.Text)]
        public string Pesel { get; set; }


        [Required(ErrorMessage = "Pole Ulica   jest wymagane")]
        [DataType(DataType.Text)]
        public string Ulica { get; set; }



        [Required(ErrorMessage = "Pole Nr domu   jest wymagane")]
        [DataType(DataType.Text)]
        [Display(Name = "Nr domu")]
        [StringLength(5, ErrorMessage = " {0} musi mieć przynajmniej  {2} znaków.", MinimumLength = 1)]
        public string Nr_domu { get; set; }



        [Required(ErrorMessage = "Pole Kod pocztowy   jest wymagane")]
        [DataType(DataType.Text)]
        [Display(Name = "Kod pocztowy")]
        [StringLength(5, ErrorMessage = " {0} musi mieć przynajmniej  {2} znaków.", MinimumLength = 1)]
        public string Kod_pocztowy { get; set; }

        [Required(ErrorMessage = "Pole Miasto jest wymagane")]
        [DataType(DataType.Text)]
        public string Miasto { get; set; }

        public void wykonaj()
        {
            throw new System.NotImplementedException();
        }


    }
}