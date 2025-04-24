using UserService.Models;

namespace UserService.Services.Impl
{
    public class UserServiceClient
    {
        private readonly HttpClient _http;

        public UserServiceClient(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("user-service-client");
        }

        public async Task<string?> SignInAsync(SignInModel model)
        {
            var response = await _http.PostAsJsonAsync("/api/Account/SignIn", model);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> SignUpAsync(SignUpModel model)
        {
            var response = await _http.PostAsJsonAsync("/api/Account/SignUp", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<UserModel?> GetCurrentUserAsync(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/Account/GetCurrentUser");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _http.SendAsync(request);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<UserModel>();
        }

        // ... các hàm khác tương ứng API
    }

}
