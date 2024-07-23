using Domain.Entity.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class OrderDetail : EntityAuditBase
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey(nameof(ProductId))]
        // dùng nameof cho không bị bug tào lao vì mình chỉ định đích danh ID đó, vì dùng chuỗi thì ở dưới nếu sai thì nó không báo bug (đây là chuỗi nè [ForeignKey("ProductId")] ko nên dùng như vậy )
        public Guid ProductId { get; set; }

        public Product? Product { get; set; }
        // để null sẵn khi migration không bị bug, còn việc không null thì phải cấp phát vùng nhớ và khởi tạo đối tượng cho nó.
        [Required]
        [ForeignKey(nameof(OrderId))]
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;

    }
}
