namespace ECommerceGateway
{
    public static class EnpointMapper
    {
        public static void MapEndpoints(WebApplication app)
        {
            // Endpoint SignIn
            app.MapPost("/account/SignIn", async (HttpContext context, IHttpClientFactory clientFactory) =>
            {
                var client = clientFactory.CreateClient("UserServiceClient");
                var requestBody = await context.Request.ReadFromJsonAsync<SignInModel>();

                var response = await client.PostAsJsonAsync("/api/Account/SignIn", requestBody);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                return Results.Ok(result);
            });

            //// Endpoint SignUp
            //app.MapPost("/account/SignUp", async (HttpContext context, IHttpClientFactory clientFactory) =>
            //{
            //    var client = clientFactory.CreateClient("UserServiceClient");
            //    var requestBody = await context.Request.ReadFromJsonAsync<SignUpModel>();

            //    var response = await client.PostAsJsonAsync("/api/Account/SignUp", requestBody);
            //    response.EnsureSuccessStatusCode();

            //    var result = await response.Content.ReadFromJsonAsync<bool>();
            //    return Results.Ok(result);
            //});

            // Endpoint GetCurrentUser
            app.MapGet("/account/GetCurrentUser", async (IHttpClientFactory clientFactory) =>
            {
                var client = clientFactory.CreateClient("UserServiceClient");
                var response = await client.GetAsync("/api/Account/GetCurrentUser");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<object>();
                return Results.Ok(result);
            });

            // Thêm các endpoint khác nếu cần
        }
    }
}
