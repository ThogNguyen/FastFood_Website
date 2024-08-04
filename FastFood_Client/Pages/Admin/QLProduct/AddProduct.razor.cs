using Data.Entity;
using Data.Models.CategoryModels;
using Data.Models.ProductModels;
using FastFood_Client.HttpRepositories.Interfaces;
using FastFood_Client.HttpRepositories.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http;

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

        [Inject]
        public HttpClient httpClient { get; set; }


        ProductForCreate product = new ProductForCreate();
        private IEnumerable<CategoryForView> categories = new List<CategoryForView>();

        protected override async Task OnInitializedAsync()
        {
            await LoadCategory();
        }

        private async Task LoadCategory()
        {
            categories = await httpCategoryService.GetCategoriesAsync();
        }

        private async Task CreateProduct()
        {
            await httpProductService.CreateProductAsync(product);
            navigationManager.NavigateTo("/products");
        }

    }
}
