using Microsoft.AspNetCore.Mvc;
using GymApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GymApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user =new IdentityUser{ UserName=model.Email, Email=model.Email};
                var result = await userManager.CreateAsync(user, model.Password);
                //automatic password hashing
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {   foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }
                }
            }
            return View(model);
        }
        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = userManager.FindByEmailAsync(email);
            if (user==null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        //to avoid an open redirect attack we check the return url before redirecting
                        //we can do that by checking in the if statement or by using return LocalRedirect(returnUrl)
                        //the downside of the second method is that it will throw an exception if it is not local which may not be
                        //the best way to handle this and will also affect the user experience
                        return Redirect(returnUrl);

                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "The Email or password is incorrect!");
                }
            }
            return View(model);
        }
        public IActionResult Cancel(string ReturnUrl)
        {
            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                //to avoid an open redirect attack we check the return url before redirecting
                //we can do that by checking in the if statement or by using return LocalRedirect(returnUrl)
                //the downside of the second method is that it will throw an exception if it is not local which may not be
                //the best way to handle this and will also affect the user experience
                return Redirect(ReturnUrl);

            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
    }
}
