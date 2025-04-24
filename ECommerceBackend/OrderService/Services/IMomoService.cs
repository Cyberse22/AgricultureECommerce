using OrderService.Models;

namespace OrderService.Services;

public interface IMomoService
{
    Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(CreateMomoPaymentModel  model);
    Task<string> PaymentExecuteAsync(IQueryCollection collection);
}