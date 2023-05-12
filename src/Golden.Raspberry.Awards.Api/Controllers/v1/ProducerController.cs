using Golden.Raspberry.Awards.Api.Domain.Abstraction.Services;
using Golden.Raspberry.Awards.Api.Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Golden.Raspberry.Awards.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("v1/[controller]")]
    public class ProducerController : BaseController
    {
        private readonly IProducerService _produtorService;

        public ProducerController(IProducerService produtorService)
        {
            _produtorService = produtorService;
        }

        /// <summary>
        /// Retorna a lista dos recursos
        /// </summary>		
        /// <response code="200">Retorna uma lista de recuros.</response>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(ProducerResponseViewModel))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(NotificationResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(NotificationResult))]
        public async Task<IActionResult> GetProduces()
        {
            var response = await _produtorService.GetProducersAsync();

            return await Task.FromResult(response.PossuiErro
                ? HandleError(response)
                : Ok(response.Dados));
        }
    }
}
