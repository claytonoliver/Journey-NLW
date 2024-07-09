namespace Journey.Communication.Responses;

public class ResponseErrorsJson
{
    public IList<string> Errors { get; set; } = [];

    //com o construtor obrigamos ele a sempre receber uma lista
    public ResponseErrorsJson(IList<string> errors)
    {
        Errors = errors;
    }
}
