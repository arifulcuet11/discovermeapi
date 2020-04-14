using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Domain.Models
{
    public class UserLogIn
    {
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class UserInfo : UserLogIn
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePicSrc { get; set; }
    }
    public class UserPasswordChange : UserLogIn
    {

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

    }
    public class UserPasswordReset : UserLogIn
    {
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }

    }

}
