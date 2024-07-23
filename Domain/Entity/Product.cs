using Domain.Entity.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Product : EntityAuditBase
    // kế thừa EntityAuditBase để xài lại đỡ khai báo liên tục trùng lặp, giảm bớt sự phụ thuộc
    // đúng với quy tắc S: single responsibility principle. Mỗi class chỉ được đảm nhận 1 chức năng nhất định(tổng quan)
    {
        [Key]
        public Guid Id { get; set; }
        // guid là key tự sinh bảo mật tốt :D
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public decimal Discount { get; set; } // discount này là giảm giá trên sản phẩm
        // dùng decimal số thập phân cho mục đích chỉ định độ chính xác
        // Kiểu dữ liệu Decimal cung cấp cho bạn mức độ chính xác cao và dễ dàng tránh lỗi làm tròn 128 bit (28-29 chữ số có nghĩa). Trong khi double chỉ làm tròn 64 bit ( 15-16 chữ số có nghĩa), còn float thì tệ hơn chỉ 32 bit :D
        [Required]
        public decimal Price { get; set; }
        // tiền bạc thì nên chính xác thay vì tốc độ :D decimal chậm nhưng chính xác né bị mn mắng dev ngu
        [Required]
        public bool IsValid { get; set; } = true;
        // làm cái này mục đích để ẩn hiện sản phẩm còn bán hay hong
        [Required]
        public bool IsCombo { get; set; } = false;
        // làm cái này để mốt Filter cho dễ :D
        public string? Description { get; set; }

    }
}
