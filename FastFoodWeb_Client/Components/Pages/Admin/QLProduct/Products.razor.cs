using Data.Models.ProductModels;
using FastFoodWeb_Client.HttpRepositories.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace FastFoodWeb_Client.Components.Pages.Admin.QLProduct
{
    public partial class Products
    {
        private IEnumerable<ProductForView> products = new List<ProductForView>();

        [Inject]
        private IHttpProductService? productService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            products = await productService!.GetAllProductsAsync();
        }

        private string FormatCurrency(decimal amount)
        {
            var cultureInfo = new CultureInfo("vi-VN");
            var numberFormat = cultureInfo.NumberFormat;

            numberFormat.CurrencySymbol = "VND";
            numberFormat.CurrencyDecimalDigits = 0;

            return amount.ToString("C", numberFormat);
        }
    }
}
