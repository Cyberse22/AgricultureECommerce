using Newtonsoft.Json;

namespace OrderService.Models;

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
