namespace WeQuiz.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using WeQuiz.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using static Data.DataConstants.User;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress(ErrorMessage = "Въведения e-mail е невалиден")]
            [Display(Name = "E-mail адрес")]
            public string Email { get; set; }

            [Display(Name = "Пълно име")]
            [StringLength(FullNameMaxLength, ErrorMessage = "Името трябва да е между {2} и {1} символа.", MinimumLength = FullNameMinLength)]
            public string FullName { get; set; }

            [Display(Name = "Име в системата")]
            [StringLength(AliasMaxLength, ErrorMessage = "Името трябва да е между {2} и {1} символа.", MinimumLength = AliasMinLength)]
            public string Alias { get; set; }

            [Required]
            [StringLength(PasswordMaxLength, ErrorMessage = "Паролата трябва да е между {2} и {1} символа.", MinimumLength = PasswordMinLength)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Повтори парола")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FullName = Input.FullName,
                    Alias = Input.Alias,
                    SchoolId = 0
                };

                var result = await this.userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
