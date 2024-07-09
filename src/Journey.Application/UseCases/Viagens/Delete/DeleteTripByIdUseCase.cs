using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Viagens.Delete;

public class DeleteTripByIdUseCase
{
    public void Execute(Guid id)
    {
        var dBContext = new JourneyDbContext();

        var trip = dBContext
            .Trips
            .Include(t => t.Activities)
            .FirstOrDefault(t => t.Id == id);
        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.VIAGEM_NAO_ENCONTRADA);
        }

        dBContext.Trips.Remove(trip);
        dBContext.SaveChanges();
    }

}
