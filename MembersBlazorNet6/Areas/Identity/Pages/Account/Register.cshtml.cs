using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MembersBlazorNet6.Data;
using MembersBlazorNet6.Data.Models;
using MembersBlazorNet6.Data.Models.UnionInstitutionModels;
using MembersBlazorNet6.Services.Validations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MailKit.Net;
using MembersBlazorNet6.Data.Models.Settings;
using MimeKit;
using MailKit.Net.Smtp;
using MembersBlazorNet6.StaticData;
using System.Security.Claims;
using MembersBlazorNet6.Services.AccessServices;
using Microsoft.AspNetCore.Components.Authorization;

namespace MembersBlazorNet6.Areas.Identity.Pages.Account
{
    [Authorize(Policy = "Super administrator")]
    public class RegisterModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IAccessService _accessService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IAccessService accessService)
        {
            _userManager = userManager;
            _context = context;
            _accessService = accessService;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string? ReturnUrl { get; set; }
        public UnionInstitution? UnionInstitution { get; set; }
        public EmailMessage EmailMessage { get; set; }
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Pole wymagane")]
            [EmailAddress(ErrorMessage = "Wpisz poprawny email")]
            [Display(Name = "Email")]
            public string? Email { get; set; }

            [Display(Name = "Imię")]
            [Required(ErrorMessage = "Pole 'Imię' jest wymagane")]
            public string? FirstName { get; set; }

            [Display(Name = "Imię")]
            [Required(ErrorMessage = "Pole 'Imię' jest wymagane")]
            public string? LastName { get; set; }

            [Display(Name = "Telefon")]
            [Required(ErrorMessage = "Pole 'Telefon' jest wymagane")]
            public string? PhoneNumber { get; set; }

            public int? UnionInstitutionId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int? id, string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
            if (id == null)
            {
                return NotFound();
            }
            UnionInstitution = await _context.UnionInstitution.FirstOrDefaultAsync(i => i.Id == id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? unionId, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                if (unionId != null)
                {
                    UnionInstitution = await _context.UnionInstitution
                        .FirstOrDefaultAsync(i => i.Id == unionId);
                    StatusMessage = "Błąd, Wystapił błąd połączenia z bazą danych. Spróbuj ponownie";
                }
                return Page();
            }
            string role = Request.Form["rdUserRole"].ToString();
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    PhoneNumber = Input.PhoneNumber,
                    RegisterDate = DateTime.Now
                };

                var userFromDb = await _context.ApplicationUser.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (userFromDb != null)
                {
                    var accessFromDb = await _accessService.GetUserAccessesByUserIdAndUnionInstId(userFromDb.Id, unionId, role);
                    if (accessFromDb != null)
                    {
                        if (accessFromDb.Access == RoleSD.SuperAdmin || accessFromDb.Access == RoleSD.Admin)
                        {
                            return LocalRedirect("/admin/users/usersList");
                        }
                        else
                        {
                            return LocalRedirect($"/admin/institution/UnionInstitutionUsersList/{accessFromDb.UnionInstitutionId}");
                        }
                    }
                    if (role == RoleSD.SuperAdmin || role == RoleSD.Admin)
                    {
                        //var claim = new Claim(role, role);
                        //await _userManager.AddClaimAsync(userFromDb, claim);
                        var result = await _accessService.CreateSuperAdminAccess(userFromDb, role);
                        if (result)
                        {
                            return LocalRedirect("/admin/users/usersList");
                        }
                        else
                        {
                            StatusMessage = "Błąd, Wystąpił błąd zapisu, spróbuj ponownie";
                            return Page();
                        }
                    }
                    if (unionId != null)
                    {
                        //var claim = new Claim(role, UnionInstitution.Id.ToString());
                        //await _userManager.AddClaimAsync(userFromDb, claim);
                        var result = await _accessService.CreateUserAccess(userFromDb, unionId, role);
                        if (result)
                        {
                            return LocalRedirect($"/admin/institution/UnionInstitutionUsersList/{unionId}");
                        }
                        else
                        {
                            StatusMessage = "Błąd, Wystąpił błąd zapisu, spróbuj ponownie";
                            return Page();
                        }
                    }
                    return LocalRedirect($"/admin/institution/UnionInstitutionUsersList/{unionId}");
                }
                else
                {
                    var GeneratePassword = PasswordGenerator.GeneratePassword(true, true, true, true, 8);
                    var result = await _userManager.CreateAsync(user, GeneratePassword);
                    if (result.Succeeded)
                    {
                        if (role == RoleSD.SuperAdmin || role == RoleSD.Admin)
                        {
                            //var claim = new Claim(role, role);
                            //await _userManager.AddClaimAsync(user, claim);
                            var resultAccessSave = await _accessService.CreateSuperAdminAccess(user, role);
                            if (resultAccessSave)
                            {
                                return LocalRedirect("/admin/users/usersList");
                            }
                            else
                            {
                                StatusMessage = "Błąd, Wystąpił błąd zapisu, spróbuj ponownie";
                                return Page();
                            }
                        }
                        if (unionId != null)
                        {
                            //var claim = new Claim(role, UnionInstitution.Id.ToString());
                            //await _userManager.AddClaimAsync(userFromDb, claim);
                            var resultAccessSave = await _accessService.CreateUserAccess(userFromDb, unionId, role);
                            if (resultAccessSave)
                            {
                                return LocalRedirect($"/admin/institution/UnionInstitutionUsersList/{unionId}");
                            }
                            else
                            {
                                StatusMessage = "Błąd, Wystąpił błąd zapisu, spróbuj ponownie";
                                return Page();
                            }
                        }
                        //Email
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            protocol: Request.Scheme);
                        //nowa wiadomość
                        var emailMessage = new MimeMessage();
                        //pobranie danych z ustawień
                        var emailSetup = await _context.EmailPortsSetting.FirstOrDefaultAsync(e => e.Name == "Potwierdzenie rejestracji");
                        //adres z
                        MailboxAddress emailFrom = new MailboxAddress("Platforma Members ZNP", emailSetup.EmailFrom);
                        emailMessage.From.Add(emailFrom);
                        //adres do
                        MailboxAddress emailTo = new MailboxAddress("", Input.Email);
                        emailMessage.Subject = "Potwierdzenie założenia konta na platformie Members ZNP";
                        emailMessage.To.Add(emailTo);
                        //tworzenie treści wiadomości
                        BodyBuilder emailBody = new BodyBuilder();
                        emailBody.HtmlBody = $"<p>Witaj {user.FirstName} {user.LastName}</p>" +
                            $"<p>Twoje tymczasowe hasło {GeneratePassword}</p>" +
                            $"<p>Zostało utworzone dla ciebie konto na platformie Members ZNP</p>" +
                            $"<p>Aby potwierdzić swoje konto <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Kliknij tutaj</a>.</p>";
                        emailMessage.Body = emailBody.ToMessageBody();
                        //ustawienia smtp
                        SmtpClient client = new SmtpClient();
                        client.Connect(emailSetup.SmtpName, emailSetup.SmtpPort, true);
                        client.Authenticate(emailSetup.EmailFrom, emailSetup.Password);
                        client.Send(emailMessage);
                        client.Disconnect(true);
                        client.Dispose();
                        EmailMessage = new EmailMessage()
                        {
                            Title = emailMessage.Subject,
                            Adresat = Input.LastName + " " + Input.FirstName,
                            AdresEmail = Input.Email,
                            Content = emailBody.HtmlBody,
                            EmailDate = DateTime.Now
                        };
                        _context.EmailMessages.Add(EmailMessage);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return Page();
                    }
                }
            }
            return LocalRedirect($"/admin/institution/UnionInstitutionUsersList/{unionId}");
        }
    }
}
