using Data.Models.AccountModels;
using FastFood_Client.HttpRepositories.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FastFood_Client.Pages.Accounts
{
    public partial class Register
    {
        private RegisterVM registerVM = new RegisterVM();
        [Inject]
        public IHttpAccountService accountService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
    }
}
