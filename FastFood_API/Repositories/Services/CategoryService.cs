using AutoMapper;
using Data.DatabaseConnect;
using Data.Entity;
using Data.Models.AccountModels.Response;
using Data.Models.CategoryModels;
using FastFood_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FastFood_API.Repositories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BaseResponseMessage> CreateCategoryAsync(CategoryForCreate categoryDto)
        {
            var isCategoryNameExist = await _context.Categories.FirstOrDefaultAsync(x => x.Name == categoryDto.Name);
            if (isCategoryNameExist != null)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Tên loại đã tồn tại."
                };
            }

            Category category = _mapper.Map<Category>(categoryDto);
            category.CreateDate = DateTimeOffset.UtcNow;
            _context.Add(category);
            await _context.SaveChangesAsync();
            return new BaseResponseMessage
            {
                IsSuccess = true,
                Errors = "Thêm loại thành công."
            };
        }

        public async Task<IEnumerable<CategoryForView>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            var view = _mapper.Map<IEnumerable<CategoryForView>>(categories);
            return view;
        }

        public async Task<CategoryForView> GetCategoryByIdAsync(Guid id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                throw new Exception("Không tìm thấy loại này");
            }
            var view = _mapper.Map<CategoryForView>(category);
            return view;
        }

        public async Task<BaseResponseMessage> UpdateCategoryAsync(CategoryForUpdate categoryDto, Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Không tìm thấy loại này."
                };
            }

            var isCategoryNameExist = await _context.Categories.FirstOrDefaultAsync(x => x.Name == categoryDto.Name);

            if (isCategoryNameExist != null && category.Name != categoryDto.Name)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Tên loại đã tồn tại."
                };
            }

            _mapper.Map(categoryDto, category);
            _context.Update(category);
            await _context.SaveChangesAsync();
            return new BaseResponseMessage
            {
                IsSuccess = true,
                Errors = "Sửa thành công."
            };

            throw new NotImplementedException();
        }
    }
}
