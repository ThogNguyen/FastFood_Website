using Data.Models.AccountModels;
using FastFood_Client.HttpRepositories.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FastFood_Client.Pages.Accounts
{
    public partial class Register
    {
        private RegisterVM registerVM = new RegisterVM();
        [Inject]
        public IHttpAccountService AccountService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private async Task HandleRegister()
        {
            try
            {
                var response = await AccountService.Register(registerVM);

                if (response.IsSuccess)
                {
                    NavigationManager.NavigateTo("/login");
                }
                else
                {
                    Console.WriteLine(response.Errors);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
