//using MassTransit;
//using OrderService.Models;
//using System;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace OrderService.Services.Impl
//{
//    public class OrderQueryConsumer : IConsumer<OrderQueriedMessage>
//    {
//        private readonly HttpClient _httpClient;
//        private readonly string _cartServiceBaseUrl;

//        public OrderQueryConsumer(IHttpClientFactory httpClientFactory, IConfiguration configuration)
//        {
//            _httpClient = httpClientFactory.CreateClient();
//            _cartServiceBaseUrl = configuration.GetValue<string>("CartService:BaseUrl", "http://localhost:7004");
//            _httpClient.BaseAddress = new Uri(_cartServiceBaseUrl);
//        }
//        public async Task Consume(ConsumeContext<OrderQueriedMessage> context)
//        {
//            var message = context.Message;

//            try
//            {
//                Console.WriteLine($"Order query for UserId: {message.UserId}, OrderCount: {message.OrderCount}, Timestamp: {message.Timestamp}");

//                // Check cart status via CartService API
//                var response = await _httpClient.GetAsync($"/api/cart/{message.UserId}");
//                if (response.IsSuccessStatusCode)
//                {
//                    var cart = await response.Content.ReadAsStringAsync();
//                    Console.WriteLine($"Cart for UserId {message.UserId}: {cart}");
//                }
//                else
//                {
//                    Console.WriteLine($"No cart found for UserId: {message.UserId}, Status: {response.StatusCode}");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error processing OrderQueriedMessage: {ex.Message}");
//                throw;
//            }
//        }
//    }
//}
