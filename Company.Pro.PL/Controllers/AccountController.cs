using Company.Pro.DAL.Models;
using Company.Pro.PL.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.Pro.PL.Controllers
{
    // AccountController => MVC Controller
    // Password : P@ssW0rd => Me
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
            if(ModelState.IsValid) // Server Side Validation
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

        #endregion
    }
}
