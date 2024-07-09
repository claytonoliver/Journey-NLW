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

        var validator = new RegisterTripValidator();
        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErroValidacaoException(errorMessages);
        }
    }
}