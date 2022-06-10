using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheCoreBanking.Authenticate.Services;
using TheCoreBanking.Data.Models;

namespace TheCoreBanking.Authenticate.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        [Remote(action:"IsEmailInUse",controller:"Administration")]
        public string Username { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [ModelBinder(BinderType =typeof(FormJsonBinder))]
        public TblStaffInformation staffInformation { get; set; }
        public List<IFormFile> Staffsignature { get; set; }
        public ForgotPasswordViewModel ForgotPasswords { get; set; }
        public ResetPasswordViewModel ResetPasswords { get; set; }
        public LoginInputModel loginInputModel { get; set; }
        public string ReturnUrl { get; set; }


    }

    public class RoleViewModel
    {
        [Required]
        [Display(Name = "RoleName")]
        public string RoleName { get; set; }
       
    }

    public class UserRoleViewModel
    {
        [Required]
        [Display(Name = "RoleName")]
        public string RoleName { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        public TblSecurityUsers securityUsers { get; set; }
    }
    
    public class PermissionViewModel
    {
        [Required]
        [Display(Name = "Permission")]
        public string Permission { get; set; }

    }




}
