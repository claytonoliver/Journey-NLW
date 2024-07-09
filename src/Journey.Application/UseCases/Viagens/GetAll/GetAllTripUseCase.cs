using Journey.Communication.Responses;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Viagens.GetAll;

public class GetAllTripUseCase
{
    public ResponseTripsJson Execute()
    {
        var dBContext = new JourneyDbContext();

        var trips = dBContext.Trips.ToList();

        return new ResponseTripsJson
        {
            Trips = trips.Select(t => new ResponseShortTripJson
            {
                Id = t.Id,
                Name = t.Name,
                EndDate = t.EndDate,
                StartDate = t.StartDate
            }).ToList()
        };
    }
}
