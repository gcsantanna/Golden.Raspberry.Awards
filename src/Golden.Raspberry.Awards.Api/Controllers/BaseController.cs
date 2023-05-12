using Golden.Raspberry.Awards.Api.Domain.Enum;
using Golden.Raspberry.Awards.Api.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Golden.Raspberry.Awards.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public ActionResult HandleError<T>(Response<T> response)
        {
            ObjectResult DefaultError()
            {
                return Problem(response.DetalheErro);
            }

            return response.MotivoErro switch
            {
                MotivoErro.Conflict => Conflict(new NotificationResult { DetalheErro = response.DetalheErro }),
                MotivoErro.BadRequest => BadRequest(new NotificationResult { DetalheErro = response.DetalheErro }),
                MotivoErro.InternalServerError => DefaultError(),
                _ => DefaultError()
            };
        }
    }
}
