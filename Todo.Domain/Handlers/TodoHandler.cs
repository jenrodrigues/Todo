using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers{
    public class TodoHandler : Notifiable, IHandler<CreateTodoCommand>
    {
        private readonly ITodoRepository _repository;
        public TodoHandler(ITodoRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult Handle(CreateTodoCommand command)
        {
            //FAIL FAST VALIDATION
            command.Validate();
            if(command.Invalid){
                return new GenericCommandResult(false, "Ops, algo errado com a tarefa", command.Notifications);
            }

            //criar um todo item
            var todoItem = new TodoItem(command.Title, command.User, command.Date);

            //Salvar um todo item no repositorio (persistencia de dados)
            _repository.Create(todoItem);

            //Notificar o usuario
            return new GenericCommandResult(true, "Tarefa salva com sucesso", todoItem);
        }

    
    }
}