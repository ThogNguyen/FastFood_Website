using Data.Entity;
using Data.Models.CategoryModels;
using Data.Models.ProductModels;
using FastFood_Client.HttpRepositories.Interfaces;
using FastFood_Client.HttpRepositories.Services;
using Microsoft.AspNetCore.Components;

namespace FastFood_Client.Pages.Admin.QLProduct
{
    public partial class AddProduct
    {
        [Inject]
        public IHttpProductService httpProductService { get; set; }
        [Inject]
        public IHttpCategoryService httpCategoryService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        private string statusMessage;


        ProductForCreate product = new ProductForCreate();
        private IEnumerable<CategoryForView> categories = new List<CategoryForView>();

        protected override async Task OnInitializedAsync()
        {
            categories = await httpCategoryService.GetCategoriesAsync();
        }

        private async Task HandleSubmit()
        {
            statusMessage = "Product added successfully!";
        }

        private string GetAlertClass()
        {
            if (statusMessage.StartsWith("Error"))
                return "alert-danger";
            if (statusMessage.StartsWith("Product added successfully"))
                return "alert-success";
            return "";
        }
    }
}
