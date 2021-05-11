using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers{
    //Handlers pra cada um dos commands de um TodoItem
    public class TodoHandler : Notifiable, IHandler<CreateTodoCommand>, 
        IHandler<UpdateTitleCommand>, IHandler<MarkTodoAsDoneCommand>, 
        IHandler<MarkTodoAsUndoneCommand>
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

        public ICommandResult Handle(UpdateTitleCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid) return new GenericCommandResult(false, "Ops, parece que sua tarefa esta errada!", command.Notifications);

            //Recuperando o TodoItem a ser alterado ("re-hidratacao")
            var todo = _repository.GetById(command.Id, command.User);

            //Alterando o titulo
            todo.UpdateTitle(command.Title);

            //Salva no banco de dados
            _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);

        }

        public ICommandResult Handle(MarkTodoAsDoneCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid) return new GenericCommandResult(false, "Ops, parece que sua tarefa esta errada!", command.Notifications);

            //Recuperando o TodoItem a ser alterado ("re-hidratacao")
            var todo = _repository.GetById(command.Id, command.User);

            //Alterando o estado
            todo.MarkAsDone();

            //Salva no banco de dados
            _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);
        }

        public ICommandResult Handle(MarkTodoAsUndoneCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (command.Invalid) return new GenericCommandResult(false, "Ops, parece que sua tarefa esta errada!", command.Notifications);

            //Recuperando o TodoItem a ser alterado ("re-hidratacao")
            var todo = _repository.GetById(command.Id, command.User);

            //Alterando o estado
            todo.MarkAsUndone();

            //Salva no banco de dados
            _repository.Update(todo);

            return new GenericCommandResult(true, "Tarefa salva", todo);
        }
    }
}