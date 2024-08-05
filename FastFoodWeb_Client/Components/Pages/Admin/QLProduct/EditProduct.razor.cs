using Data.Models.CategoryModels;
using Data.Models.ProductModels;
using FastFoodWeb_Client.HttpRepositories.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FastFoodWeb_Client.Components.Pages.Admin.QLProduct
{
    public partial class EditProduct
    {
        [Inject]
        public IHttpProductService httpProductService { get; set; }
        [Inject]
        public IHttpCategoryService httpCategoryService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public HttpClient httpClient { get; set; }

        [Parameter]
        public Guid ProductId { get; set; }

        private ProductForUpdate product = new ProductForUpdate();
        private ProductForView productView = new ProductForView();

        private IEnumerable<CategoryForView> categories = new List<CategoryForView>();
        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
            await LoadProduct();
        }

        private async Task LoadCategories()
        {
            categories = await httpCategoryService.GetAllCategory();
        }

        private async Task LoadProduct()
        {
            productView = await httpProductService.GetProductByIdAsync(ProductId);
        }


        private async Task UpdateProduct()
        {
            await httpProductService.UpdateProductAsync(ProductId, product);
        }

        private void OnImageChange(InputFileChangeEventArgs e)
        {
            // Handle file upload
        }
    }
}
