using AutoMapper;
using Domain.Data;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Models.PaymentModel;

namespace Services.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PaymentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreatePaymentAsync(PaymentForCreate paymentDto)
        {
            try
            {
                Order payment = _mapper.Map<Order>(paymentDto);
                await _context.AddAsync(payment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PaymentForView>> GetAllPaymentsAsync()
        {
            try
            {
                var payments = await _context.Payments.ToListAsync();
                IEnumerable<PaymentForView> view = _mapper.Map<IEnumerable<PaymentForView>>(payments);
                return view;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<PaymentForView> GetPaymentByIdAsync(Guid id)
        {
            try
            {
                var payment = await _context.Payments.SingleOrDefaultAsync(x => x.Id == id);
                PaymentForView view = _mapper.Map<PaymentForView>(payment);
                return view;
            }
            catch (Exception ex)
            {
                throw new Exception("Don't have this Discount");
            }
        }

        public async Task<bool> UpdatePaymentAsync(PaymentForUpdate paymentDto, Guid id)
        {
            try
            {
                var payment = await _context.Payments.SingleOrDefaultAsync(x => x.Id == id);
                _mapper.Map(paymentDto, payment);
                _context.Payments.Update(payment);
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
