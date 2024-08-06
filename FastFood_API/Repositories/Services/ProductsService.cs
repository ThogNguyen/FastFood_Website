using AutoMapper;
using Data.DatabaseConnect;
using Data.Entity;
using Data.Models.AccountModels.Response;
using Data.Models.ProductModels;
using FastFood_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FastFood_API.Repositories.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BaseResponseMessage> CreateProductAsync(ProductForCreate productDto)
        {
            // kiem tra ten san pham co ton tai chua
            var isProductNameExist = _context.Products.FirstOrDefault(x => x.ProductName == productDto.ProductName);
            if (isProductNameExist != null)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Tên sản phẩm đã tồn tại."
                };
            }

            if (productDto.Discount > productDto.Price)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Giá giảm không được lớn hơn giá sản phẩm."
                };
            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == productDto.Category_Id);
            if (!categoryExists)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Loại sản phẩm không tồn tại."
                };
            }

            var product = _mapper.Map<Product>(productDto);
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return new BaseResponseMessage
            {
                IsSuccess = true,
                Errors = "Thêm sản phẩm thành công."
            };
        }

        public async Task<IEnumerable<ProductForView>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            IEnumerable<ProductForView> view = _mapper.Map<IEnumerable<ProductForView>>(products);
            return view;
        }

        public async Task<ProductForView> GetProductByIdAsync(Guid id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                throw new Exception("Không tìm thấy sản phẩm này");
            }
            ProductForView view = _mapper.Map<ProductForView>(product);
            return view;
        }

        public async Task<BaseResponseMessage> UpdateProductAsync(ProductForUpdate productDto, Guid id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);

            // kiem tra ten san pham co ton tai chua
            var isProductNameExist = _context.Products.FirstOrDefault(x => x.ProductName == productDto.ProductName);
            if (isProductNameExist != null && product != isProductNameExist)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Tên sản phẩm đã tồn tại."
                };
            }

            if (productDto.Discount > productDto.Price)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Giá giảm không được lớn hơn giá sản phẩm."
                };
            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == productDto.Category_Id);
            if (!categoryExists)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Loại sản phẩm không tồn tại."
                };
            }

            _mapper.Map(productDto, product);
            _context.Products.Update(product!);
            await _context.SaveChangesAsync();
            return new BaseResponseMessage
            {
                IsSuccess = true,
                Errors = "Sửa sản phẩm thành công."
            };
        }
        public async Task<IEnumerable<ProductForView>> GetProductsAsync(
        string? keyword,
        string? sortOption,
        Guid? categoryId)
        {
            IEnumerable<ProductForView> products = await _context.Products
                .Include(p => p.Category) // Include category để lấy tên danh mục
                .Select(p => new ProductForView
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Image = p.Image,
                    Description = p.Description,
       
                    CategoryName = p.Category.Name // Lấy tên danh mục
                })
                .ToListAsync();

            // Tìm kiếm theo từ khóa
            if (!string.IsNullOrEmpty(keyword))
            {
                products = products.Where(p => p.ProductName.ToLower().Contains(keyword.ToLower()));
            }

            // Lọc theo danh mục
            if (categoryId.HasValue)
            {
                products = products.Where(p => p.Category_Id == categoryId.Value);
            }

            // Sắp xếp theo tên (A-Z hoặc Z-A)
            if (sortOption == "asc")
            {
                products = products.OrderBy(p => p.ProductName);
            }
            else if (sortOption == "desc")
            {
                products = products.OrderByDescending(p => p.ProductName);
            }
            // Sắp xếp theo giá (tăng dần hoặc giảm dần)
            else if (sortOption == "price-asc")
            {
                products = products.OrderBy(p => p.Price);
            }
            else if (sortOption == "price-desc")
            {
                products = products.OrderByDescending(p => p.Price);
            }

            return products;
        }
    }
}
