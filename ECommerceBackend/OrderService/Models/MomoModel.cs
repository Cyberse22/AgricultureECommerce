using Newtonsoft.Json;

namespace OrderService.Models
{
    public class MomoModel
    {
        public class MomoIpnRequest
        {
            [JsonProperty("partnerCode")]
            public string? partnerCode { get; set; }
            [JsonProperty("requestId")]
            public string? requestId { get; set; }
            [JsonProperty("orderId")]
            public string? orderId { get; set; }
            [JsonProperty("amount")]
            public long? amount { get; set; }
            [JsonProperty("responseTime")]
            public long? responseTime { get; set; }
            [JsonProperty("message")]
            public string? message { get; set; }
            [JsonProperty("orderInfo")]
            public int? resultCode { get; set; }
            [JsonProperty("orderType")]
            public string? payUrl { get; set; }
            [JsonProperty("shortLink")]
            public string? shortLink { get; set; }
        }

        public class CreateMomoPaymentRequest
        {
            public string? Id { get; set; }
            public string? Total { get; set; }
            public string? OrderID { get; set; }
        }

        
        public class MomoPaymentResponse
        {
            [JsonProperty("partnerCode")]
            public string? PartnerCode { get; set; }

            [JsonProperty("orderId")]
            public string? OrderId { get; set; }

            [JsonProperty("requestId")]
            public string? RequestId { get; set; }

            [JsonProperty("amount")]
            public long? Amount { get; set; }

            [JsonProperty("responseTime")]
            public long? ResponseTime { get; set; }

            [JsonProperty("message")]
            public string? Message { get; set; }

            [JsonProperty("resultCode")]
            public int? ResultCode { get; set; }

            [JsonProperty("payUrl")]
            public string? PayUrl { get; set; }

            [JsonProperty("deeplink")]
            public string? Deeplink { get; set; }

            [JsonProperty("qrCodeUrl")]
            public string? QrCodeUrl { get; set; }
        } 
        public class MomoCreatePaymentResponseModel
        {
            [JsonProperty("payUrl")]
            public string? PayUrl { get; set; }

            [JsonProperty("deeplink")]
            public string? Deeplink { get; set; }

            [JsonProperty("qrCodeUrl")]
            public string? QrCodeUrl { get; set; }

            [JsonProperty("resultCode")]
            public int ResultCode { get; set; }

            [JsonProperty("message")]
            public string? Message { get; set; }
        }
    }
}