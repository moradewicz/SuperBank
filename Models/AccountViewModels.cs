using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankApp_V2_MVC.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Kod")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Zapamiętać tą przeglądarke")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Pole Email jest wymagane")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Pole Email jest niepoprawne")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest wymagane")]
        [DataType(DataType.Password, ErrorMessage = "Pole hasło jest niepoprawne")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętać ?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Pole Email jest wymagane")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest wymagane")]
        [StringLength(100, ErrorMessage = " {0} musi mieć przynajmniej  {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = " Hasła muszą mieć conajmniej jedną cyfrę oraz znak specjalny, hasło musi również zawierać chociaż jedną dużą literę")]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Pole hasło jest wymagane")]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Podane hasła nie są takie same")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Pole Imie  jest wymagane")]
        [DataType(DataType.Text)]
        [StringLength(10, ErrorMessage = " {0} musi mieć przynajmniej  {2} znaków.", MinimumLength = 1)]
        public string Imie { get; set; }
        [Required(ErrorMessage = "Pole Nazwisko  jest wymagane")]
        [DataType(DataType.Text)]
        [StringLength(15, ErrorMessage = " {0} musi mieć przynajmniej  {2} znaków.", MinimumLength = 1)]
        public string Nazwisko { get; set; }
        [Required(ErrorMessage = "Pole Pesel   jest wymagane")]
        [DataType(DataType.Text)]
        [StringLength(11, ErrorMessage = " {0} musi mieć równo  11 znaków.", MinimumLength = 11)]
        public string Pesel { get; set; }


        [Required(ErrorMessage = "Pole Ulica   jest wymagane")]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = " {0} musi mieć przynajmniej  {2} znaków.", MinimumLength = 1)]
        public string Ulica { get; set; }



        [Required(ErrorMessage = "Pole Nr domu   jest wymagane")]
        [DataType(DataType.Text)]
        [Display(Name = "Nr domu")]
        [StringLength(5, ErrorMessage = " {0} musi mieć przynajmniej  {2} znaków.", MinimumLength = 1)]
        public string Nr_domu { get; set; }



        [Required(ErrorMessage = "Pole Kod pocztowy   jest wymagane")]
        [DataType(DataType.Text)]
        [Display(Name = "Kod pocztowy")]
        [StringLength(5, ErrorMessage = " {0} musi mieć równo  5 znaków.", MinimumLength = 1)]
        public string Kod_pocztowy { get; set; }

        [Required(ErrorMessage = "Pole Miasto jest wymagane")]
        [DataType(DataType.Text)]
        [StringLength(20, ErrorMessage = " {0} musi mieć przynajmniej  {2} znaków.", MinimumLength = 1)]
        public string Miasto { get; set; }



    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Pole Email jest wymagane")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole  Hasło jest wymagane")]
        [StringLength(100, ErrorMessage = " {0} musi mieć przynajmniej  {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Pole hasło jest wymagane")]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Podane hasła nie są takie same")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Pole Email jest wymagane")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
