using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly;
using SignUpForm.Data;
using SignUpForm.Models;
using SignUpForm.Models.ViewModel;
using System.Security.Claims;

namespace SignUpForm.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext context;

        public AccountController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LoginSignUpViewModel model)

        {

            if (ModelState.IsValid)
            {
                var data = context.users.Where(e => e.username == model.username).SingleOrDefault();
                if(data!= null)
                {
                    bool isValid = (data.username == model.username && data.password == model.password);
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, model.username) }, 
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal=new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("username",model.username);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["errorMessage"]= "Invalid Password";

                        return View(model);
                    }
                }
                else
                {
                    TempData["errorMessage"] = "User Not Found";
                    return View(model);
                }
            }
            else
            {
                return View(model);

            }
        }

        public IActionResult logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedcookies = Request.Cookies.Keys;
            foreach(var cookies in storedcookies)
            {
                Response.Cookies.Delete(cookies);
            }
            return RedirectToAction("LogIn", "Account");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = new User()
                {
                    username = model.username,
                    email = model.email,
                    password = model.password


                };
                context.users.Add(data);
                context.SaveChanges();
                TempData["successMessage"]="you are eligible to login, please fill the crendentials then login";
                return RedirectToAction("LogIn");
                
            }
            else
            {
                TempData["errorMessage"] = "empty form cannot be submitted";
                return View(model);
            }
        }
    }
}
