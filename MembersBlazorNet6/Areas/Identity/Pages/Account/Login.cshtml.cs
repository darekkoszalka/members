using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MembersBlazorNet6.Data.Models;
using MembersBlazorNet6.Data;
using Microsoft.EntityFrameworkCore;
using MembersBlazorNet6.StaticData;

namespace MembersBlazorNet6.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public LoginModel(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage ="*Pole 'Email' jest wymagane")]
            [EmailAddress(ErrorMessage = "Wpisz poprawny adres email")]
            public string? Email { get; set; }

            [Required(ErrorMessage ="*Podaj hasło")]
            [DataType(DataType.Password)]
            public string? Password { get; set; }

            public bool RememberMe { get; set; }


        }

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
        
            if (ModelState.IsValid)
            {
                
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    ApplicationUser = await _context.ApplicationUser
                .FirstOrDefaultAsync(u => u.Email == Input.Email);

                    if(ApplicationUser != null)
                    {
                        ApplicationUser.LastLoginDate = DateTime.Now;

                        _context.ApplicationUser.Update(ApplicationUser);

                        await _context.SaveChangesAsync();
                    }            
                    
                        return LocalRedirect("/");
                    
                    
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Twoje konto zostało zablokowane. Skontaktuj się z administratorem");
                    return Page();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Błędny login lub hasło.");
                    return Page();
                }
            }

            
            return Page();
        }
    }
}
