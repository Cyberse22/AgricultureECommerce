namespace OrderService.Services
{
    public interface IVnpayService
    {
        Task CreatePaymentUrl(decimal moneyToPay, string description);
    }
}
