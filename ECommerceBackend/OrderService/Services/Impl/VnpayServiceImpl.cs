using VNPAY.NET;

namespace OrderService.Services.Impl
{
    public class VnpayServiceImpl
    {
        private readonly IVnpay _vnpay;
        private readonly IConfiguration _configuration;

        public VnpayServiceImpl(IVnpay vnpay, IConfiguration configuration)
        {
            _vnpay = vnpay;
            _configuration = configuration;
            _vnpay.Initialize(_configuration["Vnpay:TmnCode"], _configuration["Vnpay:HashSecret"], _configuration["Vnpay:BaseUrl"], _configuration["Vnpay:ReturnUrl"]);
        }
    }
}
