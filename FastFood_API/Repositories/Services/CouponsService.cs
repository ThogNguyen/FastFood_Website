using AutoMapper;
using Data.DatabaseConnect;
using Data.Entity;
using Data.Models.AccountModels.Response;
using Data.Models.CouponModels;
using FastFood_API.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FastFood_API.Repositories.Services
{
    public class CouponsService : ICouponsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CouponsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BaseResponseMessage> CreateDiscountAsync(CouponForCreate couponDto)
        {
            var isNameExist = _context.Coupons.FirstOrDefault(x => x.Code == couponDto.Code);
            if (isNameExist != null)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Mã giảm giá đã tồn tại."
                };
            }

            Coupon discount = _mapper.Map<Coupon>(couponDto);
            discount.StartDate = DateTime.UtcNow.ToUniversalTime();
            //discount.EndDate = DateTime.UtcNow.AddDays(3).ToUniversalTime();

            await _context.Coupons.AddAsync(discount);
            await _context.SaveChangesAsync();
            return new BaseResponseMessage
            {
                IsSuccess = true,
                Errors = "Tạo mã giảm giá thành công."
            };
        }

        public async Task<IEnumerable<CouponForView>> GetAllDiscountsAsync()
        {
            var discounts = await _context.Coupons.ToListAsync();
            IEnumerable<CouponForView> view = _mapper.Map<IEnumerable<CouponForView>>(discounts);
            return view;
        }

        public async Task<CouponForView> GetDiscountByIdAsync(Guid id)
        {
            var coupon = await _context.Coupons.SingleOrDefaultAsync(x => x.Id == id);
            if (coupon == null)
            {
                throw new Exception("Không tìm thấy mã giảm giá này.");
            }
            CouponForView view = _mapper.Map<CouponForView>(coupon);
            return view;
        }

        public async Task<BaseResponseMessage> UpdateDiscountAsync(CouponForUpdate couponDto, Guid id)
        {
            var coupon = await _context.Coupons.SingleOrDefaultAsync(x => x.Id == id);

            var isNameExist = _context.Coupons.FirstOrDefault(x => x.Code == couponDto.Code);
            // nếu tồn tại và tên đang nhập khác tên với db
            if (isNameExist != null && coupon!.Code != couponDto.Code)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Mã giảm giá đã tồn tại."
                };
            }

            if (couponDto.EndDate <= couponDto.StartDate)
            {
                return new BaseResponseMessage
                {
                    IsSuccess = false,
                    Errors = "Ngày kết thúc không được nhỏ hơn hoặc bằng ngày bắt đầu."
                };
            }

            _mapper.Map(couponDto, coupon);
            _context.Coupons.Update(coupon!);
            await _context.SaveChangesAsync();
            return new BaseResponseMessage
            {
                IsSuccess = true,
                Errors = "Sửa mã giảm giá thành công."
            };
        }
    }
}
