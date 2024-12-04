using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SosyalMedya_Web.Models;
using SosyalMedya_Web.Models.Utilities.Helpers;
using System.Security.Claims;
using System.Text;

namespace SosyalMedya_Web.Controllers
{
    public class AuthController : Controller
    {
        private const string AdminRole = "admin";
        private const string UserRole = "user";
        private readonly IHttpClientFactory _httpClientFactory;
        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("giris-yap")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("LoginPost")]
        public async Task<IActionResult> Login(UserForLogin userForLogin)
        {
            var httpClient=_httpClientFactory.CreateClient();
            var jsonLogin = JsonConvert.SerializeObject(userForLogin);
            StringContent conten=new StringContent(jsonLogin, Encoding.UTF8, "application/json");
            var responseMessage = await httpClient.PostAsync("http://localhost:5280/api/Auth/login", conten);

            if(responseMessage.IsSuccessStatusCode)
            {
                var userForLoginSuccess=await GetUserForLogin(responseMessage);
                TempData["Message"] = userForLoginSuccess.Message;
                TempData["Success"] = userForLoginSuccess.Success;
                var JwtToken=userForLoginSuccess.Data.Token;
                var roleClaims=ExtractRoleClaimsFromJwtToken.GetRoleClaims(JwtToken);
                var userId=ExtractUserIdentityFromJwtToken.GetUserIdentityFromJwtToken(JwtToken);

                HttpContext.Session.SetInt32("UserId",userId);

                return await SignInUserByRole(roleClaims);
            }
            else
            {
                var userForLoginError = await GetUserForLogin(responseMessage);
                TempData["LoginFail"] = userForLoginError.Message;
                return RedirectToAction("Login", "Auth");
            }
           
        }

        private async Task<IActionResult> SignInUserByRole(List<string> roleClaims)
        {
            if (roleClaims != null && roleClaims.Any())
            {
                if (roleClaims.Contains(AdminRole))
                {
                    return await SignInUserByRoleClaim(AdminRole);
                }
                if (roleClaims.Contains(UserRole))
                {
                    return await SignInUserByRoleClaim(UserRole);
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        private async Task<IActionResult> SignInUserByRoleClaim(string role)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, role) };
            var userIdentity=new ClaimsIdentity(claims,role);
            var userPrincipal=new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(userPrincipal);
            return RedirectToAction("Index", "Home");

        }

        private async Task<ApiAuthDataResponse<UserForLogin>> GetUserForLogin(HttpResponseMessage responseMessage)
        {
            string responseContent=await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiAuthDataResponse<UserForLogin>>(responseContent);
        }
    }
}