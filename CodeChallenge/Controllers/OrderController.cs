using System;
using System.Threading.Tasks;
using CodeChallenge.Services;
using CodeChallenge.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _service;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderViewModel viewModel)
        {
            try
            {
                var order = await _service.Create(viewModel);
                return new OkObjectResult(order);
            }
            catch (ArgumentNullException e)
            {
                _logger.LogError(e, "ArgumentNullException thrown in Organization Controller");
                return  BadRequest("The request you sent is invalid!");
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, "ArgumentException thrown in Organization Controller");
                return NotFound("The request you sent contains values that do not exist!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown in Organization Controller");
                return Problem("Something went wrong we are looking into the issue...");
            }
        }

    }
}
