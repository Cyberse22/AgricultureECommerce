namespace OrderService.Data
{
    public class MomopaymentRequest
    {
        public string? PartnerCode { get; set; } = default!;
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        public string OrderId { get; set; } = default!;
        public string OrderInfo { get; set; } = default!;
        public string RedirectUrl { get; set; } = default!;
        public string IpnUrl { get; set; } = default!;
        public long Amount { get; set; } = default!;
        public string Currency { get; set; } = default!;
        public string RequestType { get; set; } = default!;
        public string Signature { get; set; } = default!;
        public string ExtraData { get; set; } = default!;
        public string Lang { get; set; } = default!;
    }
}
