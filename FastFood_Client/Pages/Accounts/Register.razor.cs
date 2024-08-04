namespace FastFood_Client.Pages.Accounts
{
    public partial class Register
    {
        private RegisterVM registerVM = new RegisterVM();
        [Inject]
        public IHttpAccountService accountService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private async Task HandleRegister()
        {
            try
            {
                var response = await accountService.Register(registerVM);

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
        private async Task HandleRegister()
        {
            try
            {
                var response = await accountService.Register(registerVM);

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
