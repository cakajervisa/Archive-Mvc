using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arkiva.Models
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
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
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
        [Required(ErrorMessage = "Emaili është një fushë e detyrueshme për tu identifikuar!")]
        [Display(Name = "Email")]
        [EmailAddress (ErrorMessage = "Nuk është një adresë emaili e vlefshme.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fjalëkalimi është një fushë e detyrueshme për tu identifikuar!")]
        [DataType(DataType.Password, ErrorMessage = "Nuk është një fjalëkalim i vlefshëm")]
        [Display(Name = "Fjalëkalimi")]
        public string Password { get; set; }

        [Display(Name = "Më mbaj mend?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Emaili është një fushë e detyrueshme për tu regjistruar!")]
        [EmailAddress(ErrorMessage = "Nuk është një adresë emaili e vlefshme.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Fjalëkalimi është një fushë e detyrueshme për tu regjistruar!")]
        [StringLength(100, ErrorMessage = "{0} duhet të ketë të paktën {2} karaktere.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Fjalëkalimi duhet të ketë një shkronjë të madhe, një të vogël, një numër dhe një karakter special")]
        [Display(Name = "Fjalëkalimi")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Konfirmo fjalëkalimin")]
        [Compare("Password", ErrorMessage = "Fjalëkalimi dhe konfirmimi i tij nuk përputhen.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} duhet të ketë të paktën {2} karaktere.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Fjalëkalimi dhe konfirmimi i tij nuk përputhen.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
