using System.Net;

namespace Journey.Exception.ExceptionsBase;

public class ErroValidacaoException : JourneyException
{
    public ErroValidacaoException(string message) : base(message)
    {
        
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.BadRequest;
    }
}
