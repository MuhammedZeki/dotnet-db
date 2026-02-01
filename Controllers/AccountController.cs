using System.Security.Claims;
using dotnet_db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_db.Controllers;

[Route("account")]
public class AccountController : Controller
{


    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }


    [HttpGet("login")]
    public ActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(AccountLoginModel model, string? ReturnUrl)
    {

        if (ModelState.IsValid)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {

                await _signInManager.SignOutAsync();
                var results = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                if (results.Succeeded)
                {

                    await _userManager.ResetAccessFailedCountAsync(user);
                    await _userManager.SetLockoutEndDateAsync(user, null);

                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (results.IsLockedOut)
                {
                    var lockoutDate = await _userManager.GetLockoutEndDateAsync(user);
                    var timeLeft = lockoutDate.Value - DateTime.UtcNow;
                    ModelState.AddModelError("", $"Hesabınız kitlendi. Lütfen {timeLeft.Minutes + 1} dakika sonra tekrar deneyin");
                }
                else
                {
                    ModelState.AddModelError("", "Şifre hatalı");
                }
            }
            else
            {
                ModelState.AddModelError("", "Email Bulunamadı!");
            }
        }
        return View(model);
    }


    [HttpGet("create")]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create(AccountCreateModel model)
    {
        if (ModelState.IsValid)
        {

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
        }
        return View(model);
    }


    [Authorize]
    [HttpGet("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("access-denied")]
    public ActionResult AccessDenied()
    {
        return View();
    }

    [Authorize]
    [HttpGet("settings/user-edit")]
    public async Task<ActionResult> UserEdit()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);

        return View(
            new AccountEditUserModel
            {
                FullName = user.FullName,
                Email = user.Email!
            }
        );
    }


    [Authorize]
    [HttpPost("settings/user-edit")]
    public async Task<ActionResult> UserEdit(AccountEditUserModel model)
    {

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //Giriş yapan kull. id'si
        var user = await _userManager.FindByIdAsync(userId!);

        if (ModelState.IsValid)
        {
            if (user != null)
            {
                user.FullName = model.FullName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["message"] = "Bilgileriniz güncellendi!";
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

        }
        return View(model);
    }
}