using System.Net;

namespace Journey.Exception.ExceptionsBase;

public class ErroValidacaoException : JourneyException
{
    private readonly IList<string> _messageErrors;

    public ErroValidacaoException(IList<string> messages) : base(string.Empty)
    {
        _messageErrors = messages;
    }
    public override IList<string> GetErrorMessages()
    {
        return _messageErrors;
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.BadRequest;
    }
}
