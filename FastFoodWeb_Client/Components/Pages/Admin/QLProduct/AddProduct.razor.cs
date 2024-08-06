using Data.Entity;
using Data.Models.CategoryModels;
using Data.Models.ProductModels;
using FastFoodWeb_Client.HttpRepositories.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FastFoodWeb_Client.Components.Pages.Admin.QLProduct
{
    public partial class AddProduct
    {
        [Inject]
        public IHttpProductService httpProductService { get; set; }
        [Inject]
        public IHttpCategoryService httpCategoryService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        ProductForCreate product = new ProductForCreate();
        private IEnumerable<CategoryForView> categories = new List<CategoryForView>();

        protected override async Task OnInitializedAsync()
        {
            await LoadCategory();
        }

        private async Task LoadCategory()
        {
            categories = await httpCategoryService.GetAllCategory();
        }
        private async Task Create()
        {
            await httpProductService.CreateProduct(product);
            navigationManager.NavigateTo("/products");
        }
        private void AssignImage(string imgUrl) => product.Image = imgUrl;
    }
}
