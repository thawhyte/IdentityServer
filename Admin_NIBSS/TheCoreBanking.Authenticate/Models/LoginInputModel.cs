// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace TheCoreBanking.Authenticate.Models
{
    public class LoginInputModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
     
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
    public class AuthenticateCodeInputModel
    {
        public string Username { get; set; }
        [Required]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }
    }
}