using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Viagens.Register;

// a classe criada deve herdar de AbstractValidator, e passar a classe a ser validada antes
public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
{
    //criar construtor como "Função especial" sempre que instanciarmos RegisterTripValidator
    public RegisterTripValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NOME_VAZIO);
        
        RuleFor(r => r.StartDate.Date)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage(ResourceErrorMessages.DATA_INICIO_MENOR_DATA_HOJE);
        
        //Sempre o comparador deverá ser sempre True no must
        RuleFor(r => r)
            .Must(r => r.EndDate.Date >= r.StartDate.Date)
            .WithMessage(ResourceErrorMessages.DATA_FIM_MENOR_DATA_INICIO);
    }
}
