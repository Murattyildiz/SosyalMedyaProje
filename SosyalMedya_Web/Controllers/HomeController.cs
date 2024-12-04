using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SosyalMedya_Web.Models;
using System.Diagnostics;

namespace SosyalMedya_Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles="admin")]
        [HttpGet]
       public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var responseMessage = await httpClient.GetAsync("http://localhost:5280/api/Iceriks/geticerikwithdetails");
           if(responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                var apiDataResponse = JsonConvert.DeserializeObject<ApiDataResponse<IcerikDetail>>(jsonResponse);

                return apiDataResponse.Success ? View(apiDataResponse.Data) : (IActionResult)View("Veri Gelmiyor");
            }
            return View("Veri gelmiyor");
        }
    }
}
