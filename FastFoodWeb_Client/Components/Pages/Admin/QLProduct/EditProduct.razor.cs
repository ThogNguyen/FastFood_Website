using Data.Entity;
using Data.Models.CategoryModels;
using Data.Models.ProductModels;
using FastFoodWeb_Client.HttpRepositories.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Headers;
using Tewr.Blazor.FileReader;

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
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public Guid ProductId { get; set; }

        private ProductForUpdate product = new ProductForUpdate();
        private ProductForView productView = new ProductForView();
        private string _errorMessage;

        private bool isImageChanged = false;
        private ElementReference _input;
        [Parameter]
        public string ImgUrl { get; set; }
        [Parameter]
        public EventCallback<string> OnChange { get; set; }

        [Inject]
        public IFileReaderService FileReaderService { get; set; }

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

            // Sao chép dữ liệu từ productView vào product
            product = new ProductForUpdate
            {
                ProductName = productView.ProductName,
                Price = productView.Price,
                Image = productView.Image,
                Discount = productView.Discount,
                Description = productView.Description,
                IsActive = productView.IsActive,
                IsCombo = productView.IsCombo,
                Category_Id = productView.Category_Id
            };
        }

        private async Task UpdateProduct()
        {
            try
            {
                if (isImageChanged)
                {
                    product.Image = ImgUrl;
                }
                await httpProductService.UpdateProductAsync(ProductId, product);
                NavigationManager.NavigateTo("/products");
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
        }

        private async Task HandleSelected()
        {
            foreach (var file in await FileReaderService.CreateReference(_input).EnumerateFilesAsync())
            {
                if (file != null)
                {
                    var fileInfo = await file.ReadFileInfoAsync();
                    using (var ms = await file.CreateMemoryStreamAsync(4 * 1024))
                    {
                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        content.Add(new StreamContent(ms, (int)ms.Length), "image", fileInfo.Name);

                        ImgUrl = await httpProductService.UploadProductImage(content);
                        isImageChanged = true;
                        StateHasChanged();
                    }
                }
            }
        }
    }
}
