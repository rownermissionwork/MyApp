using Microsoft.AspNetCore.Mvc;
using Service.Application.Dtos.Category;
using Service.Application.Dtos.Services;
using Service.Application.Interfaces;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController(ICategoryService category,IServicesService services) : ControllerBase
    {
        private readonly ICategoryService _category = category;
        private readonly IServicesService _services = services;

        // POST api/<ServiceController>
        [HttpPost("Category/Add")]
        public async Task<IActionResult> AddCategory(CancellationToken cancellationToken,[FromBody] AddCategoryRequest request)
        {
            try {
                var result = await _category.AddCategory(request, cancellationToken);
                if (!result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Error);
                }
                return StatusCode(StatusCodes.Status201Created, result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"unable to save the changes: {ex.Message}");
            }
        }

        // GET: api/<ServiceController>
        [HttpGet("Category/GetAll")]
        public async Task<ActionResult<List<CategoriesDto>>> GetCategoryList(CancellationToken cancellationToken)
        {
            try {
                return await _category.GetAllAsync(cancellationToken);
            }
            catch (Exception ex) {
                return StatusCode(500, $"There was an error processing your request. Please try again : {ex.Message}");
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddService([FromBody] AddServiceRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _services.AddService(request, cancellationToken);
                if (!result.IsSuccess)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Error);
                }
                return StatusCode(StatusCodes.Status201Created, result.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"unable to save the changes: {ex.Message}");
            }
        }
        [HttpGet("Get/{categoryPublicId}")]
        public async  Task<ActionResult<List<ServicesDto>>> Get(string categoryPublicId, CancellationToken cancellationToken)
        {
            try
            {
                return await _services.GetServicesByCategoryPublicIdAsync(categoryPublicId, cancellationToken);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"unable to save the changes: {ex.Message}");
            }
        }
        // GET: api/<ServiceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ServiceController>
        [HttpPost]
        public void Update([FromBody] string value)
        {
        }

        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
