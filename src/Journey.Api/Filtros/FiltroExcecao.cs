﻿using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filtros
{
    public class FiltroExcecao : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JourneyException)
            {
                var journeyException = (JourneyException)context.Exception;

                context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();
                
                var responseJson = new ResponseErrorsJson(journeyException.GetErrorMessages());
                
                context.Result = new ObjectResult(responseJson);
            }
            else 
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var responseJson = new ResponseErrorsJson(
                    new List<string> { "Erro Desconhecido"
                    });

                context.Result = new ObjectResult(responseJson);
            }
        }
    }
}
