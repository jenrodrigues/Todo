//Command responsavel por CRIAR UM NOVO TODO ITEM

using System;
using Flunt.Notifications;
using Flunt.Validations;
using Todo.Domain.Commands.Contracts;

namespace Todo.Domain.Commands{
    public class CreateTodoCommand: Notifiable, ICommand{
        public CreateTodoCommand()
        {
            
        }
        public CreateTodoCommand(string title, string user, DateTime date)
        {
            Title = title;
            User = user;
            Date = date;            
        }
        public string Title { get;  set; }
        public string User { get;  set; }
        public DateTime Date { get;  set; }

        //Todo comando disparado para a aplicacao precisa ser validado.
        //As validacoes especificar para a CRIACAO DE UM NOVO TODO ITEM
        //serao feitas nesse metodo.
        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Title, 3, "Title", "Titulo da tarefa precisa de mais detalhes")
                    .HasMinLen(User, 6, "User", "Usuario nao possui numero minimo de caracteres necessario")
            );
        }
    }
   





}