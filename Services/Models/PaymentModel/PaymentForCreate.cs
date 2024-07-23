using System.ComponentModel.DataAnnotations;

namespace Services.Models.PaymentModel
{
    public class PaymentForCreate
    {
        public Guid Id { get; set; }
        public string PaymentStatus { get; set; }
    }
}
