using System.Threading.Tasks;
using FitHub.WebApp.Models;
using FitHub.WebApp.RequestModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitHub.WebApp.Controllers
{
    [Route("api/Registration")]
    public class RegistrationController : Controller
    {
        private readonly UserManager<Customer> userManager;

        public RegistrationController(UserManager<Customer> userManager)
        {
            this.userManager = userManager;
        }
        
        [HttpPost]
        public async Task<IActionResult> Registration ([FromBody] RegistrationRequest request)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer()
                {
                    UserName = request.Email,
                    Email = request.Email,
                };

                var identity = await userManager.CreateAsync(customer, request.Password);
                if (!identity.Succeeded)
                {
                    return BadRequest();
                }

                return Ok(customer.Id);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}