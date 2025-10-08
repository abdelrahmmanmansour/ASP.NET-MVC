using Company.Pro.DAL.Models;
using Company.Pro.PL.Dtos;
using Company.Pro.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Identity.Client;
using System.Net;
using System.Threading.Tasks;

namespace Company.Pro.PL.Controllers
{
    // AccountController => MVC Controller
    // Password : P@@$$w0rd => Me
    // Password : W@ssW0rd => Walla Mohsen
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp

        // GET: Account/SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: Account/SignUp
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                // Check If UserName Or Email Already Exists:
                // You Can Use _userManager.FindByNameAsync(model.UserName) Or _userManager.FindByEmailAsync(model.Email)
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is null)
                    {
                        // Manual Mapping:
                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree
                        };
                        var result = await _userManager.CreateAsync(user, model.Password); // To Save User In DataBase
                        if (result.Succeeded) // If User Created Successfully
                        {
                            return RedirectToAction("SignIn", "Account"); // Redirect To SignIn Action In Account Controller
                        }
                        foreach (var error in result.Errors) // If User Not Created Successfully
                        {
                            ModelState.AddModelError(string.Empty, error.Description); // To Show Error Message
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid SignUp !!"); // To Show Error Message
            }
            return View(model);
        }

        #endregion

        #region SignIn

        // GET: Account/SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        // POST: Account/SignIn
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)  // If User Found
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password); // To Check Password 
                    if (flag) // If Password Is Correct
                    {
                        // SignIn User

                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                        if (result.Succeeded) // If User Signed In Successfully
                        {
                            return RedirectToAction("Index", "Home"); // Redirect To Index Action In Home Controller
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid SignIn !!"); // To Show Error Message
            }

            return View(model);
        }
        #endregion

        #region SignOut

        [HttpGet] // GET: Account/SignOut
        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult(); // SignOut User
            return RedirectToAction("SignIn", "Account"); // Redirect To SignIn Action In Account Controller
        }
        #endregion

        #region ForgetPassword

        [HttpGet] // GET: Account/ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUL(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                // Check If Email Exists:
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    // Generate Token To Reset Password:
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Create URL To Reset Password:
                    var ulr = Url.Action("ResetPassword", "Account", new { email = model.Email, token }, Request.Scheme);

                    // Send Email To User Contain Reset Password Link
                    // Must Make Class Represent Email Service:  TO, Subject, Body
                    // Create Email:
                    var email = new Email()
                    {
                        TO = model.Email,
                        Subject = "Reset Password",
                        Body = ulr
                    };
                    var flag = EmailSetting.SendEmail(email);
                    if (flag) // If Email Sent Successfully
                    {
                        // Check Your Email To Reset Your Password
                        return RedirectToAction("CheckYourInbox", "Account");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Reset Password !!"); // To Show Error Message
            return View("ForgetPassword", model);
        }

        #endregion

        #region ResetPassword

        [HttpGet]
        public IActionResult ResetPassword(string email, string token) // Bind Email + Token From Query String
        {
            TempData["email"] = email; // To Use Email In View
            TempData["token"] = token; // To Use Token In View
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model) // Pa$$wOrd => new Password
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string; // To Use Email In View
                var token = TempData["token"] as string; // To Use Token In View
                // Check If Email Exists:
                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    var Result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword); // Reset Password
                    if (Result.Succeeded) // If Password Reset Successfully
                    {
                        return RedirectToAction("SignIn", "Account"); // Redirect To SignIn Action In Account Controller
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Reset Password !!"); // To Show Error Message
            return View(model);
        }

        #endregion

        #region CheckYourInbox

        [HttpGet] // GET: Account/CheckYourInbox
        public IActionResult CheckYourInbox()
        {
            return View(); // View To Check Your Email
        }

        #endregion

    }
}
