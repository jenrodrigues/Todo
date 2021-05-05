using System;

namespace Todo.Domain.Entities{
    //Entity e considerada nesse contexto uma classe base. 
    //Portanto, o abstract impede que a classe seja instanciada
    public abstract class Entity: IEquatable<Entity>{
        public Guid Id { get; private set; }

        public Entity(){
            Id = Guid.NewGuid();
        }

        public bool Equals(Entity other)
        {
            return (Id == other.Id);
        }
    }
}