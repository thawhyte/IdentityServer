using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheCoreBanking.Authenticate.Models
{
  
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "We need your email to send you a reset link!")]
        //[Display(Name = "Your account email")]
        [EmailAddress(ErrorMessage = "Not a valid email--what are you trying to do here?")]
        //private readonly string _Email;

        //public string Email
        //{
        //    get { return _Email; }
        //}
        //public ForgotPasswordViewModel(string EmailAddress)
        //{
        //    _Email = EmailAddress;
        //}
        public string Email { get; set; }
    }
}
