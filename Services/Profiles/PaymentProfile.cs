using AutoMapper;
using Domain.Entity;
using Services.Models.PaymentModel;

namespace Services.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            // chuyển từ giao diện hiển thị tới DB
            CreateMap<PaymentForCreate, Payment>();
            CreateMap<PaymentForUpdate, Payment>();
            // cái này hiển thị nên sẽ làm ngược lại từ DB ra giao diện
            CreateMap<Payment, PaymentForView>();
        }
    }
}
