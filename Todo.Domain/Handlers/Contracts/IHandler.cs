using Todo.Domain.Commands.Contracts;

//Um "contrato" de handler define o que o
//passo-a-passo que o handler deve executar
namespace Todo.Domain.Handlers.Contracts{
    public interface IHandler<T> where T: ICommand{
        ICommandResult Handle(ICommand command);
    }

}