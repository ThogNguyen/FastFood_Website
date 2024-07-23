using Services.Models.PaymentModel;

namespace Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> CreatePaymentAsync(PaymentForCreate paymentDto);
        Task<bool> UpdatePaymentAsync(PaymentForUpdate paymentDto, Guid id);
        Task<IEnumerable<PaymentForView>> GetAllPaymentsAsync();
        Task<PaymentForView> GetPaymentByIdAsync(Guid id);
    }
}
