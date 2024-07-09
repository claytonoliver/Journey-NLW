using Journey.Communication.Enums;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Viagens.GetById;

public class GetByIdUseCase
{
    public ResponseTripJson Execute(Guid id)
    {
        var dbContext = new JourneyDbContext();

        //first vs firstOrDefault 
        //first caso não venha nulo irá gerar uma exceção
        //FirstOrDefault não irá gerar a exceção
        var trip = dbContext
            .Trips
            .Include(t => t.Activities) // Include significa que ele vai fazer o join com a tabela de atividades
            .FirstOrDefault(t => t.Id == id);

        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.VIAGEM_NAO_ENCONTRADA);
        }

        return new ResponseTripJson
        {
            Id = trip.Id,
            Name = trip.Name,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            Activities = trip.Activities.Select(A => new ResponseActivityJson
            {
                Id = A.Id,
                Name = A.Name,
                Date = A.Date,
                Status = (Communication.Enums.ActivityStatus)A.Status,
            }).ToList()
        };
    }
}
