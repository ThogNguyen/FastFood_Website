﻿namespace Services.Models.OrderModel
{
    public class OrderForCreate
    {
        public Guid Id { get; set; }
        public Guid DiscountId { get; set; }
        public Guid UserId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; } = "Pending";
        public decimal VAT { get; set; }
        public string? Note { get; set; }
        public DateTime ShippingDate { get; set; } = DateTime.Now;
        public string ShippingStatus { get; set; } = "Pending";
        public string ShippingAddress { get; set; }
    }
}
