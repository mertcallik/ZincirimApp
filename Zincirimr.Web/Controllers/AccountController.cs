using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;
using Zincirimr.Data.Abstract;
using Zincirimr.Data.Models;
using Zincirimr.Web.Models.Mail;
using Zincirimr.Web.ViewModels;

namespace Zincirimr.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IMailSender _mailSender;

        public AccountController(IAuthRepository authRepository, IMapper mapper, IMailSender mailSender)
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _mailSender = mailSender;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AuthLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Lütfen tüm alanları doldurunuz");
                return View(model);
            }

            var result = await _authRepository.Login(model.Email ?? "", model.Password ?? "", model.RememberMe, true);
            if (!result.Succeeded)
            {
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Mailinize gönderilen bağlantıya giderek hesabınızı onaylayınız.");
                    return View(model);
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", $"Ard Arda çok fazla deneme yaptınız,Lütfen {DateTime.Now.AddMinutes(5).ToString("t")}'de tekrar deneyiniz");
                    return View(model);
                }
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                TempData["icon"] = "error";
                TempData["confirmMessage"] = "Kullanıcı adı veya şifre hatalı";
                return View(model);
            }
            TempData["icon"] = "success";
            TempData["confirmMessage"] = "Başarıyla Giriş Yaptınız";

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AuthRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Tüm alanları doldurunuz");
                return View(model);
            }

            var user = _mapper.Map<AppUser>(model);
            var result = await _authRepository.Register(user, model.Password);
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(err => ModelState.AddModelError("", err.Description));
                return View(model);
            }

            var confirmToken = _authRepository.GenerateConfirmToken(user.Id).Result;
            var confirmurl = Url.Action("ConfirmEmail", new { userId = user.Id, token = confirmToken });

            TempData["confirmMessage"] = "E-postanıza giderek hesabınızı doğrulayınız";
            await _mailSender.SendEmailAsync(user.Email, "Hesabınızı Dogrulayınız", $"Lütfen <a href='https://localhost:7193{confirmurl}'>tıklayınız</a>");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(token))
            {
                TempData["confirmMessage"] = "Hesabınız Onaylanamadı";
                return RedirectToAction("Index", "Home");
            }

            var confirmEmail = _authRepository.ConfirmEmail(userId, token);
            if (!confirmEmail.Result.Succeeded)
            {
                TempData["confirmMessage"] = "Hesabınız onaylanamadı";
                return RedirectToAction("Index", "Home");
            }
            TempData["confirmMessage"] = "Hesabınız başarıyla onaylandı";


            return RedirectToAction("Index", "Home");
        }
    }
}
