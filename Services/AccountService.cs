using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HajurKoCarRental.Models.ViewModels;
using HajurKoCarRental.Models.DataModels;

namespace HajurKoCarRental.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        //Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordViewModel model);
    }
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                // Add other properties as needed
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Assign the user role to the new user
                await _userManager.AddToRoleAsync(user, "Customer");
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        //public async Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordViewModel model)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        //    return result;
        //}

        // Add other account-related methods as needed
    }
}
