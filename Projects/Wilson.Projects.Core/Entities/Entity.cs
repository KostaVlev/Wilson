using System;

namespace Wilson.Projects.Core.Entities
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; } = new Guid();
    }
}
