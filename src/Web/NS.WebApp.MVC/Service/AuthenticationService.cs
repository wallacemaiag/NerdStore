using Microsoft.Extensions.Options;
using NS.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Service
{
    public class AuthenticatorService : Service, IAuthenticatorService
    {
        private readonly HttpClient _httpClient;

        public AuthenticatorService(HttpClient httpClient, 
                                    IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthenticationUrl);
            _httpClient = httpClient;
        }

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContent = GetContent(userLogin);

            var response = await _httpClient.PostAsync("/api/identity/autenticar", loginContent);

            if (!HandleErrorsResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializerObjectResponse<ResponseResult>(response)
                };
            }

            return await DeserializerObjectResponse<UserResponseLogin>(response);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("/api/identity/nova-conta", registerContent);

            if (!HandleErrorsResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializerObjectResponse<ResponseResult>(response),
                };
            }

            return await DeserializerObjectResponse<UserResponseLogin>(response);
        }
    }
}
