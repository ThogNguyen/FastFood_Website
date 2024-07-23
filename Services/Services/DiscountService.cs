using AutoMapper;
using Domain.Data;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.DiscountModel;

namespace Services.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DiscountService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateDiscountAsync(DiscountForCreate discountDto)
        {
            try
            {
                Order discount = _mapper.Map<Order>(discountDto);
                await _context.AddAsync(discount);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<DiscountForView>> GetAllDiscountsAsync()
        {
            try
            {
                var discounts = await _context.Discounts.ToListAsync();
                IEnumerable<DiscountForView> view = _mapper.Map<IEnumerable<DiscountForView>>(discounts);
                return view;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<DiscountForView> GetDiscountByIdAsync(Guid id)
        {
            try
            {
                var discount = await _context.Discounts.SingleOrDefaultAsync(x => x.Id == id);
                DiscountForView view = _mapper.Map<DiscountForView>(discount);
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception("Don't have this Discount");
            }
        }

        public async Task<bool> UpdateDiscountAsync(DiscountForUpdate discountDto, Guid id)
        {
            try
            {
                var discount = await _context.Discounts.SingleOrDefaultAsync(x => x.Id == id);
                _mapper.Map(discountDto, discount);
                _context.Discounts.Update(discount);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
