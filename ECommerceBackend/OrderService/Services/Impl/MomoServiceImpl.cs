using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderService.Models;
using OrderService.Repositories;
using RestSharp;

namespace OrderService.Services;

public class MomoServiceImpl : IMomoService
{
    private readonly IOptions<MomoOptionModel> _options;
    private readonly IOrderRepository _orderRepository;

    public MomoServiceImpl(IOptions<MomoOptionModel> options, IOrderRepository orderRepository)
    {
        _options = options;
        _orderRepository = orderRepository;
    }

    private string ComputeHmacSha256(string message, string secretKey)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(CreateMomoPaymentModel model)
    {
        var order = await _orderRepository.GetOrderByIdAsync(model.OrderId);
        if (order == null) throw new Exception("Order not found");

        var orderInfo = $"Customer: {model.FullName}. Nội dung: {model.OrderInfo}";
        var rawData =
            $"partnerCode={_options.Value.PartnerCode}" +
            $"&accessKey={_options.Value.AccessKey}" +
            $"&requestId={model.OrderId}" +
            $"&amount={model.Amount}" +
            $"&orderId={model.OrderId}" +
            $"&orderInfo={orderInfo}" +
            $"&returnUrl={_options.Value.ReturnUrl}" +
            $"&extraData=";

        var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

        var client = new RestClient(_options.Value.MomoApiUrl);
        var request = new RestRequest { Method = Method.Post };
        request.AddHeader("Content-Type", "application/json");

        var requestData = new
        {
            accessKey = _options.Value.AccessKey,
            partnerCode = _options.Value.PartnerCode,
            requestType = _options.Value.RequestType,
            notifyUrl = _options.Value.NotifyUrl,
            returnUrl = _options.Value.ReturnUrl,
            orderId = model.OrderId,
            amount = model.Amount.ToString(),
            orderInfo,
            requestId = model.OrderId,
            extraData = "",
            signature
        };

        request.AddJsonBody(requestData);
        var response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
            throw new Exception("Failed to create MoMo payment");

        return JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content)!;
    }
    

    public async Task<string> PaymentExecuteAsync(IQueryCollection query)
    {
        // Handle MoMo return URL callback here...
        return "Payment received successfully";
    }
}