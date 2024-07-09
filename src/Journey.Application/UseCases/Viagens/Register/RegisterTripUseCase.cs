using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Viagens.Register;
//regras de negócio
public class RegisterTripUseCase
{
    public ResponseShortTripJson Executar(RequestRegisterTripJson request)
    {
        Validar(request);

        var dbContext = new JourneyDbContext();

        var entity = new Trip
        {
            Name = request.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };

        dbContext.Add(entity);
        dbContext.SaveChanges();

        return new ResponseShortTripJson
        {
            Name = entity.Name,
            EndDate = entity.EndDate,
            StartDate = entity.StartDate,
            Id = entity.Id
        };
    }

    private void Validar(RequestRegisterTripJson request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ErroValidacaoException(ResourceErrorMessages.NOME_VAZIO);
        }

        if (request.StartDate.Date < DateTime.UtcNow.Date)
        {
            throw new ErroValidacaoException(ResourceErrorMessages.DATA_INICIO_MENOR_DATA_HOJE);
        }

        if (request.EndDate.Date < request.StartDate.Date)
        {
            throw new ErroValidacaoException(ResourceErrorMessages.DATA_FIM_MENOR_DATA_INICIO);
        }
    }
}