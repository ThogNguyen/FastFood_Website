namespace Domain.Entity.Base
{
    public abstract class EntityAuditBase
    // dùng abstract để tránh việc khởi tạo đối tượng bậy bạ
    {
        public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? UpdateDate { get; set; } = DateTimeOffset.Now;
    }
}
