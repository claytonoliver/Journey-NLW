using Journey.Application.UseCases.Viagens.Delete;
using Journey.Application.UseCases.Viagens.GetAll;
using Journey.Application.UseCases.Viagens.GetById;
using Journey.Application.UseCases.Viagens.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViagensController : ControllerBase
    {
        
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status400BadRequest)]
        //**ActionResult e referente a um retorno da ação gerada pelo metodo
        //função a ser executada, nota que os parametros serão enviados ao tripjson que automaticamente irá captar valores do corpo json da requisição
        public IActionResult Registrar([FromBody]RequestRegisterTripJson request)
        {
            var useCase = new RegisterTripUseCase();

            var response = useCase.Executar(request);

            //string vazia pois a url é vazia
            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status400BadRequest)]
        public IActionResult GetAll()
        {
            var useCase = new GetAllTripUseCase();

            var result = useCase.Execute();
            return Ok(result);
        }

        // não é possivel ter dois get, post... no mesmo controller pois se não ele dara erro
        //pode ser feito assim [HttpGet ("")] ou como abaixo o id é o mesmo do parametro
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute]Guid id)
        {
            var useCase = new GetByIdUseCase();

            var response = useCase.Execute(id);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var useCase = new DeleteTripByIdUseCase();

            useCase.Execute(id);

            return NoContent();
        }
    }
}
