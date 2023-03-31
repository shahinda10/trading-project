using Microsoft.Build.Framework;

namespace SignUpForm.Models.ViewModel
{
    public class LoginSignUpViewModel
    {
        [Required]
        public string username { get; set; }

        //public string email { get; set; }
        public string password { get; set; }
        //public string confirmpassword { get; set; }
        //public bool isactive { get; set; }
        //public bool isremember { get; set; }
    }
}
